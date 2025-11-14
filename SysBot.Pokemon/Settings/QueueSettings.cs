using System;
using System.ComponentModel;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace SysBot.Pokemon;

public enum FlexBiasMode
{
    Add,

    Multiply,
}

public enum FlexYieldMode
{
    LessCheatyFirst,

    Weighted,
}

public class QueueSettings
{
    private const string FeatureToggle = nameof(FeatureToggle);

    private const string QueueToggle = nameof(QueueToggle);

    private const string TimeBias = nameof(TimeBias);

    private const string UserBias = nameof(UserBias);

    [Category(FeatureToggle), Description("允许用户在交易过程中退出队列.")]
    [DisplayName("是否在交易时出列")]
    public bool CanDequeueIfProcessing { get; set; }

    [Category(FeatureToggle), Description("切换用户是否可以加入队列.")]
    [DisplayName("是否允许加入队列")]

    public bool CanQueue { get; set; } = true;

    [Category(TimeBias), Description("乘以队列中的用户数量，以估算用户被处理前需要的时间.")]
    [DisplayName("估计延迟分子")]

    public float EstimatedDelayFactor { get; set; } = 1.1f;

    [Category(FeatureToggle), Description("确定灵活模式（Flex Mode）如何处理队列.")]
    [DisplayName("灵活模式")]

    public FlexYieldMode FlexMode { get; set; } = FlexYieldMode.Weighted;

    [Category(QueueToggle), Description("计划模式：队列关闭后，经过多少秒自动解锁.")]
    [DisplayName("自动解锁时间")]

    public int IntervalCloseFor { get; set; } = 15 * 60;

    [Category(QueueToggle), Description("计划模式：队列开启后，经过多少秒自动锁定.")]
    [DisplayName("自动锁定时间")]

    public int IntervalOpenFor { get; set; } = 5 * 60;

    // General
    [Category(FeatureToggle), Description("当队列中已有此数量的用户时，禁止新增用户.")]
    [DisplayName("最大队列数")]

    public int MaxQueueCount { get; set; } = 30;

    [Category(FeatureToggle), Description("确定何时打开和关闭队列.")]
    [DisplayName("队列开关模式")]

    public QueueOpening QueueToggleMode { get; set; } = QueueOpening.Threshold;

    [Category(FeatureToggle), Description("启用后，当队列因达到最大容量而关闭时，会向公告频道发送嵌入式通知.")]
    [DisplayName("是否启用队列关闭通知")]
    public bool NotifyOnQueueClose { get; set; } = true;

    [Category(QueueToggle), Description("阈值模式：达到此用户数量时，队列将关闭.")]
    [DisplayName("阈值模式：关闭阈值")]
    public int ThresholdLock { get; set; } = 30;

    [Category(QueueToggle), Description("阈值模式：降至此用户数量时，队列将开启.")]
    [DisplayName("阈值模式：开放阈值")]
    public int ThresholdUnlock { get; set; }

    [Category(UserBias), Description("根据队列中的用户数量，调整克隆队列（Clone Queue）的权重。")]
    public int YieldMultCountClone { get; set; } = 100;

    [Category(UserBias), Description("根据队列中的用户数量，调整存储队列（Dump Queue）的权重。")]
    public int YieldMultCountDump { get; set; } = 100;

    [Category(UserBias), Description("根据队列中的用户数量，调整修正原训练家队列（FixOT Queue）的权重。")]
    public int YieldMultCountFixOT { get; set; } = 100;

    [Category(UserBias), Description("根据队列中的用户数量，调整种子检查队列（Seed Check Queue）的权重。")]
    public int YieldMultCountSeedCheck { get; set; } = 100;

    [Category(UserBias), Description("根据队列中的用户数量，调整交易队列（Trade Queue）的权重。")]
    public int YieldMultCountTrade { get; set; } = 100;

    [Category(TimeBias), Description("确定权重应添加到总权重中还是与总权重相乘。")]
    public FlexBiasMode YieldMultWait { get; set; } = FlexBiasMode.Multiply;

    [Category(TimeBias), Description("检查用户加入克隆队列（Clone Queue）后的已等待时间，并相应增加队列权重。")]
    public int YieldMultWaitClone { get; set; } = 1;

    [Category(TimeBias), Description("检查用户加入存储队列（Dump Queue）后的已等待时间，并相应增加队列权重。")]
    public int YieldMultWaitDump { get; set; } = 1;

    [Category(TimeBias), Description("检查用户加入修正原训练家队列（FixOT Queue）后的已等待时间，并相应增加队列权重。")]
    public int YieldMultWaitFixOT { get; set; } = 1;

    [Category(TimeBias), Description("检查用户加入种子检查队列（Seed Check Queue）后的已等待时间，并相应增加队列权重。")]
    public int YieldMultWaitSeedCheck { get; set; } = 1;

    // Queue Toggle
    // Flex Users
    // Flex Time
    [Category(TimeBias), Description("检查用户加入交易队列（Trade Queue）后的已等待时间，并相应增加队列权重。")]
    public int YieldMultWaitTrade { get; set; } = 1;

    /// <summary>
    /// 估算用户将被处理前的时间（分钟）。
    /// </summary>
    /// <param name="position">在队列中的位置</param>
    /// <param name="botct">处理请求的机器人数量</param>
    /// <returns>估算时间（分钟）</returns>
    public float EstimateDelay(int position, int botct) => (EstimatedDelayFactor * position) / botct;

    /// <summary>
    /// 根据队列中的用户数量和用户等待时间，获取<see cref="PokeTradeType"/>的权重。
    /// </summary>
    /// <param name="count"><see cref="type"/>对应的用户数量</param>
    /// <param name="time">下一位待处理用户加入队列的时间</param>
    /// <param name="type">队列类型</param>
    /// <returns>交易类型的有效权重。</returns>
    public long GetWeight(int count, DateTime time, PokeTradeType type)
    {
        var now = DateTime.Now;
        var seconds = (now - time).Seconds;

        var cb = GetCountBias(type) * count;
        var tb = GetTimeBias(type) * seconds;

        return YieldMultWait switch
        {
            FlexBiasMode.Multiply => cb * tb,
            _ => cb + tb,
        };
    }

    public override string ToString() => "队列加入设置";

    private int GetCountBias(PokeTradeType type) => type switch
    {
        PokeTradeType.Seed => YieldMultCountSeedCheck,
        PokeTradeType.Clone => YieldMultCountClone,
        PokeTradeType.Dump => YieldMultCountDump,
        PokeTradeType.FixOT => YieldMultCountFixOT,
        _ => YieldMultCountTrade,
    };

    private int GetTimeBias(PokeTradeType type) => type switch
    {
        PokeTradeType.Seed => YieldMultWaitSeedCheck,
        PokeTradeType.Clone => YieldMultWaitClone,
        PokeTradeType.Dump => YieldMultWaitDump,
        PokeTradeType.FixOT => YieldMultWaitFixOT,
        _ => YieldMultWaitTrade,
    };
}

public enum QueueOpening
{
    Manual,

    Threshold,

    Interval,
}
