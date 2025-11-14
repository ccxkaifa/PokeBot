using System.ComponentModel;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace SysBot.Pokemon;

public sealed class PokeTradeHubConfig : BaseConfig
{
    [Browsable(false)]
    private const string BotEncounter = nameof(BotEncounter);

    private const string BotTrade = nameof(BotTrade);

    private const string Integration = nameof(Integration);
    // 修复：补充 Operation 常量定义（原代码缺失导致编译报错）
    private const string Operation = nameof(Operation);

    [Category(BotTrade), Description("Name of the Discord Bot the Program is Running. This will Title the window for easier recognition. Requires program restart.")]
    [DisplayName("Discord机器人名称")]
    public string BotName { get; set; } = string.Empty;

    [Category(Integration)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Discord配置")]
    public DiscordSettings Discord { get; set; } = new();

    [Category(BotTrade), Description("Settings for idle distribution trades.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("空闲时分发配置")]
    public DistributionSettings Distribution { get; set; } = new();

    // Encounter Bots - For finding or hosting Pokémon in-game.
    [Browsable(false)]
    [Category(BotEncounter)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("剑盾遭遇战配置")]
    public EncounterSettings EncounterSWSH { get; set; } = new();

    [Category(Integration), Description("Allows favored users to join the queue with a more favorable position than unfavored users.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("优先用户设置")]
    public FavoredPrioritySettings Favoritism { get; set; } = new();

    [Category(Operation)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("队列配置")]
    public QueueSettings Queues { get; set; } = new();

    [Browsable(false)]
    [Category(BotEncounter)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("剑盾 raids 配置")]
    public RaidSettings RaidSWSH { get; set; } = new();

    [Browsable(false)]
    [Category(BotTrade)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("剑盾种子检测配置")]
    public SeedCheckSettings SeedCheckSWSH { get; set; } = new();

    [Browsable(false)]
    [DisplayName("是否打乱顺序")]
    public override bool Shuffled => Distribution.Shuffled;

    [Browsable(false)]
    [Category(BotEncounter), Description("Stop conditions for EncounterBot.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("遭遇战机器人停止条件")]
    public StopConditionSettings StopConditions { get; set; } = new();

    [Category(Integration), Description("Configure generation of assets for streaming.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("流媒体资源配置")]
    public StreamSettings Stream { get; set; } = new();

    [Browsable(false)]
    [Category(Integration), Description("Users Theme Option Choice.")]
    [DisplayName("用户主题选项")]
    public string ThemeOption { get; set; } = string.Empty;

    [Category(Operation), Description("Add extra time for slower Switches.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("时序配置（适配低速Switch）")]
    public TimingSettings Timings { get; set; } = new();

    // Trade Bots
    [Category(BotTrade)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("交易配置")]
    public TradeSettings Trade { get; set; } = new();

    [Category(BotTrade)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("交易滥用防护配置")]
    public TradeAbuseSettings TradeAbuse { get; set; } = new();

    // Integration
    [Category(Integration)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Twitch配置")]
    public TwitchSettings Twitch { get; set; } = new();

    [Category(Integration)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("YouTube配置")]
    public YouTubeSettings YouTube { get; set; } = new();

    [Category(Operation), Description("Settings for automatic bot recovery after crashes.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("崩溃自动恢复配置")]
    public RecoverySettings Recovery { get; set; } = new();

    [Category(Integration), Description("Settings for the Web Control Panel server.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Web控制面板服务器配置")]
    public WebServerSettings WebServer { get; set; } = new();
}
