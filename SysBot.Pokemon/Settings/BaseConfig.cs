using System.ComponentModel;

namespace SysBot.Pokemon;

/// <summary>
/// Console agnostic settings
/// </summary>
public abstract class BaseConfig
{
    protected const string FeatureToggle = nameof(FeatureToggle);

    protected const string Operation = nameof(Operation);

    [Browsable(false)]
    private const string Debug = nameof(Debug);

    [Category(FeatureToggle), Description("启用后，机器人在未处理任何事务时会偶尔按下 B 按钮（以避免进入休眠状态）.")]
    [DisplayName("是否启用防睡眠")]
    public bool AntiIdle { get; set; }

    [Category(Operation)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public FolderSettings Folder { get; set; } = new();

    [Category(Operation)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public LegalitySettings Legality { get; set; } = new();

    [Category(FeatureToggle), Description("启用文本日志。重启以应用更改.")]
    [DisplayName("是否启用日志")]
    public bool LoggingEnabled { get; set; } = true;

    [Category(FeatureToggle), Description("要保留的旧文本日志文件的最大数量。将此设置为≤0 可禁用日志清理。重启以应用更改.")]
    [DisplayName("最大日志文件数")]
    public int MaxArchiveFiles { get; set; } = 14;

    public abstract bool Shuffled { get; }

    [Browsable(false)]
    [Category(Debug), Description("程序启动时跳过创建机器人；这对测试集成很有帮助.")]
    public bool SkipConsoleBotCreation { get; set; }

    [Category(FeatureToggle), Description("启用后，机器人将通过键盘输入链接交易代码（速度更快）.")]
    [DisplayName("是否使用键盘输入")]
    public bool UseKeyboard { get; set; } = true;
}
