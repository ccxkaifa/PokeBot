using PKHeX.Core;
using SysBot.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace SysBot.Pokemon;

public class TradeSettings : IBotStateSettings, ICountSettings
{
    private const string CountStats = nameof(CountStats);

    private const string HOMELegality = nameof(HOMELegality);

    private const string TradeConfig = nameof(TradeConfig);

    private const string VGCPastesConfig = nameof(VGCPastesConfig);

    private const string Miscellaneous = nameof(Miscellaneous);

    private const string RequestFolders = nameof(RequestFolders);

    private const string EmbedSettings = nameof(EmbedSettings);

    public override string ToString() => "交易配置设置";

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EmojiInfo
    {
        [Description("这个表情符号的完整字符串.")]
        public string EmojiString { get; set; } = string.Empty;

        public override string ToString()
        {
            return string.IsNullOrEmpty(EmojiString) ? "Not Set" : EmojiString;
        }
    }

    [Category(TradeConfig), Description("与交易配置相关的设置."), DisplayName("交易配置"), Browsable(true)]
    public TradeSettingsCategory TradeConfiguration { get; set; } = new();

    [Category(EmbedSettings), Description("与 Discord 中的交易嵌入相关的设置."), DisplayName("交易嵌入设置"), Browsable(true)]
    public TradeEmbedSettingsCategory TradeEmbedSettings { get; set; } = new();

    [Category(RequestFolders), Description("与请求文件夹相关的设置."), DisplayName("请求文件夹设置"), Browsable(true)]
    public RequestFolderSettingsCategory RequestFolderSettings { get; set; } = new();

    [Category(CountStats), Description("与交易次数统计相关的设置."), DisplayName("交易计数统计设置"), Browsable(true)]
    public CountStatsSettingsCategory CountStatsSettings { get; set; } = new();

    [Category(TradeConfig), TypeConverter(typeof(CategoryConverter<TradeSettingsCategory>))]
    public class TradeSettingsCategory
    {
        public override string ToString() => "交易配置";

        [Category(TradeConfig), Description("最小链接交换代码."), DisplayName("最小链接交换代码")]
        public int MinTradeCode { get; set; } = 0;

        [Category(TradeConfig), Description("最大链接交换代码."), DisplayName("最大链接交换代码")]
        public int MaxTradeCode { get; set; } = 9999_9999;

        [Category(TradeConfig), Description("如果设置为 True,Discord 用户的交易代码将被存储并重复使用,不会更改."), DisplayName("存储和重复使用交易代码")]
        public bool StoreTradeCodes { get; set; } = true;

        [Category(TradeConfig), Description("等待交易伙伴的时间（以秒为单位）."), DisplayName("交易伙伴等待时间（秒）")]
        public int TradeWaitTime { get; set; } = 30;

        [Category(TradeConfig), Description("按下A键等待交易处理的最长时间（以秒为单位）."), DisplayName("最大交易确认时间（秒）")]
        public int MaxTradeConfirmTime { get; set; } = 25;

        [Category(TradeConfig), Description("如果配置了,则使用ItemTrade物种."), DisplayName("物品交易的默认物种")]
        public Species ItemTradeSpecies { get; set; } = Species.None;

        [Category(TradeConfig), Description("如果未指定任何物品，则发送默认携带物品."), DisplayName("交易的默认持有物品")]
        public HeldItem DefaultHeldItem { get; set; } = HeldItem.None;

        [Category(TradeConfig), Description("如果设置为 “True”,每只有效的宝可梦都会自带所有建议的可回忆技能,无需使用批量命令."), DisplayName("默认建议可重新学习的招式")]
        public bool SuggestRelearnMoves { get; set; } = true;

        [Category(TradeConfig), Description("切换以允许或禁止批量交易."), DisplayName("允许批量交易")]
        public bool AllowBatchTrades { get; set; } = true;

        [Category(TradeConfig), Description("检查昵称和原训练者（OT）是否有垃圾信息."), DisplayName("启用垃圾检查")]
        public bool EnableSpamCheck { get; set; } = true;

