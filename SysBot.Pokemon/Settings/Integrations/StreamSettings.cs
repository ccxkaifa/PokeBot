using PKHeX.Core;
using SysBot.Base;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace SysBot.Pokemon;
public class StreamSettings
{
    private const string Operation = nameof(Operation);
    private static readonly byte[] BlackPixel = // 1x1 黑色像素
    [
        0x42, 0x4D, 0x3A, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x36, 0x00, 0x00, 0x00, 0x28, 0x00,
        0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00,
        0x00, 0x00, 0x01, 0x00, 0x18, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x00, 0x00,
    ];
    public static Action<PKM, string>? CreateSpriteFile { get; set; }

    [Category(Operation), Description("已完成交易的显示格式。{0} = 计数")]
    [DisplayName("已完成交易显示格式")]
    public string CompletedTradesFormat { get; set; } = "已完成交易数：{0}";

    [Category(Operation), Description("如果交易区块文件存在则复制，否则复制占位图。")]
    [DisplayName("复制交易区块图片")]
    public bool CopyImageFile { get; set; } = true;

    [Category(Operation), Description("生成直播资源；关闭后将停止生成资源。")]
    [DisplayName("启用直播资源生成")]
    public bool CreateAssets { get; set; }

    [Category(Operation), Description("新交易开始时，创建文件标识已完成交易数。")]
    [DisplayName("生成已完成交易数文件")]
    public bool CreateCompletedTrades { get; set; } = true;

    [Category(Operation), Description("创建文件列出用户加入队列后的预计等待时间。")]
    [DisplayName("生成预计等待时间文件")]
    public bool CreateEstimatedTime { get; set; } = true;

    [Category(Operation), Description("生成当前待处理用户列表（第一组）。")]
    [DisplayName("生成待处理列表（第一组）")]
    public bool CreateOnDeck { get; set; } = true;

    [Category(Operation), Description("生成当前待处理用户列表（第二组）。")]
    [DisplayName("生成待处理列表（第二组）")]
    public bool CreateOnDeck2 { get; set; } = true;

    [Category(Operation), Description("生成交易开始详情，标识机器人正在与谁交易。")]
    [DisplayName("生成交易对象详情")]
    public bool CreateTradeStart { get; set; } = true;

    [Category(Operation), Description("生成交易开始详情，标识机器人正在交易的内容。")]
    [DisplayName("生成交易内容精灵图")]
    public bool CreateTradeStartSprite { get; set; } = true;

    [Category(Operation), Description("生成当前正在交易的用户列表。")]
    [DisplayName("生成交易中用户列表")]
    public bool CreateUserList { get; set; } = true;

    [Category(Operation), Description("创建文件标识队列中的用户数。")]
    [DisplayName("生成队列用户数文件")]
    public bool CreateUsersInQueue { get; set; } = true;

    [Category(Operation), Description("创建文件列出最近退出队列用户的等待时长。")]
    [DisplayName("生成最近用户等待时长文件")]
    public bool CreateWaitedTime { get; set; } = true;

    [Category(Operation), Description("预计完成时间戳的显示格式。")]
    [DisplayName("预计完成时间戳格式")]
    public string EstimatedFulfillmentFormat { get; set; } = @"hh\:mm\:ss";

    // 预计时间
    [Category(Operation), Description("预计等待时间的显示格式。")]
    [DisplayName("预计等待时间显示格式")]
    public string EstimatedTimeFormat { get; set; } = "预计等待时间：{0:F1} 分钟";

    [Category(Operation), Description("待处理列表（第一组）用户的显示格式。{0} = ID，{3} = 用户")]
    [DisplayName("待处理列表（第一组）显示格式")]
    public string OnDeckFormat { get; set; } = "（ID {0}）- {3}";

    [Category(Operation), Description("待处理列表（第二组）用户的显示格式。{0} = ID，{3} = 用户")]
    [DisplayName("待处理列表（第二组）显示格式")]
    public string OnDeckFormat2 { get; set; } = "（ID {0}）- {3}";

    [Category(Operation), Description("待处理列表（第一组）用户的分隔符。")]
    [DisplayName("待处理列表（第一组）分隔符")]
    public string OnDeckSeparator { get; set; } = "\n";

    [Category(Operation), Description("待处理列表（第二组）用户的分隔符。")]
    [DisplayName("待处理列表（第二组）分隔符")]
    public string OnDeckSeparator2 { get; set; } = "\n";

    [Category(Operation), Description("待处理列表（第一组）顶部跳过的用户数。若要隐藏正在处理的用户，设置为你的主机数量即可。")]
    [DisplayName("待处理列表（第一组）顶部跳过数")]
    public int OnDeckSkip { get; set; }

