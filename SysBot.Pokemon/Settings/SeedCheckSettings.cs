using System.ComponentModel;

namespace SysBot.Pokemon;

public class SeedCheckSettings
{
    private const string FeatureToggle = nameof(FeatureToggle);

    [Category(FeatureToggle), Description("允许仅返回最近的闪光帧、第一个普通闪光帧和方形闪光帧，或前三个闪光帧。")]
    public SeedCheckResults ResultDisplayMode { get; set; }

    [Category(FeatureToggle), Description("启用后，种子检查将返回所有可能的种子结果，而非第一个有效匹配项。")]
    public bool ShowAllZ3Results { get; set; }

    public override string ToString() => "种子检查设置";
}

public enum SeedCheckResults
{
    ClosestOnly,            // 仅获取第一个闪光帧

    FirstStarAndSquare,     // 获取第一个普通闪光帧和第一个方形闪光帧

    FirstThree,             // 获取前三个闪光帧
}