        [Category(TradeConfig), Description("单次交易的最大宝可梦数量.如果此配置小于该数量,批量模式将关闭."), DisplayName("每次交易的最大宝可梦数量")]
        public int MaxPkmsPerTrade { get; set; } = 1;

        [Category(TradeConfig), Description("导出交易：导出例程将在单个用户的最大转储次数后停止."), DisplayName("每笔交易的最大转储量")]
        public int MaxDumpsPerTrade { get; set; } = 20;

        [Category(TradeConfig), Description("导出交易：在交易中花费x秒后,带出例程将停止."), DisplayName("最大转储交易时间（秒）")]
        public int MaxDumpTradeTime { get; set; } = 45;

        [Category(TradeConfig), Description("导出交易：如果启用，导出例程将向用户输出合法性检查信息."), DisplayName("转储交易合法性检查")]
        public bool DumpTradeLegalityCheck { get; set; } = true;

        [Category(TradeConfig), Description("LGPE设置.")]
        public int TradeAnimationMaxDelaySeconds = 25;

        public enum HeldItem
        {
            None = 0,

            MasterBall = 1,

            RareCandy = 50,

            ppUp = 51,

            ppMax = 53,

            BigPearl = 89,

            Nugget = 92,

            AbilityCapsule = 645,

            BottleCap = 795,

            GoldBottleCap = 796,

            expCandyL = 1127,

            expCandyXL = 1128,

            AbilityPatch = 1606,

            FreshStartMochi = 2479,
        }
    }

    [Category(EmbedSettings), TypeConverter(typeof(CategoryConverter<TradeEmbedSettingsCategory>))]
    public class TradeEmbedSettingsCategory
    {
        public override string ToString() => "交易嵌入配置设置";

        private bool _useEmbeds = true;

        [Category(EmbedSettings), Description("如果为True时,将在你的 Discord 交易频道中显示精美的嵌入内容,展示用户正在交易的物品.如果为False,则会显示默认文本."), DisplayName("使用嵌入")]
        public bool UseEmbeds
        {
            get => _useEmbeds;
            set
            {
                _useEmbeds = value;
                OnUseEmbedsChanged();
            }
        }

        private void OnUseEmbedsChanged()
        {
            if (!_useEmbeds)
            {
                PreferredImageSize = ImageSize.Size256x256;
                MoveTypeEmojis = false;
                ShowScale = false;
                ShowTeraType = false;
                ShowLevel = false;
                ShowMetDate = false;
                ShowAbility = false;
                ShowNature = false;
                ShowIVs = false;
            }
        }

        [Category(EmbedSettings), Description("嵌入的首选物种图片尺寸."), DisplayName("物种图像尺寸")]
        public ImageSize PreferredImageSize { get; set; } = ImageSize.Size256x256;

        [Category(EmbedSettings), Description("将在交易嵌入内容中的招式旁显示招式类型图标（仅适用于 Discord）.这需要用户将表情符号上传到他们的服务器."), DisplayName("显示招式类型表情符号")]
        public bool MoveTypeEmojis { get; set; } = true;

        [Category(EmbedSettings), Description("招式类型的自定义表情符合信息."), DisplayName("自定义类型表情符号")]
        public List<MoveTypeEmojiInfo> CustomTypeEmojis { get; set; } =
        [
            new(MoveType.Bug),
            new(MoveType.Fire),
            new(MoveType.Flying),
            new(MoveType.Ground),
            new(MoveType.Water),
            new(MoveType.Grass),
            new(MoveType.Ice),
            new(MoveType.Rock),
            new(MoveType.Ghost),
            new(MoveType.Steel),
            new(MoveType.Fighting),
            new(MoveType.Electric),
            new(MoveType.Dragon),
            new(MoveType.Psychic),
            new(MoveType.Dark),
            new(MoveType.Normal),
            new(MoveType.Poison),
            new(MoveType.Fairy),
            new(MoveType.Stellar)
        ];

