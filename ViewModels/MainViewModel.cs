using Memo.Common.Models; // 引入公共模型
using Memo.Common; // 引入公共命名空间
using Memo.Extensions; // 引入扩展方法
using Prism.Commands; // 引入 Prism 的命令功能
using Prism.Events; // 引入 Prism 事件机制
using Prism.Ioc; // 引入 Prism 的 IoC 容器
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using Prism.Regions; // 引入 Prism 的区域管理
using System; // 引入系统命名空间
using System.Collections.Generic; // 引入集合
using System.Collections.ObjectModel; // 引入可观察集合
using System.Linq; // 引入 LINQ
using System.Text; // 引入文本
using System.Threading.Tasks; // 引入异步任务
using System.Diagnostics; // 引入调试功能

namespace Memo.ViewModels
{
    /// <summary>
    /// 主视图模型，负责应用程序的主界面逻辑和用户操作。
    /// </summary>
    public class MainViewModel : BindableBase, IConfigureService
    {
        private string userName; // 用户名属性

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); } // 属性更改通知
        }

        public DelegateCommand LoginOutCommand { get; private set; } // 注销命令

        public MainViewModel(IContainerProvider containerProvider,
            IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>(); // 初始化菜单栏集合
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate); // 初始化导航命令

            //Configure();
            //CreateMenuBar();

            // 初始化后退和前进命令
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                    journal.GoBack();
            });

            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                    journal.GoForward();
            });

            // 注销当前用户的命令
            LoginOutCommand = new DelegateCommand(() =>
            {
                App.LoginOut(containerProvider);
            });

            // 依赖注入服务
            this.containerProvider = containerProvider;
            this.regionManager = regionManager;
        }

        /// <summary>
        /// 导航到指定的视图。
        /// </summary>
        /// <param name="obj">菜单栏对象</param>
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return; // 如果对象为空或命名空间无效，返回

            // 请求导航到目标视图
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal; // 记录导航历史
            });
        }

        // 命令定义
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        private ObservableCollection<MenuBar> menuBars; // 菜单栏集合
        private readonly IContainerProvider containerProvider; // IoC 容器
        private readonly IRegionManager regionManager; // 区域管理器
        private IRegionNavigationJournal journal; // 导航历史

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); } // 属性更改通知
        }

        /// <summary>
        /// 创建菜单栏项。
        /// </summary>
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });

            // 调试输出
            Debug.WriteLine($"MenuBars Count: {MenuBars.Count}");
        }

        /// <summary>
        /// 配置首页初始化参数。
        /// </summary>
        public void Configure()
        {
            UserName = AppSession.UserName; // 设置用户名
            CreateMenuBar(); // 创建菜单栏
            //regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView"); // 导航到首页
        }
    }
}
