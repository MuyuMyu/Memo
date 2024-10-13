using Memo.Common.Models; // 引入公共模型命名空间
using Memo.Extensions; // 引入扩展方法命名空间
using Prism.Commands; // 引入 Prism 的命令功能
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using Prism.Regions; // 引入 Prism 的区域导航功能
using System; // 引入系统命名空间
using System.Collections.Generic; // 引入集合
using System.Collections.ObjectModel; // 引入可观察集合
using System.Linq; // 引入 LINQ
using System.Text; // 引入文本
using System.Threading.Tasks; // 引入异步任务

namespace Memo.ViewModels
{
    /// <summary>
    /// 设置视图模型，用于处理应用程序设置相关的逻辑。
    /// </summary>
    public class SettingsViewModel : BindableBase
    {
        private readonly IRegionManager regionManager; // 区域管理器

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager">区域管理器</param>
        public SettingsViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>(); // 初始化菜单栏集合
            this.regionManager = regionManager; // 保存区域管理器引用
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate); // 初始化导航命令
            CreateMenuBar(); // 创建菜单栏
        }

        /// <summary>
        /// 导航到指定视图
        /// </summary>
        /// <param name="obj">要导航的菜单项</param>
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return; // 如果菜单项为空或命名空间无效，则返回

            // 请求导航到指定的视图
            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
        }

        /// <summary>
        /// 导航命令
        /// </summary>
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        /// <summary>
        /// 菜单栏集合
        /// </summary>
        private ObservableCollection<MenuBar> menuBars;

        /// <summary>
        /// 获取或设置菜单栏集合
        /// </summary>
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); } // 通知属性变更
        }

        /// <summary>
        /// 创建菜单栏项
        /// </summary>
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Palette", Title = "个性化", NameSpace = "SkinView" }); // 个性化菜单项
            //MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "系统设置", NameSpace = "" }); // 系统设置菜单项（未实现）
            MenuBars.Add(new MenuBar() { Icon = "Information", Title = "关于更多", NameSpace = "AboutView" }); // 关于菜单项
        }
    }
}
