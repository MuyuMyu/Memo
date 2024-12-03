using MaterialDesignThemes.Wpf; // 引入 Material Design 主题的 WPF 组件
using Memo.Common; // 引入公共命名空间
using Memo.Shared.Dtos; // 引入 ToDoDto 类型
using Prism.Commands; // 引入 Prism 的命令功能
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using Prism.Services.Dialogs; // 引入 Prism 的对话框服务

namespace Memo.ViewModels.Dialogs
{
    /// <summary>
    /// 添加或编辑待办事项的对话框视图模型，负责处理用户输入和对话框的交互逻辑。
    /// </summary>
    public class AddToDoViewModel : BindableBase, IDialogHostAware
    {
        public AddToDoViewModel()
        {
            // 初始化命令
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private ToDoDto model; // 待办事项数据模型

        /// <summary>
        /// 新增或编辑的实体。
        /// </summary>
        public ToDoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); } // 通知属性已更改
        }

        /// <summary>
        /// 取消操作的方法。
        /// </summary>
        private void Cancel()
        {
            // 检查对话框是否打开，如果是则关闭并返回 No
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No)); // 取消返回 No，表示操作结束
        }

        /// <summary>
        /// 保存操作的方法。
        /// </summary>
        private void Save()
        {
            // 验证待办事项标题和内容是否为空
            if (string.IsNullOrWhiteSpace(Model.Title) ||
                string.IsNullOrWhiteSpace(model.Content)) return;

            // 检查对话框是否打开，如果是则返回 OK 和编辑的待办事项
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters();
                param.Add("Value", Model); // 将编辑的实体添加到参数中
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param)); // 关闭对话框并返回 OK
            }
        }

        public string DialogHostName { get; set; } // 对话框主机名称
        public DelegateCommand SaveCommand { get; set; } // 保存命令
        public DelegateCommand CancelCommand { get; set; } // 取消命令

        /// <summary>
        /// 当对话框打开时调用的方法，初始化视图模型。
        /// </summary>
        /// <param name="parameters">传入的对话框参数。</param>
        public void OnDialogOpend(IDialogParameters parameters)
        {
            // 检查参数中是否包含待办事项值
            if (parameters.ContainsKey("Value"))
            {
                Model = parameters.GetValue<ToDoDto>("Value"); // 如果有，初始化 Model
            }
            else
                Model = new ToDoDto(); // 如果没有，创建新的 ToDoDto 实例
        }
    }
}
