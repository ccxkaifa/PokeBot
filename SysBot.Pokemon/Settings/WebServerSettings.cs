using System.ComponentModel;

namespace SysBot.Pokemon;

/// <summary>
/// Settings for the Web Control Panel server
/// </summary>
public sealed class WebServerSettings
{
    private const string WebServer = nameof(WebServer);
    
    [Category(WebServer)]
    [Description("机器人控制面板网页界面的端口号.默认值为8080.")]
    [DisplayName("控制面板端口")]
    public int ControlPanelPort { get; set; } = 8080;
    
    [Category(WebServer)]
    [Description("启用或禁用Web控制面板.禁用时,Web界面将无法访问.")]
    [DisplayName("是否启用Web服务器")]
    public bool EnableWebServer { get; set; } = true;
    
    [Category(WebServer)]
    [Description("允许外部连接到 Web 控制面板，当为 false 时，仅允许本地主机连接.")]
    [DisplayName("是否允许外部连接")]
    public bool AllowExternalConnections { get; set; } = false;
}