    [Category(Operation), Description("待处理列表（第二组）顶部跳过的用户数。若要隐藏正在处理的用户，设置为你的主机数量即可。")]
    [DisplayName("待处理列表（第二组）顶部跳过数")]
    public int OnDeckSkip2 { get; set; }

    // 待处理列表（第一组）
    [Category(Operation), Description("待处理列表（第一组）显示的用户数。")]
    [DisplayName("待处理列表（第一组）显示数量")]
    public int OnDeckTake { get; set; } = 5;

    // 待处理列表（第二组）
    [Category(Operation), Description("待处理列表（第二组）显示的用户数。")]
    [DisplayName("待处理列表（第二组）显示数量")]
    public int OnDeckTake2 { get; set; } = 5;

    // 交易代码区块
    [Category(Operation), Description("输入交易代码时要复制的图片源文件名。若留空，将创建占位图。")]
    [DisplayName("交易代码区块图片源文件")]
    public string TradeBlockFile { get; set; } = string.Empty;

    [Category(Operation), Description("链接代码屏蔽图片的目标文件名。{0} 将替换为本地IP地址。")]
    [DisplayName("屏蔽图片目标文件名格式")]
    public string TradeBlockFormat { get; set; } = "屏蔽_{0}.png";

    [Category(Operation), Description("当前交易详情的显示格式。{0} = ID，{1} = 用户")]
    [DisplayName("当前交易详情显示格式")]
    public string TrainerTradeStart { get; set; } = "（ID {0}）{1}";

    [Category(Operation), Description("用户列表的显示格式。{0} = ID，{3} = 用户")]
    [DisplayName("用户列表显示格式")]
    public string UserListFormat { get; set; } = "（ID {0}）- {3}";

    [Category(Operation), Description("用户列表的分隔符。")]
    [DisplayName("用户列表分隔符")]
    public string UserListSeparator { get; set; } = "，";

    [Category(Operation), Description("用户列表顶部跳过的用户数。若要隐藏正在处理的用户，设置为你的主机数量即可。")]
    [DisplayName("用户列表顶部跳过数")]
    public int UserListSkip { get; set; }

    // 用户列表
    [Category(Operation), Description("用户列表显示的用户数。")]
    [DisplayName("用户列表显示数量")]
    public int UserListTake { get; set; } = -1;

    // 队列中用户数
    [Category(Operation), Description("队列中用户数的显示格式。{0} = 计数")]
    [DisplayName("队列用户数显示格式")]
    public string UsersInQueueFormat { get; set; } = "队列中用户数：{0}";

    // 等待时长
    [Category(Operation), Description("最近退出队列用户等待时长的显示格式。")]
    [DisplayName("等待时长显示格式")]
    public string WaitedTimeFormat { get; set; } = @"hh\:mm\:ss";

    public void EndEnterCode(PokeRoutineExecutorBase b)
    {
        try
        {
            var file = GetBlockFileName(b);
            if (File.Exists(file))
                File.Delete(file);
        }
        catch (Exception e)
        {
            LogUtil.LogError(e.Message, nameof(StreamSettings));
        }
    }

