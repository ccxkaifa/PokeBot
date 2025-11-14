using System.ComponentModel;

namespace SysBot.Pokemon;

/// <summary>
/// 机器人崩溃或取消令牌停止后的自动恢复配置设置。
/// </summary>
public class RecoverySettings
{
    private const string Recovery = nameof(Recovery);

    [Category(Recovery), Description("支持对崩溃或停止运行的机器人进行自动恢复尝试.")]
    [DisplayName("启用自动恢复")]
    public bool EnableRecovery { get; set; } = true;

    [Category(Recovery), Description("放弃一个机器人之前的最大连续恢复尝试次数.")]
    [DisplayName("最大尝试次数")]
    public int MaxRecoveryAttempts { get; set; } = 3;

    [Category(Recovery), Description("尝试重启崩溃的机器人之前的初始延迟（以秒为单位).")]
    [DisplayName("初始恢复秒数")]
    public int InitialRecoveryDelaySeconds { get; set; } = 5;

    [Category(Recovery), Description("尝试恢复的最大延迟（以秒为单位）.")]
    [DisplayName("最大恢复秒数")]
    public int MaxRecoveryDelaySeconds { get; set; } = 300; // 5 minutes

    [Category(Recovery), Description("指数退避的乘数.")]
    [DisplayName("后退乘数")]
    public double BackoffMultiplier { get; set; } = 2.0;

    [Category(Recovery), Description("用于跟踪崩溃历史的时间窗口（以分钟为单位）。此窗口之外的崩溃将不被计算在内.")]
    [DisplayName("崩溃历史窗口分钟数")]
    public int CrashHistoryWindowMinutes { get; set; } = 60; // 1 hour

    [Category(Recovery), Description("在永久关闭前,历史窗口内允许的最大崩溃次数.")]
    [DisplayName("窗口内最大崩溃次数")] 

    public int MaxCrashesInWindow { get; set; } = 5;

    [Category(Recovery), Description("启用对故意停止的机器人的恢复（适用于网络断开等场景).")]
    [DisplayName("是否启用机器人恢复")]

    public bool RecoverIntentionalStops { get; set; } = false;

    [Category(Recovery), Description("成功恢复后，在重置尝试计数器之前需要等待的秒数延迟.")]
    [DisplayName("成功恢复重置延迟秒数")]

    public int SuccessfulRecoveryResetDelaySeconds { get; set; } = 300; // 5 minutes

    [Category(Recovery), Description("当机器人崩溃并尝试恢复时发送通知.")]
    [DisplayName("恢复尝试时通知")]
    public bool NotifyOnRecoveryAttempt { get; set; } = true;

    [Category(Recovery), Description("当机器人在所有尝试后仍无法恢复时发送通知.")]
    [DisplayName("恢复失败时通知")]
    public bool NotifyOnRecoveryFailure { get; set; } = true;

    [Category(Recovery), Description("机器人被视为稳定前的最短运行时间（以秒为单位）.")]
    [DisplayName("最小稳定运行时间（秒）")]
    public int MinimumStableUptimeSeconds { get; set; } = 600; // 10 minutes

    public override string ToString() => "机器人恢复设置";
}
