using MaterialDesignThemes.Wpf; // 引入 Material Design 主题
using Memo.Common; // 引入公共命名空间
using Prism.Commands; // 引入 Prism 的命令功能
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using Prism.Services.Dialogs; // 引入 Prism 的对话框服务
using System; // 引入系统命名空间
using System.Collections.Generic; // 引入集合
using System.Linq; // 引入 LINQ
using System.Text; // 引入文本
using System.Threading.Tasks; // 引入异步任务

namespace Memo.ViewModels
{
    /// <summary>
    /// 消息对话框视图模型，用于处理消息对话框的显示与逻辑。
    /// </summary>
    public class MsgViewModel : BindableBase, IDialogHostAware
    {
        public MsgViewModel()
        {
            SaveCommand = new DelegateCommand(Save); // 初始化保存命令
            CancelCommand = new DelegateCommand(Cancel); // 初始化取消命令
        }

        private string title; // 对话框标题

        /// <summary>
        /// 对话框标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); } // 属性更改通知
        }

        private string content; // 对话框内容

        /// <summary>
        /// 对话框内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); } // 属性更改通知
        }

        private void Cancel()
        {
            // 关闭对话框并返回 No 结果
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        private void Save()
        {
            // 关闭对话框并返回 OK 结果
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters(); // 创建对话框参数
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        public string DialogHostName { get; set; } = "Root"; // 对话框宿主名称，默认为 Root
        public DelegateCommand SaveCommand { get; set; } // 保存命令
        public DelegateCommand CancelCommand { get; set; } // 取消命令

        /// <summary>
        /// 当对话框打开时调用，设置标题和内容。
        /// </summary>
        /// <param name="parameters">对话框参数</param>
        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Title")) // 检查参数中是否包含 Title
                Title = parameters.GetValue<string>("Title"); // 获取标题

            if (parameters.ContainsKey("Content")) // 检查参数中是否包含 Content
                Content = parameters.GetValue<string>("Content"); // 获取内容
        }
    }
}