        [Category(EmbedSettings), Description("雄性性别表情符号的完整字符串."), DisplayName("雄性表情符号")]
        public EmojiInfo MaleEmoji { get; set; } = new EmojiInfo();

        [Category(EmbedSettings), Description("雌性性别的表情符号的完整字符串."), DisplayName("雌性表情符号")]
        public EmojiInfo FemaleEmoji { get; set; } = new EmojiInfo();

        [Category(EmbedSettings), Description("用于显示神秘礼物状态的表情符号信息."), DisplayName("神秘礼物表情符号")]
        public EmojiInfo MysteryGiftEmoji { get; set; } = new EmojiInfo();

        [Category(EmbedSettings), Description("用于显示Alpha标记的表情符号信息."), DisplayName("Alpha标记表情符号")]
        public EmojiInfo AlphaMarkEmoji { get; set; } = new EmojiInfo();

        [Category(EmbedSettings), Description("用于显示最强标记的表情符号信息."), DisplayName("最强标记表情符号")]
        public EmojiInfo MightiestMarkEmoji { get; set; } = new EmojiInfo();

        [Category(EmbedSettings), Description("《宝可梦传说：阿尔宙斯》中用于显示Alpha宝可梦图标的表情符号信息."), DisplayName("Alpha PLA表情符号 ")]
        public EmojiInfo AlphaPLAEmoji { get; set; } = new EmojiInfo();

        [Category(EmbedSettings), Description("将在交易嵌入内容中的招式旁显示招式类型图标（仅适用于 Discord）。要求用户将表情符号上传到他们的服务器."), DisplayName("显示太晶化属性表情符号?")]
        public bool UseTeraEmojis { get; set; } = true;

        [Category(EmbedSettings), Description("太晶属性的太晶属性表情符号信息."), DisplayName("自定义太晶属性表情符号")]
        public List<TeraTypeEmojiInfo> TeraTypeEmojis { get; set; } =
        [
            new(MoveType.Bug),
            new(MoveType.Fire),
            new(MoveType.Flying),
            new(MoveType.Ground),
            new(MoveType.Water),
            new(MoveType.Grass),
            new(MoveType.Ice),
            new(MoveType.Rock),
            new(MoveType.Ghost),
            new(MoveType.Steel),
            new(MoveType.Fighting),
            new(MoveType.Electric),
            new(MoveType.Dragon),
            new(MoveType.Psychic),
            new(MoveType.Dark),
            new(MoveType.Normal),
            new(MoveType.Poison),
            new(MoveType.Fairy),
            new(MoveType.Stellar)
        ];

        [Category(EmbedSettings), Description("将在交易嵌入中显示比例（仅适用于 SV 和 Discord）,需要用户将表情符号上传到他们的服务器."), DisplayName("显示比例")]
        public bool ShowScale { get; set; } = true;

        [Category(EmbedSettings), Description("将在交易嵌入中显示太晶属性 (仅适用于 SV 和 Discord)."), DisplayName("显示太晶属性")]
        public bool ShowTeraType { get; set; } = true;

        [Category(EmbedSettings), Description("将在交易嵌入中显示等级（仅适用于 Discord）."), DisplayName("显示等级")]
        public bool ShowLevel { get; set; } = true;

        [Category(EmbedSettings), Description("将在交易嵌入中显示 MetDate（仅适用于 Discord）."), DisplayName("显示Met日期")]
        public bool ShowMetDate { get; set; } = true;

        [Category(EmbedSettings), Description("将显示交易嵌入特性（仅适用于 Discord）."), DisplayName("显示特性")]
        public bool ShowAbility { get; set; } = true;

        [Category(EmbedSettings), Description("将在交易嵌入中显示性格（仅适用于 Discord）."), DisplayName("显示性格")]
        public bool ShowNature { get; set; } = true;

        [Category(EmbedSettings), Description("将在交易嵌入中显示PKM语言（仅适用于 Discord）."), DisplayName("显示语言")]
        public bool ShowLanguage { get; set; } = true;

