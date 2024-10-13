using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Common.Events
{
    // 定义更新模型类，封装更新状态信息
    public class UpdateModel
    {
        // 用于标记某个功能是否开启/关闭的布尔属性
        public bool IsOpen { get; set; }
    }

    // 定义更新加载事件类，继承自 PubSubEvent<T>，使用 UpdateModel 作为事件数据类型
    public class UpdateLoadingEvent : PubSubEvent<UpdateModel>
    {

    }
}
