using System;
using System.ComponentModel;
using static SysBot.Pokemon.TradeSettings;

namespace SysBot.Pokemon;

public class DiscordSettings
{
    private const string Channels = nameof(Channels);

    private const string Operation = nameof(Operation);

    private const string Roles = nameof(Roles);

    private const string Servers = nameof(Servers);

    private const string Startup = nameof(Startup);

    private const string Users = nameof(Users);

    public enum EmbedColorOption
    {
        Blue,

        Green,

        Red,

        Gold,

        Purple,

        Teal,

        Orange,

        Magenta,

        LightGrey,

        DarkGrey
    }

    public enum ThumbnailOption
    {
        Gengar,

        Pikachu,

        Umbreon,

        Sylveon,

        Charmander,

        Jigglypuff,

        Flareon,

        Custom
    }

    [Category(Startup), Description("机器人登录令牌.")]
    [DisplayName("机器人令牌")]
    public string Token { get; set; } = string.Empty;

    [Category(Operation), Description("要添加到嵌入描述开头的额外文本.")]
    [DisplayName("添加额外文本")]
    public string[] AdditionalEmbedText { get; set; } = [];

    [Category(Users), Description("禁用此功能将移除全局 sudo 支持.")]
    [DisplayName("是否移除全局支持")]
    public bool AllowGlobalSudo { get; set; } = true;

    [Category(Channels), Description("会记录特殊消息（如公告）的频道.")]
    [DisplayName("记录特别信息的频道")]
    public RemoteControlAccessList AnnouncementChannels { get; set; } = new();

    [Category(Channels), Description("会记录滥用信息的频道.")]
    [DisplayName("滥用日志频道")]
    public RemoteControlAccessList AbuseLogChannels { get; set; } = new();

    public AnnouncementSettingsCategory AnnouncementSettings { get; set; } = new();

    [Category(Startup), Description("仅考虑交易类型的机器人时，指示 Discord 在线状态颜色.")]
    [DisplayName("机器人颜色状态")]
    public bool BotColorStatusTradeOnly { get; set; } = true;

    [Category(Startup), Description("将向所有白名单频道发送在线 / 离线状态嵌入信息.")]
    [DisplayName("机器人嵌入状态")]
    public bool BotEmbedStatus { get; set; } = true;

    [Category(Startup), Description("游戏中的自定义状态.")]
    [DisplayName("机器人游戏状态")]
    public string BotGameStatus { get; set; } = "Pokémon";

    [Category(Startup), Description("将根据当前状态在频道名称中添加在线 / 离线表情符号。仅限白名单频道.")]
    [DisplayName("频道状态")]
    public bool ChannelStatus { get; set; } = true;

    [Category(Channels), Description("只有具有这些 ID 的频道，机器人才会响应命令.")]
    [DisplayName("频道白名单")]
    public RemoteControlAccessList ChannelWhitelist { get; set; } = new();

    [Category(Startup), Description("机器人命令前缀.")]
    [DisplayName("设置命令前缀")]
    public string CommandPrefix { get; set; } = "$";

    [Category(Operation), Description("机器人可以在它能看到的任何频道中回复 ShowdownSet，而不仅仅是那些已将机器人列入白名单允许其运行的频道。只有当你希望机器人在非机器人频道中发挥更多作用时，才将此设置为 true。.")]
    [DisplayName("机器人是否可有在所有频道回复ShowdownSet")]
    public bool ConvertPKMReplyAnyChannel { get; set; } = false;

    [Category(Operation), Description("当有 PKM 文件被附加（而非通过命令）时，机器人会监听频道消息，并回复一个 ShowdownSet.")]
    [DisplayName("是否开启将PKM转换为Showdown集合")]
    public bool ConvertPKMToShowdownSet { get; set; } = true;

    [Category(Users), Description("用逗号分隔的 Discord 用户 ID，这些用户将拥有 Bot Hub 的超级用户访问权限.")]
    [DisplayName("超级用户列表")]

    public RemoteControlAccessList GlobalSudoList { get; set; } = new();

    [Category(Operation), Description("当用户向机器人说 “你好” 时，机器人将回复的自定义消息。使用字符串格式化在回复中提及用户.")]
    [DisplayName("机器人回复设置")]

    public string HelloResponse { get; set; } = "Hi {0}!";

    [Category(Channels), Description("将回显日志机器人数据的频道 ID.")]
    [DisplayName("机器人回显数据频道")]
    public RemoteControlAccessList LoggingChannels { get; set; } = new();

    [Category(Startup), Description("机器人启动时不会加载的模块列表（用逗号分隔）.")]
    [DisplayName("黑名单模块")]
    public string ModuleBlacklist { get; set; } = string.Empty;

    [Category(Startup), Description("机器人离线时使用的自定义表情符号.")]
    [DisplayName("机器人离线的自定义符号设置")]
    public string OfflineEmoji { get; set; } = "❌";

    [Category(Startup), Description("机器人在线时使用的自定义表情符号.")]
    [DisplayName("机器人在线的自定义符号设置")]
    public string OnlineEmoji { get; set; } = "✅";

    [Category(Operation), Description("如果用户不被允许在频道中使用特定命令，机器人会回复他们。如果为 “False”，机器人将默默忽略他们.")]
    [DisplayName("是否允许用户使用特定命令")]
    public bool ReplyCannotUseCommandInChannel { get; set; } = true;

