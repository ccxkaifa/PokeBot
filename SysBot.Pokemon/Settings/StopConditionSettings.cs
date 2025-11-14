using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SysBot.Pokemon;

public class StopConditionSettings
{
    private const string StopConditions = nameof(StopConditions);

    [Category(StopConditions), Description("当遭遇机器人或化石机器人找到匹配的宝可梦时，按住捕获按钮录制30秒片段。")]
    [DisplayName("录制视频片段")]
    public bool CaptureVideoClip { get; set; }

    [Category(StopConditions), Description("遭遇机器人或化石机器人匹配到宝可梦后，额外等待的毫秒数，之后再按下捕获键。")]
    [DisplayName("录制视频额外等待时间")]
    public int ExtraTimeWaitCaptureVideo { get; set; } = 10000;

    [Category(StopConditions), Description("仅在具有标记的宝可梦时停止。")]
    [DisplayName("仅标记宝可梦")]
    public bool MarkOnly { get; set; }

    [Category(StopConditions), Description("如果不为空，提供的字符串将添加到找到结果的日志消息前，为你指定的对象发送回声提醒。对于Discord，使用<@用户ID>进行@提及。")]
    [DisplayName("匹配找到回声提及")]
    public string MatchFoundEchoMention { get; set; } = string.Empty;

    [Category(StopConditions), Description("如果设置为TRUE，将同时匹配闪光目标和目标个体值设置。否则，寻找闪光目标或目标个体值的匹配项。")]
    [DisplayName("同时匹配闪光和个体值")]
    public bool MatchShinyAndIV { get; set; } = true;

    [Category(StopConditions), Description("选择停止时的闪光类型。")]
    [DisplayName("闪光目标类型")]
    public TargetShinyType ShinyTarget { get; set; } = TargetShinyType.DisableOption;

    [Category(StopConditions), Description("仅在具有此形态ID的宝可梦时停止。留空则无限制。")]
    [DisplayName("停止于指定形态")]
    public int? StopOnForm { get; set; }

    [Category(StopConditions), Description("仅在该物种的宝可梦时停止。设置为\"无\"则无限制。")]
    [DisplayName("停止于指定物种")]
    public Species StopOnSpecies { get; set; }

    [Category(StopConditions), Description("可接受的最大个体值格式为HP/攻击/防御/特攻/特防/速度。使用\"x\"表示不检查的个体值，使用\"/\"作为分隔符。")]
    [DisplayName("目标最大个体值")]
    public string TargetMaxIVs { get; set; } = "";

    [Category(StopConditions), Description("可接受的最小个体值格式为HP/攻击/防御/特攻/特防/速度。使用\"x\"表示不检查的个体值，使用\"/\"作为分隔符。")]
    [DisplayName("目标最小个体值")]
    public string TargetMinIVs { get; set; } = "";

    [Category(StopConditions), Description("仅在指定性格的宝可梦时停止。")]
    [DisplayName("目标性格")]
    public Nature TargetNature { get; set; } = Nature.Random;

    [Category(StopConditions), Description("要忽略的标记列表，用逗号分隔。使用全名，例如\"稀有标记, 黎明标记, 骄傲标记\"。")]
    [DisplayName("不想要的标记")]
    public string UnwantedMarks { get; set; } = "";

