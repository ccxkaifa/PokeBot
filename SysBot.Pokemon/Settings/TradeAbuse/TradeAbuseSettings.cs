using System.ComponentModel;

namespace SysBot.Pokemon;

public class TradeAbuseSettings
{
    private const string Monitoring = nameof(Monitoring);
    public override string ToString() => "交易滥用监控设置";

    [Category(Monitoring), Description("当一个人在小于此设置的值（分钟）内再次出现时,将会发送通知.")]
    [DisplayName("交易冷却时间（分钟）")]
    public double TradeCooldown { get; set; }

    [Category(Monitoring), Description("当一个人忽略交易冷却时，回显消息将包含他们的任天堂账户ID.")]
    [DisplayName("冷却时间滥用是否回显任天堂在线ID")]
    public bool EchoNintendoOnlineIDCooldown { get; set; } = true;

    [Category(Monitoring), Description("如果不为空，则提供的字符串将附加到回显警报中，以便在用户违反交易冷却时间时通知你指定的任何人,对于Discord,使用 <@userIDnumber> 来提及对方.")]
    [DisplayName("Discord是否开启冷却滥用提及")]

    public string CooldownAbuseEchoMention { get; set; } = string.Empty;

    [Category(Monitoring), Description("当有人在少于此设置值（分钟）的时间内使用不同的 Discord/Twitch 账户出现时,将会发送一条通知.")]
    [DisplayName("多账户滥用时间窗口（分钟）")]
    public double TradeAbuseExpiration { get; set; } = 120;

    [Category(Monitoring), Description("当检测到有人使用多个 Discord/Twitch 账号时,回显消息将包含他们的任天堂账号ID.")]
    [DisplayName("多账户滥用是否回显任天堂在线ID")]

    public bool EchoNintendoOnlineIDMulti { get; set; } = true;

    [Category(Monitoring), Description("当检测到有人向多个游戏内账户发送信息时,回显消息将包含他们的任天堂账户ID.")]
    [DisplayName("多接收者是否回显任天堂在线ID")]
    public bool EchoNintendoOnlineIDMultiRecipients { get; set; } = true;

    [Category(Monitoring), Description("当检测到有人使用多个 Discord/Twitch 账号时,会采取此行动.")]
    [DisplayName("多用户滥用行动")]
    public TradeAbuseAction TradeAbuseAction { get; set; } = TradeAbuseAction.Quit;

    [Category(Monitoring), Description("当一个人因多账号在游戏中被封禁时,他们的在线ID会被添加到 BannedIDs 中.")]
    [DisplayName("多账户阻止用户时是否添加到禁止ID列表")]
    public bool BanIDWhenBlockingUser { get; set; } = true;

    [Category(Monitoring), Description("如果不为空,所提供的字符串将被附加到回显警报中,以便在发现用户使用多个账户时通知你指定的人.对于 Discord,请使用 <@userIDnumber> 来提及对方.")]
    [DisplayName("多账户滥用是否回显提及")]
    public string MultiAbuseEchoMention { get; set; } = string.Empty;

    [Category(Monitoring), Description("如果不为空,所提供的字符串将被附加到回声警报中,以便在发现用户向游戏中的多个玩家发送信息时,通知你指定的任何人,对于 Discord,请使用 <@userIDnumber> 来提及用户.")]
    [DisplayName("多接收者回显提及")]
    public string MultiRecipientEchoMention { get; set; } = string.Empty;

    [Category(Monitoring), Description("触发交易退出或游戏内封禁的被禁在线ID.")]
    [DisplayName("被禁止的ID列表")]
    public RemoteControlAccessList BannedIDs { get; set; } = new();

    [Category(Monitoring), Description("当遇到被封禁的ID时,在退出交易前在游戏中阻止他们.")]
    [DisplayName("是否在游戏中阻止封禁用户")]
    public bool BlockDetectedBannedUser { get; set; } = true;

    [Category(Monitoring), Description("如果不为空,所提供的字符串将被添加到 Echo 提醒中,以便在用户匹配到被封禁 ID 时通知你指定的人.对于 Discord，请使用 <@userIDnumber> 来提及对方.")]
    [DisplayName("被禁ID匹配是否回显提及")]

    public string BannedIDMatchEchoMention { get; set; } = string.Empty;

    [Category(Monitoring), Description("当检测到使用 Ledy 昵称的用户存在滥用行为时，回显消息将包含他们的任天堂账户ID.")]
    [DisplayName("Ledy昵称滥用是否回显任天堂在线ID")]
    public bool EchoNintendoOnlineIDLedy { get; set; } = true;

    [Category(Monitoring), Description("如果不为空,所提供的字符串将被附加到 Echo 警报中,以便在用户违反 Ledy 交易规则时通知你指定的任何人.对于 Discord,请使用 <@userIDnumber> 来提及用户.")]
    [DisplayName("Ledy滥用是否回显提及")]
    public string LedyAbuseEchoMention { get; set; } = string.Empty;
}
