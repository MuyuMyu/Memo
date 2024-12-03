using Memo.Extensions; // 引入扩展方法命名空间
using Prism.Events; // 引入 Prism 的事件聚合器
using Prism.Ioc; // 引入 Prism 的容器
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using Prism.Regions; // 引入 Prism 的区域导航功能
using System; // 引入系统命名空间
using System.Collections.Generic; // 引入集合
using System.Linq; // 引入 LINQ
using System.Text; // 引入文本
using System.Threading.Tasks; // 引入异步任务

namespace Memo.ViewModels
{
    /// <summary>
    /// 导航视图模型，用于处理导航逻辑。
    /// </summary>
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider; // 容器提供者
        public readonly IEventAggregator aggregator; // 事件聚合器

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="containerProvider">容器提供者</param>
        public NavigationViewModel(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
            aggregator = containerProvider.Resolve<IEventAggregator>(); // 解析事件聚合器
        }

        /// <summary>
        /// 判断是否为导航目标
        /// </summary>
        /// <param name="navigationContext">导航上下文</param>
        /// <returns>始终返回 true，表示可以导航</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true; // 默认实现，始终允许导航
        }

        /// <summary>
        /// 当导航离开此视图时调用
        /// </summary>
        /// <param name="navigationContext">导航上下文</param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // 默认实现，不执行任何操作
        }

        /// <summary>
        /// 当导航到此视图时调用
        /// </summary>
        /// <param name="navigationContext">导航上下文</param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 默认实现，不执行任何操作
        }

        /// <summary>
        /// 更新加载状态
        /// </summary>
        /// <param name="IsOpen">加载状态是否打开</param>
        public void UpdateLoading(bool IsOpen)
        {
            aggregator.UpdateLoading(new Common.Events.UpdateModel()
            {
                IsOpen = IsOpen // 发送加载状态更新事件
            });
        }
    }
}
