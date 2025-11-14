using System;
using System.ComponentModel;
using System.Linq;

namespace SysBot.Pokemon;

public class TwitchSettings
{
    private const string Messages = nameof(Messages);

    private const string Operation = nameof(Operation);

    private const string Startup = nameof(Startup);

    [Category(Operation), Description("启用后，机器人将处理发送到频道的命令.")]
    [DisplayName("是否允许通过频道发送指令")]
    public bool AllowCommandsViaChannel { get; set; } = true;

    [Category(Operation), Description("启用后，机器人将允许用户通过私信发送命令（可绕过慢速模式）")]
    [DisplayName("是否允许通过私信发送指令")]
    public bool AllowCommandsViaWhisper { get; set; }

    [Category(Startup), Description("发送消息的渠道")]
    [DisplayName("目标频道")]
    public string Channel { get; set; } = string.Empty;

    [Category(Startup), Description("机器人命令前缀")]
    [DisplayName("指令前缀")]
    public char CommandPrefix { get; set; } = '$';

    [Category(Operation), Description("Discord 服务器链接.")]
    [DisplayName("Discord服务器链接")]
    public string DiscordLink { get; set; } = string.Empty;

    [Category(Messages), Description("切换分发交易是否在开始前进行倒计时.")]
    [DisplayName("分发交易倒计时")]
    public bool DistributionCountDown { get; set; } = true;

    [Category(Operation), Description("捐赠链接.")]
    [DisplayName("捐赠链接")]
    public string DonationLink { get; set; } = string.Empty;

    [Category(Operation), Description("屏障解除时发送的消息.")]
    [DisplayName("屏障解除消息")]
    public string MessageStart { get; set; } = string.Empty;

    [Category(Messages), Description("指定通用通知的发送位置.")]
    [DisplayName("通用通知发送位置")]
    public TwitchMessageDestination NotifyDestination { get; set; }

    [Category(Operation), Description("管理员用户名")]
    [DisplayName("管理员列表")]
    public string SudoList { get; set; } = string.Empty;

    [Category(Operation), Description("如果在过去 Y 秒内已发送 X 条消息，就限制机器人发送消息.")]
    [DisplayName("消息发送数量限制")]
    public int ThrottleMessages { get; set; } = 100;

    // Messaging
    [Category(Operation), Description("如果在过去 Y 秒内已发送 X 条消息，就限制该机器人发送消息.")]
    [DisplayName("消息限制限制（秒）")]
    public double ThrottleSeconds { get; set; } = 30;

    [Category(Operation), Description("如果在过去 Y 秒内已发送 X 条消息，就限制机器人发送私聊消息.")]
    [DisplayName("私信发送限制数量")]
    public int ThrottleWhispers { get; set; } = 100;

    [Category(Operation), Description("如果在过去 Y 秒内已发送 X 条消息，就限制机器人发送私聊消息.")]
    [DisplayName("私信限制时间（秒）")]

    public double ThrottleWhispersSeconds { get; set; } = 60;

    [Category(Startup), Description("机器人登录令牌")]
    [DisplayName("机器人令牌")]
    public string Token { get; set; } = string.Empty;

    [Category(Messages), Description("指定交易取消通知的发送位置.")]
    [DisplayName("交易取消通知位置")]

    public TwitchMessageDestination TradeCanceledDestination { get; set; } = TwitchMessageDestination.Channel;

    [Category(Messages), Description("指定交易完成通知的发送位置.")]
    [DisplayName("交易完成通知位置")]
    public TwitchMessageDestination TradeFinishDestination { get; set; }

    [Category(Messages), Description("指定交易搜索通知的发送位置.")]
    [DisplayName("交易搜索通知位置")]
    public TwitchMessageDestination TradeSearchDestination { get; set; }

    // Message Destinations
    [Category(Messages), Description("指定交易开始通知的发送位置.")]
    [DisplayName("交易开始通知位置")]
    public TwitchMessageDestination TradeStartDestination { get; set; } = TwitchMessageDestination.Channel;

    [Category(Operation), Description("机器人使用教程链接.")]
    [DisplayName("使用教程链接")]
    public string TutorialLink { get; set; } = string.Empty;

    [Category(Operation), Description("机器人使用教程文本.")]
    [DisplayName("使用教程文本")]
    public string TutorialText { get; set; } = string.Empty;

    // Operation
    [Category(Operation), Description("拥有这些用户名的用户无法使用该机器人.")]
    [DisplayName("用户黑名单")]
    public string UserBlacklist { get; set; } = string.Empty;

    // Startup
    [Category(Startup), Description("机器人用户名")]
    [DisplayName("机器人用户名")]
    public string Username { get; set; } = string.Empty;

    public bool IsSudo(string username)
    {
        var sudos = SudoList.Split([",", ", ", " "], StringSplitOptions.RemoveEmptyEntries);
        return sudos.Contains(username);
    }

    public override string ToString() => "Twitch集成设置";
}

public enum TwitchMessageDestination
{
    Disabled,

    Channel,

    Whisper,
}
