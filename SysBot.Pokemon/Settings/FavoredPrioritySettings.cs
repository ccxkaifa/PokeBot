using System;
using System.ComponentModel;

namespace SysBot.Pokemon;

/// <summary>
/// Settings for priority user favoritism in the trade queue.
/// Priority users can skip ahead of regular users while ensuring regular users still get processed.
/// </summary>
public class FavoredPrioritySettings : IFavoredCPQSetting
{
    private const int MinSkipPercentage = 0;
    private const int MaxSkipPercentage = 100;
    private const int MinRegularUsers = 0;

    private const string Configure = nameof(Configure);
    private const string Operation = nameof(Operation);

    private int _skipPercentage = 50;
    private int _minimumRegularUsersFirst = 3;

    [Category(Operation), Description("启用或禁用优先用户偏袒机制.禁用时,所有用户将被平等对待.")]
    [DisplayName("是否启用优待")]
    public bool EnableFavoritism { get; set; } = true;

    [Category(Configure), Description("优先用户可跳过的普通用户比例(0-100).例如：50% 意味着优先用户会排在队列中普通用户的中间位置.比例越高,对优先用户越有利.")]
    [DisplayName("可跳过比例")]
    public int SkipPercentage
    {
        get => _skipPercentage;
        set => _skipPercentage = Math.Clamp(value, MinSkipPercentage, MaxSkipPercentage);
    }

    [Category(Configure), Description("在任何优先用户可以插队之前,必须处理的普通用户的最小数量.这可以防止优先用户完全阻挡普通用户,即便是在长队列中也是如此.")]
    [DisplayName("最小非优先用户数量")]
    public int MinimumRegularUsersFirst
    {
        get => _minimumRegularUsersFirst;
        set => _minimumRegularUsersFirst = Math.Max(MinRegularUsers, value);
    }

    public override string ToString() => "优先待遇设置";
}
