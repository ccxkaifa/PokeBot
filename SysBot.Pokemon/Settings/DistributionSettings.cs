using PKHeX.Core;
using SysBot.Base;
using System.ComponentModel;

namespace SysBot.Pokemon;

public class DistributionSettings : ISynchronizationSetting
{
    private const string Distribute = nameof(Distribute);

    private const string Synchronize = nameof(Synchronize);

    [Category(Distribute), Description("启用后,处于空闲状态的 LinkTrade 机器人将从 DistributeFolder 中随机分发 PKM 文件.")]
    [DisplayName("是否空闲时分发")]

    public bool DistributeWhileIdle { get; set; } = true;

    [Category(Distribute), Description("当设置为true时,随机 Ledy 昵称交换交易将退出,而不是从池中交易随机实体.")]
    [DisplayName("Ledy无匹配退出")]

    public bool LedyQuitIfNoMatch { get; set; }

    [Category(Distribute), Description("当设置为None以外的值时,随机交易除了要求昵称匹配外,还需要此物种.")]
    [DisplayName("Ledy物种要求")]

    public Species LedySpecies { get; set; } = Species.None;

    [Category(Distribute), Description("分发交易链接代码使用最小和最大范围,而非固定的贸易代码.")]
    [DisplayName("是否开启随机交易代码")]

    public bool RandomCode { get; set; }

    [Category(Distribute), Description("对于宝可梦晶灿钻石和明亮珍珠(BDSP),分发机器人将前往特定房间并一直保持在那里,直到机器人被停止.")]
    [DisplayName("是否留在联合房间(BDSP)")]

    public bool RemainInUnionRoomBDSP { get; set; } = true;

    // Distribute
    [Category(Distribute), Description("启用后,DistributionFolder将随机生成内容,而非按照相同的顺序生成.")]
    [DisplayName("是否启用随机分发")]

    public bool Shuffled { get; set; }

    [Category(Synchronize), Description("链接交易：使用多个分发机器人时，所有机器人将同时确认其交易代码,当设为本地模式时,所有机器人在屏蔽处就绪后将继续.当设为远程模式时,则需要其他东西向机器人发出继续运行的信号.")]
    [DisplayName("同步选项")]

    public BotSyncOption SynchronizeBots { get; set; } = BotSyncOption.LocalSync;

    // Synchronize
    [Category(Synchronize), Description("链接交易：使用多个分发机器人时一旦所有机器人准备好确认交易代码，Hub将等待X毫秒后再释放所有机器人.")]
    [DisplayName("同步延迟屏蔽")]

    public int SynchronizeDelayBarrier { get; set; }

    [Category(Synchronize), Description("链接交易：使用多个分发机器人时,机器人在无论如何继续之前会等待同步多长时间(以秒为单位).")]
    [DisplayName("同步超时时间")]

    public double SynchronizeTimeout { get; set; } = 90;

    [Category(Distribute), Description("分发交易链接代码.")]
    [DisplayName("链接代码")]

    public int TradeCode { get; set; } = 7196;

    public override string ToString() => "分发交易设置";
}