    [Category(Operation), Description("会向感谢该机器人的用户发送一条随机回复.")]
    [DisplayName("是否开启回复感谢")]
    public bool ReplyToThanks { get; set; } = true;

    [Category(Operation), Description("将交易中显示的宝可梦的个人密钥（PKMs）归还给用户.")]
    [DisplayName("是否归还PKMs")]
    public bool ReturnPKMs { get; set; } = true;

    [Category(Operation), Description("启用后，机器人会在延迟一段时间后自动删除错误消息和用户命令。禁用此功能可永久保留所有消息.")]
    [DisplayName("是否开启自动删除错误信息")]
    public bool MessageDeletionEnabled { get; set; } = true;

    [Category(Operation), Description("删除机器人错误 / 响应消息前需要等待的秒数。仅当 “消息删除已启用” 为true时才适用 .")]
    [DisplayName("错误消息删除延迟")]
    public int ErrorMessageDeleteDelaySeconds { get; set; } = 12;

    [Category(Operation), Description("启用后，用户的命令消息将与机器人的回复一同删除。禁用此功能可使用户命令保持可见.")]
    [DisplayName("是否开启删除用户命令消息")]
    public bool DeleteUserCommandMessages { get; set; } = true;

    [Category(Roles), Description("拥有此角色的用户可以进入克隆队列.")]
    [DisplayName("是否允许用户进入克隆队列")]

    public RemoteControlAccessList RoleCanClone { get; set; } = new() { AllowIfEmpty = true };

    [Category(Roles), Description("拥有此角色的用户被允许进入转储队列.")]
    [DisplayName("是否允许用户转储队列")]
    public RemoteControlAccessList RoleCanDump { get; set; } = new() { AllowIfEmpty = true };

    [Category(Roles), Description("拥有此角色的用户可加入OT修正队列.")]
    [DisplayName("是否允许用户加入OT修正队列")]
    public RemoteControlAccessList RoleCanFixOT { get; set; } = new() { AllowIfEmpty = true };

    [Category(Roles), Description("拥有此角色的用户被允许进入种子检查 / 特殊请求队列.")]
    [DisplayName("是否允许用户种子检测/特殊请求队列")]
    public RemoteControlAccessList RoleCanSeedCheckorSpecialRequest { get; set; } = new() { AllowIfEmpty = true };

    [Category(Roles), Description("拥有此角色的用户被允许进入交易队列.")]
    [DisplayName("是否允许用户进入交易队列")]
    public RemoteControlAccessList RoleCanTrade { get; set; } = new() { AllowIfEmpty = true };

    [Category(Roles), Description("拥有此角色的用户可以以更优的位置加入队列.")]
    [DisplayName("是否开启队列优先角色")]
    public RemoteControlAccessList RoleFavored { get; set; } = new() { AllowIfEmpty = false };

    // Whitelists
    [Category(Roles), Description("拥有此角色的用户被允许远程控制控制台（如果以远程控制机器人的身份运行）.")]
    [DisplayName("是否允许用户远程控制")]
    public RemoteControlAccessList RoleRemoteControl { get; set; } = new() { AllowIfEmpty = false };

    [Category(Roles), Description("拥有此角色的用户被允许绕过命令限制.")]
    [DisplayName("超级权限用户")]
    public RemoteControlAccessList RoleSudo { get; set; } = new() { AllowIfEmpty = false };

    // Operation
    [Category(Servers), Description("此列表中的服务器ID无法使用机器人，机器人会自动退出该服务器.")]
    [DisplayName("服务器黑名单")]
    public RemoteControlAccessList ServerBlacklist { get; set; } = new() { AllowIfEmpty = false };

    [Category(Channels), Description("将记录交易开始消息的日志通道.")]
    [DisplayName("交易开始日志频道")]

    public RemoteControlAccessList TradeStartingChannels { get; set; } = new();

    // Startup
    [Category(Users), Description("此列表中的用户ID无法使用机器人.")]
    [DisplayName("用户黑名单")]
    public RemoteControlAccessList UserBlacklist { get; set; } = new();

    public override string ToString() => "Discord 集成设置";

    [Category(Operation), TypeConverter(typeof(CategoryConverter<AnnouncementSettingsCategory>))]
    [DisplayName("公告设置")]
    public class AnnouncementSettingsCategory
    {
        [DisplayName("公告嵌入消息颜色")]
        public EmbedColorOption AnnouncementEmbedColor { get; set; } = EmbedColorOption.Purple;


        [Category("Embed Settings"), Description("公告的缩略图选项.")]
        [DisplayName("缩略图选项")]
        public ThumbnailOption AnnouncementThumbnailOption { get; set; } = ThumbnailOption.Gengar;

        [Category("Embed Settings"), Description("公告的自定义缩略图URL.")]
        [DisplayName("自定义缩略图网址")]
        public string CustomAnnouncementThumbnailUrl { get; set; } = string.Empty;

        [Category("Embed Settings"), Description("为公告启用随机颜色选择功能.")]
        [DisplayName("是否开启随机颜色")]
        public bool RandomAnnouncementColor { get; set; } = false;

        [Category("Embed Settings"), Description("为公告启用随机缩略图选择功能.")]
        [DisplayName("是否开启缩略图")]
        public bool RandomAnnouncementThumbnail { get; set; } = false;

        public override string ToString() => "公告设置";
    }
}