    public static bool EncounterFound<T>(T pk, int[] targetminIVs, int[] targetmaxIVs, StopConditionSettings settings, IReadOnlyList<string>? marklist) where T : PKM
    {
        // 匹配指定的性格和物种（如果已设置）。
        if (settings.StopOnSpecies != Species.None && settings.StopOnSpecies != (Species)pk.Species)
            return false;

        if (settings.StopOnForm.HasValue && settings.StopOnForm != pk.Form)
            return false;

        if (settings.TargetNature != Nature.Random && settings.TargetNature != (Nature)pk.Nature)
            return false;

        // 如果没有标记或有不想要的标记，则返回。
        var unmarked = pk is IRibbonIndex m && !HasMark(m);
        var unwanted = marklist is not null && pk is IRibbonIndex m2 && settings.IsUnwantedMark(GetMarkName(m2), marklist);
        if (settings.MarkOnly && (unmarked || unwanted))
            return false;

        if (settings.ShinyTarget != TargetShinyType.DisableOption)
        {
            bool shinymatch = settings.ShinyTarget switch
            {
                TargetShinyType.AnyShiny => pk.IsShiny,
                TargetShinyType.NonShiny => !pk.IsShiny,
                TargetShinyType.StarOnly => pk.IsShiny && pk.ShinyXor != 0,
                TargetShinyType.SquareOnly => pk.ShinyXor == 0,
                TargetShinyType.DisableOption => true,
                _ => throw new ArgumentException(nameof(TargetShinyType)),
            };

            // 如果只需要匹配其中一个条件且闪光匹配，则返回true。
            // 如果需要匹配两个条件且闪光不匹配，则返回false。
            if (!settings.MatchShinyAndIV && shinymatch)
                return true;
            if (settings.MatchShinyAndIV && !shinymatch)
                return false;
        }

        // 将速度重新排序到最后。
        Span<int> pkIVList = stackalloc int[6];
        pk.GetIVs(pkIVList);
        (pkIVList[5], pkIVList[3], pkIVList[4]) = (pkIVList[3], pkIVList[4], pkIVList[5]);

        for (int i = 0; i < 6; i++)
        {
            if (targetminIVs[i] > pkIVList[i] || targetmaxIVs[i] < pkIVList[i])
                return false;
        }
        return true;
    }

    public static string GetMarkName(IRibbonIndex pk)
    {
        for (var mark = RibbonIndex.MarkLunchtime; mark <= RibbonIndex.MarkSlump; mark++)
        {
            if (pk.GetRibbon((int)mark))
                return GameInfo.Strings.Ribbons.GetName($"Ribbon{mark}");
        }
        return "";
    }

    public static string GetPrintName(PKM pk)
    {
        var set = ShowdownParsing.GetShowdownText(pk);
        if (pk is IRibbonIndex r)
        {
            var rstring = GetMarkName(r);
            if (!string.IsNullOrEmpty(rstring))
                set += $"\n找到的宝可梦具有**{GetMarkName(r)}**！";
        }
        return set;
    }

    public static void InitializeTargetIVs(PokeTradeHubConfig config, out int[] min, out int[] max)
    {
        min = ReadTargetIVs(config.StopConditions, true);
        max = ReadTargetIVs(config.StopConditions, false);
    }

    public static void ReadUnwantedMarks(StopConditionSettings settings, out IReadOnlyList<string> marks) =>
        marks = settings.UnwantedMarks.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

    public virtual bool IsUnwantedMark(string mark, IReadOnlyList<string> marklist) => marklist.Contains(mark);

    public override string ToString() => "停止条件设置";

    private static bool HasMark(IRibbonIndex pk)
    {
        for (var mark = RibbonIndex.MarkLunchtime; mark <= RibbonIndex.MarkSlump; mark++)
        {
            if (pk.GetRibbon((int)mark))
                return true;
        }
        return false;
    }

    private static int[] ReadTargetIVs(StopConditionSettings settings, bool min)
    {
        int[] targetIVs = new int[6];
        char[] split = ['/'];

        string[] splitIVs = min
            ? settings.TargetMinIVs.Split(split, StringSplitOptions.RemoveEmptyEntries)
            : settings.TargetMaxIVs.Split(split, StringSplitOptions.RemoveEmptyEntries);

        // 最多接受6个值。如果未提供6个值，则用默认值填充。
        // 非整数的任何值都将作为通配符。
        for (int i = 0; i < 6; i++)
        {
            if (i < splitIVs.Length)
            {
                var str = splitIVs[i];
                if (int.TryParse(str, out var val))
                {
                    targetIVs[i] = val;
                    continue;
                }
            }
            targetIVs[i] = min ? 0 : 31;
        }
        return targetIVs;
    }
}

public enum TargetShinyType
{
    DisableOption,  // 不关心

    NonShiny,       // 仅匹配非闪光

    AnyShiny,       // 匹配任何闪光，无论类型

    StarOnly,       // 仅匹配星星闪光

    SquareOnly,     // 仅匹配方块闪光
}