        [Category(EmbedSettings), Description("将在交易嵌入中显示个体值（仅适用于 Discord）."), DisplayName("显示个体值")]
        public bool ShowIVs { get; set; } = true;

        [Category(EmbedSettings), Description("将在交易嵌入中显示努力值（仅限 Discord）."), DisplayName("显示努力值")]
        public bool ShowEVs { get; set; } = true;
    }

    [Category(RequestFolders), TypeConverter(typeof(CategoryConverter<RequestFolderSettingsCategory>))]
    public class RequestFolderSettingsCategory
    {
        public override string ToString() => "请求文件夹设置";

        [Category("RequestFolders"), Description("路径到您的活动文件夹.创建一个名为 “events” 的新文件夹,并将路径复制到此处."), DisplayName("活动文件夹路径")]
        public string EventsFolder { get; set; } = string.Empty;

        [Category("RequestFolders"), Description("路径到您的BattleReady文件夹.创建一个名为 “battleready” 的新文件夹并将路径复制到此处."), DisplayName("对战准备文件夹路径")]
        public string BattleReadyPKMFolder { get; set; } = string.Empty;
    }

    [Category(Miscellaneous)]
    [Description("在交易期间关闭 Switch 的屏幕")]
    [DisplayName("关闭屏幕")]
    public bool ScreenOff { get; set; } = false;

    /// <summary>
    /// Gets a random trade code based on the range settings.
    /// </summary>
    public int GetRandomTradeCode() => Util.Rand.Next(TradeConfiguration.MinTradeCode, TradeConfiguration.MaxTradeCode + 1);

    public static List<Pictocodes> GetRandomLGTradeCode(bool randomtrade = false)
    {
        var lgcode = new List<Pictocodes>();
        if (randomtrade)
        {
            for (int i = 0; i <= 2; i++)
            {
                // code.Add((pictocodes)Util.Rand.Next(10));
                lgcode.Add(Pictocodes.Pikachu);
            }
        }
        else
        {
            for (int i = 0; i <= 2; i++)
            {
                lgcode.Add((Pictocodes)Util.Rand.Next(10));

                // code.Add(pictocodes.Pikachu);
            }
        }
        return lgcode;
    }

    [Category(CountStats), TypeConverter(typeof(CategoryConverter<CountStatsSettingsCategory>))]
    public class CountStatsSettingsCategory
    {
        public override string ToString() => "交易次数统计";

        private int _completedSurprise;

        private int _completedDistribution;

        private int _completedTrades;

        private int _completedSeedChecks;

        private int _completedClones;

        private int _completedDumps;

        private int _completedFixOTs;

        [Category(CountStats), Description("已完成的惊喜交换次数")]
        [DisplayName("已完成的惊喜交换")]
        public int CompletedSurprise
        {
            get => _completedSurprise;
            set => _completedSurprise = value;
        }

        [Category(), Description("已完成的链接交易（分发）次数")]
        [DisplayName("已完成的分发交易")]
        public int CompletedDistribution
        {
            get => _completedDistribution;
            set => _completedDistribution = value;
        }

        [Category(CountStats), Description("已完成的链接交易（特定用户）次数")]
        [DisplayName("已完成的链接交易（特定用户）")]

        public int CompletedTrades
        {
            get => _completedTrades;
            set => _completedTrades = value;
        }

        [Category(CountStats), Description("已完成的修复OT交易（特定用户）次数")]
        [DisplayName("已完成的修复OT交易（特定用户）")]
        public int CompletedFixOTs
        {
            get => _completedFixOTs;
            set => _completedFixOTs = value;
        }

        [Browsable(false)]
        [Category(CountStats), Description("已完成的种子检查交易次数")]
        [DisplayName("已完成的种子检查交易")]
        public int CompletedSeedChecks
        {
            get => _completedSeedChecks;
            set => _completedSeedChecks = value;
        }

        [Category(CountStats), Description("已完成的克隆交易（特定用户）次数")]
        [DisplayName("已完成的克隆交易（特定用户）")]

