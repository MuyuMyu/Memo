using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Common.Events
{

    /// <summary>
    /// 事件聚合器模式：
    /// 发布者：发布一个 UpdateLoadingEvent，并传递 UpdateModel 作为负载。
    /// 订阅者：订阅该事件，以便在接收到通知时执行相应的操作（如更新 UI 状态）。
    /// </summary>


    // 定义消息模型类，用于封装传递的消息数据
    public class MessageModel
    {
        // 过滤器属性，用于消息的筛选
        public string Filter { get; set; }
        // 实际消息内容
        public string Message { get; set; }
    }

    /// <summary>
    /// 事件聚合器模式（Event Aggregator）
    /// 定义一个事件类，继承自 PubSubEvent<T>，使用 MessageModel 作为事件数据类型
    /// </summary>
    public class MessageEvent : PubSubEvent<MessageModel>
    {
    }
}


