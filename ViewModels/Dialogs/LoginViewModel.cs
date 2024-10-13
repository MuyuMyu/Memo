using Memo.Service; // 引入服务层
using Memo.Shared.Dtos; // 引入数据传输对象
using Memo.Extensions; // 引入扩展方法
using Prism.Commands; // 引入 Prism 的命令功能
using Prism.Events; // 引入 Prism 的事件聚合器
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using Prism.Services.Dialogs; // 引入 Prism 的对话框服务
using System;

namespace Memo.ViewModels.Dialogs
{
    /// <summary>
    /// 登录视图模型，处理用户登录和注册逻辑。
    /// </summary>
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly ILoginService loginService; // 登录服务
        private readonly IEventAggregator aggregator; // 事件聚合器
        private ResgiterUserDto userDto; // 用户 DTO

        public LoginViewModel(ILoginService loginService, IEventAggregator aggregator)
        {
            UserDto = new ResgiterUserDto(); // 初始化用户 DTO
            ExecuteCommand = new DelegateCommand<string>(Execute); // 初始化命令
            this.loginService = loginService; // 依赖注入
            this.aggregator = aggregator; // 依赖注入
        }

        public string Title { get; set; } = "ToDo"; // 对话框标题

        public event Action<IDialogResult> RequestClose; // 请求关闭事件

        public bool CanCloseDialog() => true; // 能否关闭对话框

        public void OnDialogClosed() => LoginOut(); // 对话框关闭时的处理

        public void OnDialogOpened(IDialogParameters parameters) { } // 对话框打开时的处理

        #region Login

        private int selectIndex; // 选中的索引

        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; RaisePropertyChanged(); } // 属性更改通知
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; } // 执行命令

        private string userName; // 用户名

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); } // 属性更改通知
        }

        private string passWord; // 密码

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); } // 属性更改通知
        }

        /// <summary>
        /// 执行命令，根据操作类型进行不同的逻辑处理。
        /// </summary>
        /// <param name="obj">操作类型</param>
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login": Login(); break; // 登录
                case "LoginOut": LoginOut(); break; // 登出
                case "Resgiter": Resgiter(); break; // 注册
                case "ResgiterPage": SelectIndex = 1; break; // 切换到注册页
                case "Return": SelectIndex = 0; break; // 返回到登录页
            }
        }

        public ResgiterUserDto UserDto
        {
            get { return userDto; }
            set { userDto = value; RaisePropertyChanged(); } // 属性更改通知
        }

        /// <summary>
        /// 执行登录操作。
        /// </summary>
        async void Login()
        {
            // 检查用户名和密码是否为空
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(PassWord))
            {
                return;
            }

            // 调用登录服务进行登录
            var loginResult = await loginService.Login(new Shared.Dtos.UserDto()
            {
                Account = UserName,
                PassWord = PassWord
            });

            if (loginResult != null && loginResult.Status)
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK)); // 登录成功，关闭对话框并返回 OK
            }
            else
            {
                // 登录失败提示
                aggregator.SendMessage(loginResult.Message, "Login");
            }
        }

        /// <summary>
        /// 执行注册操作。
        /// </summary>
        private async void Resgiter()
        {
            // 检查注册信息是否完整
            if (string.IsNullOrWhiteSpace(UserDto.Account) ||
                string.IsNullOrWhiteSpace(UserDto.UserName) ||
                string.IsNullOrWhiteSpace(UserDto.PassWord) ||
                string.IsNullOrWhiteSpace(UserDto.NewPassWord))
            {
                aggregator.SendMessage("请输入完整的注册信息！", "Login");
                return;
            }

            // 检查密码是否一致
            if (UserDto.PassWord != UserDto.NewPassWord)
            {
                aggregator.SendMessage("密码不一致,请重新输入！", "Login");
                return;
            }

            // 调用注册服务进行注册
            var resgiterResult = await loginService.Resgiter(new Shared.Dtos.UserDto()
            {
                Account = UserDto.Account,
                UserName = UserDto.UserName,
                PassWord = UserDto.PassWord
            });

            if (resgiterResult != null && resgiterResult.Status)
            {
                aggregator.SendMessage("注册成功", "Login");
                SelectIndex = 0; // 注册成功，返回登录页
            }
            else
            {
                aggregator.SendMessage(resgiterResult.Message, "Login");
            }
        }

        /// <summary>
        /// 执行登出操作。
        /// </summary>
        void LoginOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No)); // 返回 No 表示登出
        }

        #endregion
    }
}
