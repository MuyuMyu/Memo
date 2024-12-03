using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Memo.Common
{
    /// <summary>
    /// 自定义对话框服务，实现了 IDialogHostService 接口。
    /// 该服务负责展示基于 DialogHost 的对话框。
    /// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        // 依赖注入容器，用于解析对话框的实例
        private readonly IContainerExtension containerExtension;

        // 构造函数，通过依赖注入将容器传入
        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }


        /// <summary>
        /// 显示对话框的方法
        /// </summary>
        /// <param name="name">对话框的名称（或视图名称）</param>
        /// <param name="parameters">对话框参数</param>
        /// <param name="dialogHostName">对话框主机的名称（默认是 "Root"）</param>
        /// <returns>返回一个对话框的结果</returns>
        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null)
                parameters = new DialogParameters();

            //从容器当中去除弹出窗口的实例
            var content = containerExtension.Resolve<object>(name);

            // 验证解析出的内容是否是 FrameworkElement
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            // 如果视图没有绑定数据上下文且没有自动设置 ViewModel，则自动绑定 ViewModel
            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            // 验证 DataContext 是否实现了 IDialogHostAware 接口，确保 ViewModel 能处理对话框相关的事件
            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            // 设置 ViewModel 的 DialogHostName 属性
            viewModel.DialogHostName = dialogHostName;

            // 设置对话框打开事件的处理程序
            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpend(parameters);// 当对话框打开时，传递参数
                }
                eventArgs.Session.UpdateContent(content);// 更新对话框内容
            };

            // 使用 DialogHost 弹出对话框并返回结果
            return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
        }
    }
}