    public void IdleAssets(PokeRoutineExecutorBase b)
    {
        if (!CreateAssets)
            return;
        try
        {
            foreach (var file in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*", SearchOption.TopDirectoryOnly))
            {
                if (file.Contains(b.Connection.Name))
                    File.Delete(file);
            }
            if (CreateWaitedTime)
                File.WriteAllText("waited.txt", "00:00:00");
            if (CreateEstimatedTime)
            {
                File.WriteAllText("estimatedTime.txt", "预计等待时间：0 分钟");
                File.WriteAllText("estimatedTimestamp.txt", "");
            }
            if (CreateOnDeck)
                File.WriteAllText("ondeck.txt", "等待中...");
            if (CreateOnDeck2)
                File.WriteAllText("ondeck2.txt", "队列为空！");
            if (CreateUserList)
                File.WriteAllText("users.txt", "无");
            if (CreateUsersInQueue)
                File.WriteAllText("queuecount.txt", "队列中用户数：0");
        }
        catch (Exception e)
        {
            LogUtil.LogError(e.Message, nameof(StreamSettings));
        }
    }

    public void StartEnterCode(PokeRoutineExecutorBase b)
    {
        if (!CreateAssets)
            return;
        try
        {
            var file = GetBlockFileName(b);
            if (CopyImageFile && File.Exists(TradeBlockFile))
                File.Copy(TradeBlockFile, file);
            else
                File.WriteAllBytes(file, BlackPixel);
        }
        catch (Exception e)
        {
            LogUtil.LogError(e.Message, nameof(StreamSettings));
        }
    }

    // 已完成交易
    public void StartTrade<T>(PokeRoutineExecutorBase b, PokeTradeDetail<T> detail, PokeTradeHub<T> hub) where T : PKM, new()
    {
        if (!CreateAssets)
            return;
        try
        {
            if (CreateTradeStart)
                GenerateBotConnection(b, detail);
            if (CreateWaitedTime)
                GenerateWaitedTime(detail.Time);
            if (CreateEstimatedTime)
                GenerateEstimatedTime(hub);
            if (CreateUsersInQueue)
                GenerateUsersInQueue(hub.Queues.Info.Count);
            if (CreateOnDeck)
                GenerateOnDeck(hub);
            if (CreateOnDeck2)
                GenerateOnDeck2(hub);
            if (CreateUserList)
                GenerateUserList(hub);
            if (CreateCompletedTrades)
                GenerateCompletedTrades(hub);
            if (CreateTradeStartSprite)
                GenerateBotSprite(b, detail);
        }
        catch (Exception e)
        {
            LogUtil.LogError(e.Message, nameof(StreamSettings));
        }
    }

    public override string ToString() => "直播设置";

    private static void GenerateBotSprite<T>(PokeRoutineExecutorBase b, PokeTradeDetail<T> detail) where T : PKM, new()
    {
        var func = CreateSpriteFile;
        if (func == null)
            return;
        var file = b.Connection.Name;
        var pk = detail.TradeData;
        func.Invoke(pk, $"sprite_{file}.png");
    }

    private void GenerateBotConnection<T>(PokeRoutineExecutorBase b, PokeTradeDetail<T> detail) where T : PKM, new()
    {
        var file = b.Connection.Name;
        var name = string.Format(TrainerTradeStart, detail.ID, detail.Trainer.TrainerName, (Species)detail.TradeData.Species);
        File.WriteAllText($"{file}.txt", name);
    }

    private void GenerateCompletedTrades<T>(PokeTradeHub<T> hub) where T : PKM, new()
    {
        var msg = string.Format(CompletedTradesFormat, hub.Config.Trade.CountStatsSettings.CompletedTrades);
        File.WriteAllText("completed.txt", msg);
    }

    private void GenerateEstimatedTime<T>(PokeTradeHub<T> hub) where T : PKM, new()
    {
        var count = hub.Queues.Info.Count;
        var estimate = hub.Config.Queues.EstimateDelay(count, hub.Bots.Count);
        // 分钟数
        var wait = string.Format(EstimatedTimeFormat, estimate);
        File.WriteAllText("estimatedTime.txt", wait);
        // 预计完成时间
        var now = DateTime.Now;
        var difference = now.AddMinutes(estimate);
        var date = difference.ToString(EstimatedFulfillmentFormat);
        File.WriteAllText("estimatedTimestamp.txt", date);
    }

    private void GenerateOnDeck<T>(PokeTradeHub<T> hub) where T : PKM, new()
    {
        var ondeck = hub.Queues.Info.GetUserList(OnDeckFormat);
        ondeck = ondeck.Skip(OnDeckSkip).Take(OnDeckTake); // 筛选
        File.WriteAllText("ondeck.txt", string.Join(OnDeckSeparator, ondeck));
    }

    private void GenerateOnDeck2<T>(PokeTradeHub<T> hub) where T : PKM, new()
    {
        var ondeck = hub.Queues.Info.GetUserList(OnDeckFormat2);
        ondeck = ondeck.Skip(OnDeckSkip2).Take(OnDeckTake2); // 筛选
        File.WriteAllText("ondeck2.txt", string.Join(OnDeckSeparator2, ondeck));
    }

    private void GenerateUserList<T>(PokeTradeHub<T> hub) where T : PKM, new()
    {
        var users = hub.Queues.Info.GetUserList(UserListFormat);
        users = users.Skip(UserListSkip);
        if (UserListTake > 0)
            users = users.Take(UserListTake); // 筛选
        File.WriteAllText("users.txt", string.Join(UserListSeparator, users));
    }

    private void GenerateUsersInQueue(int count)
    {
        var value = string.Format(UsersInQueueFormat, count);
        File.WriteAllText("queuecount.txt", value);
    }

    private void GenerateWaitedTime(DateTime time)
    {
        var now = DateTime.Now;
        var difference = now - time;
        var value = difference.ToString(WaitedTimeFormat);
        File.WriteAllText("waited.txt", value);
    }

    private string GetBlockFileName(PokeRoutineExecutorBase b) => string.Format(TradeBlockFormat, b.Connection.Name);
}
