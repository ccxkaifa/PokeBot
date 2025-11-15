using System.ComponentModel;
using System.IO;

namespace SysBot.Pokemon;

public class FolderSettings : IDumper
{
    private const string FeatureToggle = nameof(FeatureToggle);

    private const string Files = nameof(Files);

    [Category(Files), Description("源文件夹：从中选择要分发的 PKM 文件的位置.")]
    [DisplayName("分发文件夹")]
    public string DistributeFolder { get; set; } = string.Empty;

    [Category(FeatureToggle), Description("功能开关 - 启用后,会将接收到的所有PKM文件（交易结果）转存到DumpFolder文件夹中.")]
    [DisplayName("是否转储交易宝可梦")]
    public bool Dump { get; set; }

    [Category(Files), Description("目标文件夹：所有接收的 PKM 文件都将被转存到此处.")]

    [DisplayName("转储文件夹")]
    public string DumpFolder { get; set; } = string.Empty;

    public void CreateDefaults(string path)
    {
        var dump = Path.Combine(path, "dump");
        Directory.CreateDirectory(dump);
        DumpFolder = dump;
        Dump = true;

        var distribute = Path.Combine(path, "distribute");
        Directory.CreateDirectory(distribute);
        DistributeFolder = distribute;
    }

    public override string ToString() => "文件夹 / 转储设置";
}

