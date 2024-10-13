using Prism.Events;
using Prism.Services.Dialogs;
using System.Threading.Tasks;
using Memo.Common;
using Memo.Common.Events;
using System;

namespace Memo.Extensions
{
    public static class DialogExtensions
    {
        /// <summary>
        /// 询问窗口
        /// </summary>
        /// <param name="dialogHost">指定的DialogHost会话主机</param>
        /// <param name="title">标题</param>
        /// <param name="content">询问内容</param>
        /// <param name="dialogHostName">会话主机名称(唯一)</param>
        /// <returns></returns>
        public static async Task<IDialogResult> Question(this IDialogHostService dialogHost,
            string title, string content, string dialogHostName = "Root"
            )
        {
            // 创建对话框参数
            DialogParameters param = new DialogParameters();
            param.Add("Title", title); // 添加标题参数
            param.Add("Content", content);// 添加内容参数
            param.Add("dialogHostName", dialogHostName); // 添加对话框宿主名称参数

            // 显示对话框并等待结果
            var dialogResult = await dialogHost.ShowDialog("MsgView", param, dialogHostName);
            return dialogResult;// 返回对话框的结果
        }

        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void UpdateLoading(this IEventAggregator aggregator, UpdateModel model)
        {
            // 发布更新加载事件
            aggregator.GetEvent<UpdateLoadingEvent>().Publish(model);
        }

        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void Resgiter(this IEventAggregator aggregator, Action<UpdateModel> action)
        {
            // 订阅更新加载事件
            aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
        }

        /// <summary>
        /// 注册提示消息 
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void ResgiterMessage(this IEventAggregator aggregator,
            Action<MessageModel> action, string filterName = "Main")
        {
            // 订阅消息事件并添加过滤条件
            aggregator.GetEvent<MessageEvent>().Subscribe(action,
                ThreadOption.PublisherThread, true, (m) =>
                {
                    return m.Filter.Equals(filterName);// 仅处理匹配 filterName 的消息
                });
        }

        /// <summary>
        /// 发送提示消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="message"></param>
        public static void SendMessage(this IEventAggregator aggregator, string message, string filterName = "Main")
        {
            // 发布消息事件
            aggregator.GetEvent<MessageEvent>().Publish(new MessageModel()
            {
                Filter = filterName,// 消息过滤器
                Message = message,// 消息内容
            });
        }



    }


    

}
//命名空间和依赖项：
//使用了 Prism 框架的 Events 和 Dialogs 命名空间，以支持事件发布/订阅和对话框服务。

//Question 方法：
//这是一个扩展方法，用于在指定的 DialogHost 中显示询问窗口。
//参数：
//dialogHost: 对话框宿主服务的实例。
//title: 询问窗口的标题。
//content: 询问的内容。
//dialogHostName: 对话框宿主的名称，默认为 "Root"。
//返回值：返回一个 Task<IDialogResult>，表示对话框的结果。

//UpdateLoading 方法：
//这是一个扩展方法，用于发布更新加载状态的事件。
//参数：
//aggregator: 事件聚合器的实例。
//model: 包含加载状态的模型。

//Resgiter 方法：
//这是一个扩展方法，用于注册对加载更新事件的订阅。
//参数：
//aggregator: 事件聚合器的实例。
//action: 处理加载更新的委托。

//ResgiterMessage 方法：
//这是一个扩展方法，用于注册提示消息的订阅。
//参数：
//aggregator: 事件聚合器的实例。
//action: 处理提示消息的委托。
//filterName: 用于过滤消息的名称，默认为 "Main"。

//SendMessage 方法：
//这是一个扩展方法，用于发送提示消息。
//参数：
//aggregator: 事件聚合器的实例。
//message: 需要发送的消息内容。
//filterName: 消息的过滤器名称，默认为 "Main"。