using Memo.Common.Models;
using Memo.Common;
using Memo.Extensions;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Memo.ViewModels
{
    public class MainViewModel : BindableBase
    {

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        //Prism 框架中的 IRegionManager 用于管理 WPF 中的区域导航。通过它可以在指定的区域（Region）内加载不同的视图。
        public MainViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
        {
            //ObservableCollection 是为了确保当 MenuBars 集合发生更改时，UI 会自动更新。这对于导航菜单来说非常重要，当菜单项被动态添加或删除时，UI 能够即时反映这些变化
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            //一个委托命令，接受 MenuBar 对象作为参数，用于执行导航操作
            NavigateCommond = new DelegateCommand<MenuBar>(Navigate);

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

            this.regionManager = regionManager;
            this.containerProvider = containerProvider;
            
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
            {   
                return;
            }

            //根据传入的 MenuBar 对象的 Title 属性，进行区域内的视图导航。通过 RequestNavigate，可以加载不同的视图到指定的区域
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal; //获取导航历史管理器
            });

        }

        public DelegateCommand<MenuBar> NavigateCommond { get; set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        private ObservableCollection<MenuBar> menuBars;
        private readonly IContainerProvider containerProvider;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal; //管理导航历史，允许用户前进或后退



        /// <summary>
        /// 导航菜单集合
        /// 使用 ObservableCollection 是为了自动更新 UI
        /// </summary>
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; 
                RaisePropertyChanged(); // 通知 UI 该属性发生了更改
            }
        }



        /// <summary>
        /// 创建导航菜单的方法
        /// 添加多个 MenuBar 对象到 MenuBars 集合中
        /// </summary>
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }


        /// <summary>
        /// 配置首页初始化参数
        /// </summary>
        public void Configure()
        {
            UserName = AppSession.UserName;
            CreateMenuBar();
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
        }

    }
}

    
 