        public int CompletedClones
        {
            get => _completedClones;
            set => _completedClones = value;
        }

        [Category(CountStats), Description("已完成的导出交易（特定用户）次数")]
        [DisplayName("已完成的导出交易（特定用户）")]
        public int CompletedDumps
        {
            get => _completedDumps;
            set => _completedDumps = value;
        }

        [Category(CountStats), Description("启用后,当请求状态检查时将发送计数.")]
        [DisplayName("在状态检查中发生计数）")]
        public bool EmitCountsOnStatusCheck { get; set; }

        public void AddCompletedTrade() => Interlocked.Increment(ref _completedTrades);

        public void AddCompletedSeedCheck() => Interlocked.Increment(ref _completedSeedChecks);

        public void AddCompletedSurprise() => Interlocked.Increment(ref _completedSurprise);

        public void AddCompletedDistribution() => Interlocked.Increment(ref _completedDistribution);

        public void AddCompletedDumps() => Interlocked.Increment(ref _completedDumps);

        public void AddCompletedClones() => Interlocked.Increment(ref _completedClones);

        public void AddCompletedFixOTs() => Interlocked.Increment(ref _completedFixOTs);

        public IEnumerable<string> GetNonZeroCounts()
        {
            if (!EmitCountsOnStatusCheck)
                yield break;
            if (CompletedSeedChecks != 0)
                yield return $"Seed Check Trades: {CompletedSeedChecks}";
            if (CompletedClones != 0)
                yield return $"Clone Trades: {CompletedClones}";
            if (CompletedDumps != 0)
                yield return $"Dump Trades: {CompletedDumps}";
            if (CompletedTrades != 0)
                yield return $"Link Trades: {CompletedTrades}";
            if (CompletedDistribution != 0)
                yield return $"Distribution Trades: {CompletedDistribution}";
            if (CompletedFixOTs != 0)
                yield return $"FixOT Trades: {CompletedFixOTs}";
            if (CompletedSurprise != 0)
                yield return $"Surprise Trades: {CompletedSurprise}";
        }
    }

    public bool EmitCountsOnStatusCheck
    {
        get => CountStatsSettings.EmitCountsOnStatusCheck;
        set => CountStatsSettings.EmitCountsOnStatusCheck = value;
    }

    public IEnumerable<string> GetNonZeroCounts()
    {
        // Delegating the call to CountStatsSettingsCategory
        return CountStatsSettings.GetNonZeroCounts();
    }

    public class CategoryConverter<T> : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes) => TypeDescriptor.GetProperties(typeof(T));

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) => destinationType != typeof(string) && base.CanConvertTo(context, destinationType);
    }

    public enum ImageSize
    {
        Size256x256,

        Size128x128
    }

    public enum MoveType
    {
        Normal,
        Fighting,
        Flying,
        Poison,
        Ground,
        Rock,
        Bug,
        Ghost,
        Steel,
        Fire,
        Water,
        Grass,
        Electric,
        Psychic,
        Ice,
        Dragon,
        Dark,
        Fairy,
        Stellar
    }

    public class MoveTypeEmojiInfo
    {
        [Description("The type of move.")]
        public MoveType MoveType { get; set; }
        [Description("The Discord emoji string for this move type.")]
        public string EmojiCode { get; set; } = string.Empty;
        public MoveTypeEmojiInfo()
        { }
        public MoveTypeEmojiInfo(MoveType moveType)
        {
            MoveType = moveType;
            EmojiCode = string.Empty;
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(EmojiCode))
                return MoveType.ToString();
            return $"{EmojiCode}";
        }
    }

    public class TeraTypeEmojiInfo
    {
        [Description("The Tera Type.")]
        public MoveType MoveType { get; set; }
        [Description("The Discord emoji string for this tera type.")]
        public string EmojiCode { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TeraTypeEmojiInfo()
        { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TeraTypeEmojiInfo(MoveType teraType)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            MoveType = teraType;
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(EmojiCode))
                return MoveType.ToString();
            return $"{EmojiCode}";
        }
    }
}
