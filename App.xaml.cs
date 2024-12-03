using DryIoc;
using Memo.Context;
using Memo.Common;
using Memo.Service;
using Memo.ViewModels;
using Memo.ViewModels.Dialogs;
using Memo.Views;
using Memo.Views.Dialogs;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Memo
{
    /// <summary>
    /// 应用程序的启动类，继承自 PrismApplication，用于初始化和启动应用程序。
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// 创建并返回应用程序的主窗口（Shell）。
        /// 通过依赖注入的方式创建 MainView 窗口。
        /// </summary>
        /// <returns>主窗口实例</returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        /// <summary>
        /// 用户注销并重新登录的功能。
        /// 隐藏主窗口并弹出登录对话框。如果登录失败则退出程序，如果成功则显示主窗口。
        /// </summary>
        /// <param name="containerProvider">容器提供者，用于解析依赖项</param>
        public static void LoginOut(IContainerProvider containerProvider)
        {
            // 隐藏当前主窗口
            Current.MainWindow.Hide();

            // 显示登录对话框
            var dialog = containerProvider.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback =>
            {
                // 如果登录失败，退出程序
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                // 登录成功后，重新显示主窗口
                Current.MainWindow.Show();
            });
        }

        /// <summary>
        /// 应用程序初始化时调用此方法，用于显示登录对话框。
        /// 如果用户未成功登录，程序将退出。
        /// </summary>
        protected override void OnInitialized()
        {

            //// 确保数据库和表已经创建
            using (var context = new MemoContext())
            {
                context.Database.EnsureCreated(); // 如果数据库不存在则创建
            }
            // 显示登录对话框
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback =>
            {
                // 如果登录失败，退出程序
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                // 登录成功后，配置主窗口的数据上下文
                var service = App.Current.MainWindow.DataContext as IConfigureService;
                if (service != null)
                    service.Configure();

                // 调用父类的初始化方法
                base.OnInitialized();
            });

        }

        /// <summary>
        /// 用于注册应用程序中的服务、视图模型和视图到容器中。
        /// </summary>
        /// <param name="containerRegistry">容器注册器，用于注册类型和实例</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            // 注册登录服务、待办事项服务、备忘录服务、对话框服务
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();

            // 注册登录视图和对应的视图模型，用于对话框显示
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();

            // 注册导航视图和视图模型，用于应用程序的导航
            containerRegistry.RegisterForNavigation<AddToDoView, AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<AddMemoView, AddMemoViewModel>();
            //containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<AboutView>();
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
    }
}
