using System;
using System.ComponentModel;
using System.Linq;

namespace SysBot.Pokemon;

public class YouTubeSettings
{
    private const string Messages = nameof(Messages);

    private const string Operation = nameof(Operation);

    private const string Startup = nameof(Startup);

    [Category(Startup), Description("用于发送消息的频道ID")]
    [DisplayName("频道ID设置")]

    public string ChannelID { get; set; } = string.Empty;

    [Category(Startup), Description("机器人客户端ID")]
    [DisplayName("机器人ID")]

    public string ClientID { get; set; } = string.Empty;

    // Startup
    [Category(Startup), Description("机器人客户端密钥")]
    [DisplayName("客户端密钥")]

    public string ClientSecret { get; set; } = string.Empty;

    [Category(Startup), Description("机器人命令前缀")]
    [DisplayName("指令前缀")]

    public char CommandPrefix { get; set; } = '$';

    [Category(Operation), Description("屏障解除时发送的消息.")]
    [DisplayName("屏障解除消息")]
    public string MessageStart { get; set; } = string.Empty;

    [Category(Operation), Description("超级权限用户")]
    [DisplayName("超级用户")]

    public string SudoList { get; set; } = string.Empty;

    // Operation
    [Category(Operation), Description("此列表中的用户ID无法使用机器人.")]
    [DisplayName("用户黑名单")]
    public string UserBlacklist { get; set; } = string.Empty;

    public bool IsSudo(string username)
    {
        var sudos = SudoList.Split([",", ", ", " "], StringSplitOptions.RemoveEmptyEntries);
        return sudos.Contains(username);
    }

    public override string ToString() => "YouTube 整合设置";
}

public enum YouTubeMessageDestination
{
    Disabled,

    Channel,
}
