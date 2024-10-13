using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Common
{
    // 继承自 Prism 框架中的 IDialogService 接口
    public interface IDialogHostService : IDialogService
    {
        // 定义一个异步方法 ShowDialog，用于显示对话框
        Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root");
    }
}
