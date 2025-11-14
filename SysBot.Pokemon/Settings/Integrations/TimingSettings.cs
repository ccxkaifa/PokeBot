using System.ComponentModel;

namespace SysBot.Pokemon;

public class TimingSettings
{
    private const string CloseGame = nameof(CloseGame);

    private const string Misc = nameof(Misc);

    private const string OpenGame = nameof(OpenGame);

    private const string Raid = nameof(Raid);

    [Category(Misc), Description("启用此功能以拒绝系统更新.")]
    [DisplayName("拒绝系统更新")]
    public bool AvoidSystemUpdate { get; set; }

    [Category(Misc), Description("在尝试重新连接之间等待的额外时间(毫秒),基础时间为30秒.")]
    [DisplayName("额外重连延迟")]
    public int ExtraReconnectDelay { get; set; }

    [Category(Raid), Description("[RaidBot] 接受好友后额外等待的时间（毫秒）.")]
    [DisplayName("额外添加好友延迟")]

    public int ExtraTimeAddFriend { get; set; }

    [Category(CloseGame), Description("点击关闭游戏后需要额外等待的时间（以毫秒为单位）.")]
    [DisplayName("关闭游戏额外延迟")]

    public int ExtraTimeCloseGame { get; set; }

    // Miscellaneous settings.
    [Category(Misc), Description("[剑盾/朱紫/传说ZA] 在点击“+”连接Y-Comm（剑盾）、点击“L”连接网络（朱紫）或连接Portal（传说ZA）后，等待的额外时间。传说ZA的基础等待时间为8秒.")]
    [DisplayName("额外链接延迟（SWSH/SV/PLZA）")]

    public int ExtraTimeConnectOnline { get; set; }

    [Category(Raid), Description("[RaidBot] 删除好友后额外等待的时间（毫秒）.")]
    [DisplayName("额外删除好友延迟")]
    public int ExtraTimeDeleteFriend { get; set; }

    [Category(Raid), Description("[RaidBot] 关闭游戏以重置 raids 前需要额外等待的时间（毫秒）.")]
    [DisplayName("额外关闭游戏重置Raid之前等待的额外时间（毫秒）")]

    public int ExtraTimeEndRaid { get; set; }

    [Category(Misc), Description("[BDSP]在尝试发起交易之前，等待联盟房间加载额外的毫秒时间.")]
    [DisplayName("额外加载联合室延迟（BDSP）")]
    public int ExtraTimeJoinUnionRoom { get; set; } = 500;

    [Category(Misc), Description("[BDSP] 离开联盟房间后，等待主世界加载的额外时间（以毫秒为单位）.")]
    [DisplayName("额外加载主世界延迟（BDSP）")]

    public int ExtraTimeLeaveUnionRoom { get; set; } = 1000;

    [Category(OpenGame), Description("在标题屏幕点击 A 之前需要额外等待的毫秒数.")]
    [DisplayName("标题屏幕点击A延迟")]
    public int ExtraTimeLoadGame { get; set; } = 5000;

    [Category(OpenGame), Description("标题屏幕后等待主世界加载的额外时间（以毫秒为单位）.")]
    [DisplayName("标题屏幕后加载主世界延迟")]

    public int ExtraTimeLoadOverworld { get; set; } = 3000;

    [Category(Misc), Description("[SV] 等待宝可梦传送门加载的额外时间（毫秒）.")]
    [DisplayName("额外加载宝可梦入口站延迟（SV）")]
    public int ExtraTimeLoadPortal { get; set; } = 1000;

    // Opening the game.
    [Category(OpenGame), Description("如果在启动游戏时需要选择一个配置文件,请启用此功能.")]
    [DisplayName("需要选择配置文件")]
    public bool ProfileSelectionRequired { get; set; } = true;

    [Category(OpenGame), Description("启动游戏时等待配置文件加载的额外时间（以毫秒为单位）.")]
    [DisplayName("启动游戏时加载配置文件延迟")]
    public int ExtraTimeLoadProfile { get; set; }

    [Category(OpenGame), Description("启用此功能可为 “检查游戏是否可玩” 弹窗添加延迟.")]
    [DisplayName("检查游戏延迟")]
    public bool CheckGameDelay { get; set; } = false;

    [Category(OpenGame), Description("等待 “正在检查游戏是否可以运行” 弹窗的额外时间.")]
    [DisplayName("检查游戏额外延迟")]
    public int ExtraTimeCheckGame { get; set; } = 200;

    // Raid-specific timings.
    [Category(Raid), Description("[RaidBot] 点击巢穴后，等待Raid加载的额外时间（毫秒）.")]
    [DisplayName("额外加载Raid延迟")]
    public int ExtraTimeLoadRaid { get; set; }

    [Category(Misc), Description("找到交易后等待盒子加载的额外时间（以毫秒为单位）.")]
    [DisplayName("额外加载盒子延迟")]
    public int ExtraTimeOpenBox { get; set; } = 1000;

    [Category(Misc), Description("交易时打开键盘进行代码输入后需要等待的时间（毫秒）.")]
    [DisplayName("打开键盘输入代码延迟")]
    public int ExtraTimeOpenCodeEntry { get; set; } = 1000;

    [Category(Raid), Description("[RaidBot] 点击 “邀请他人” 后，在锁定宝可梦之前需要额外等待的时间（以毫秒为单位）.")]
    [DisplayName("额外邀请其他人延迟")]

    public int ExtraTimeOpenRaid { get; set; }

    [Category(Misc), Description("[BDSP] 每次交易循环开始时，等待 Y 菜单加载所需的额外时间（以毫秒为单位）.")]
    [DisplayName("额外加载Y菜单延迟（BDSP）")]
    public int ExtraTimeOpenYMenu { get; set; } = 500;

    // Closing the game.
    [Category(CloseGame), Description("按下 HOME 键后最小化游戏后等待的额外时间（毫秒）.")]
    [DisplayName("最小化游戏额外延迟")]
    public int ExtraTimeReturnHome { get; set; }

    [Category(Misc), Description("在浏览 Switch 菜单或输入连接代码时，每次按键后需要等待的时间（毫秒）.")]
    [DisplayName("按键延迟")]
    public int KeypressTime { get; set; } = 200;

    [Category(Misc), Description("连接断开后尝试重新连接套接字连接的次数。将其设置为 - 1 可进行无限次尝试.")]
    [DisplayName("重连尝试次数")]
    public int ReconnectAttempts { get; set; } = 30;
    public override string ToString() => "额外时间设置";
}
