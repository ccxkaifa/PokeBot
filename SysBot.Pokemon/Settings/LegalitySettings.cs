using PKHeX.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace SysBot.Pokemon;

[DisplayName("合法性设置")]
public class LegalitySettings
{
    private const string Generate = nameof(Generate);

    private const string Misc = nameof(Misc);

    private string DefaultTrainerName = "Ash";

    [Category(Generate), Description("允许用户通过批量编辑器命令提交进一步定制.")]
    [DisplayName("是否允许批处理编辑器命令")]
    public bool AllowBatchCommands { get; set; } = true;

    [Category(Generate), Description("允许用户在Showdown的配置中提交自定义的 OT,TID,SID和OT性别.")]
    [DisplayName("是否允许Showdown配置")]
    public bool AllowTrainerDataOverride { get; set; } = true;

    [Category(Generate), Description("阻止交易需要HOME追踪器的宝可梦,即使文件中已经有一个."), DisplayName("是否阻止非原生宝可梦")]
    public bool DisallowNonNatives { get; set; } = false;

    [Category(Generate), Description("防止交易已经带有HOME追踪器的宝可梦."), DisplayName("是否阻止HOME追踪宝可梦")]
    public bool DisallowTracked { get; set; } = false;

    [Category(Generate), Description("如果提供了非法配置,机器人将创建一个彩蛋宝可梦.")]
    [DisplayName("是否启用菜单")]
    public bool EnableEasterEggs { get; set; } = false;

    [Category(Generate), Description("交易那些必须在Switch游戏之间传输过的宝可梦时,需要HOME追踪码.")]
    [DisplayName("是否启用HOME追踪器检查")]
    public bool EnableHOMETrackerCheck { get; set; } = false;

    [Category(Generate), Description("假设50级配置是100级的竞技套装.")]
    [DisplayName("是否启用50级宝可梦调整为100级")]
    public bool ForceLevel100for50 { get; set; } = true;

    [Category(Generate), Description("如果合法,则强制使用指定的精灵球.")]
    [DisplayName("是否强制指定精灵球")]
    public bool ForceSpecifiedBall { get; set; } = true;

    [Category(Generate), Description("生成的PKM文件中不匹配任何提供的PKM文件的默认语言.")]
    [DisplayName("默认语音")]
    public LanguageID GenerateLanguage { get; set; } = LanguageID.English;

    [Category(Generate), Description("生成的宝可梦文件中不匹配任何提供的PKM文件的默认初始训练家名成.")]
    [DisplayName("默认训练家名称")]
    public string GenerateOT
    {
        get => DefaultTrainerName;
        set
        {
            if (!StringsUtil.IsSpammyString(value))
                DefaultTrainerName = value;
        }
    }

    [Category(Generate), Description("包含用于重新生成PKM文件的训练家数据的PKM文件的文件夹.")]
    [DisplayName("训练家信息文件夹路径")]
    public string GeneratePathTrainerInfo { get; set; } = string.Empty;

    [Category(Generate), Description("生成的宝可梦的默认的16位秘密ID (SID).")]
    [DisplayName("默认16位里ID")]
    public ushort GenerateSID16 { get; set; } = 54321;

    [Category(Generate), Description("生成的宝可梦的默认16位训练家ID (TID).")]
    [DisplayName("默认16位训练家ID")]

    public ushort GenerateTID16 { get; set; } = 12345;

    // Generate
    [Category(Generate), Description("用于配信卡片的MGDB目录路径.")]
    [DisplayName("MGDB目录路径")]
    public string MGDBPath { get; set; } = string.Empty;

    [Category(Generate), Description("尝试生成宝可梦遭遇类型的顺序.")]
    [DisplayName("遭遇类型优先级")]

    public List<EncounterTypeGroup> PrioritizeEncounters { get; set; } =
    [
        EncounterTypeGroup.Slot, EncounterTypeGroup.Egg,
        EncounterTypeGroup.Static, EncounterTypeGroup.Mystery,
        EncounterTypeGroup.Trade,
    ];

    [Category(Generate), Description("如果PrioritizeGame设置为True时,则使用PriorityOrder开始查找遭遇. 如果设置为False时, 则使用最新的游戏作为版本,建议将此保留为“True”.")]
    [DisplayName("是否优先使用游戏版本")]
    public bool PrioritizeGame { get; set; } = false;

    [Category(Generate), Description("ALM将尝试合法化的游戏版本顺序.")]
    [DisplayName("游戏版本优先级")]
    public List<GameVersion> PriorityOrder { get; set; } =
        [.. Enum.GetValues<GameVersion>().Where(ver => ver > GameVersion.Any && ver <= (GameVersion)52)];

    // Misc
    [Browsable(false)]
    [Category(Misc), Description("将克隆的和用户请求的PKM文件的HOME追踪器清零.建议保持此功能禁用,以避免创建无效的HOME数据.")]
    [DisplayName("重置HOME追踪器")]
    public bool ResetHOMETracker { get; set; } = false;

    [Category(Generate), Description("为任何生成的宝可梦设置所有可能的合法徽章.")]
    [DisplayName("是否设置所有合法徽章")]
    public bool SetAllLegalRibbons { get; set; } = false;

    [Browsable(false)]
    [Category(Generate), Description("为支持的游戏（仅限《宝可梦剑 / 盾》）添加战斗版本,以便在在线竞技对战中使用前代宝可梦.")]
    [DisplayName("设置战斗版本")]
    public bool SetBattleVersion { get; set; } = false;

    [Category(Generate), Description("为任何生成的宝可梦设置一个匹配的精灵球（基于颜色）.")]
    [DisplayName("是否设置匹配的精灵球")]

    public bool SetMatchingBalls { get; set; } = true;

    [Category(Generate), Description("在取消之前生成配置的最长时间（以秒为单位）.这可以防止困难的配置冻结机器人")]
    [DisplayName("宝可梦生成超时时间")]
    public int Timeout { get; set; } = 15;

    [Category(Misc), Description("使用训练家的OT/SID/TID应用有效宝可梦（AutoOT)")]

    [DisplayName("是否使用交易伙伴信息")]
    public bool UseTradePartnerInfo { get; set; } = true;

    public override string ToString() => "合法性生成设置";
}
