using Memo.Common.Models; // 引入公共模型
using Memo.Shared.Dtos; // 引入数据传输对象
using Prism.Commands; // 引入 Prism 的命令功能
using Memo.Extensions; // 引入扩展方法
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using System.Collections.ObjectModel; // 引入可观察集合
using Memo.Common; // 引入公共命名空间
using Prism.Regions; // 引入 Prism 的区域管理
using Memo.Service; // 引入服务层
using Prism.Ioc; // 引入 Prism 的 IoC 容器
using Prism.Services.Dialogs; // 引入 Prism 的对话框服务
using Memo.Context;

namespace Memo.ViewModels
{
    /// <summary>
    /// 首页视图模型，负责首页的逻辑处理，包括待办事项和备忘录的管理。
    /// </summary>
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IToDoService toDoService; // 待办事项服务
        private readonly IMemoService memoService; // 备忘录服务
        private readonly IDialogHostService dialog; // 对话框服务
        private readonly IRegionManager regionManager; // 区域管理器

        public IndexViewModel(IContainerProvider provider, IDialogHostService dialog) : base(provider)
        {
            // 初始化标题，包含用户信息和当前日期
            Title = $"你好，{AppSession.UserName} {DateTime.Now.GetDateTimeFormats('D')[1].ToString()}";
            CreateTaskBars(); // 创建任务栏
            ExecuteCommand = new DelegateCommand<string>(Execute); // 初始化命令

            // 依赖注入服务
            this.toDoService = provider.Resolve<IToDoService>();
            this.memoService = provider.Resolve<IMemoService>();
            this.regionManager = provider.Resolve<IRegionManager>();
            this.dialog = dialog;

            // 初始化编辑命令
            EditMemoCommand = new DelegateCommand<Context.Memo>(AddMemo);
            EditToDoCommand = new DelegateCommand<ToDo>(AddToDo);
            ToDoCompltedCommand = new DelegateCommand<ToDo>(Complted);
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);
        }

        /// <summary>
        /// 导航到指定的视图。
        /// </summary>
        /// <param name="obj">任务栏对象</param>
        private void Navigate(TaskBar obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Target)) return; // 如果目标为空，返回

            // 创建导航参数
            NavigationParameters param = new NavigationParameters();
            if (obj.Title == "已完成")
            {
                param.Add("Value", 2); // 设置参数
            }

            // 请求导航到目标视图
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.Target, param);
        }

        /// <summary>
        /// 标记待办事项为完成。
        /// </summary>
        /// <param name="obj">待办事项对象</param>
        private async void Complted(ToDo obj)
        {
            try
            {
                UpdateLoading(true); // 更新加载状态
                var updateResult = await toDoService.UpdateAsync(obj); // 更新待办事项状态
                if (updateResult.Status) // 如果更新成功
                {
                    var todo = summary.ToDoList.FirstOrDefault(t => t.Id.Equals(obj.Id)); // 查找待办事项
                    if (todo != null)
                    {
                        summary.ToDoList.Remove(todo); // 从列表中移除
                        summary.CompletedCount += 1; // 更新已完成计数
                        summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%"); // 更新完成比例
                        this.Refresh(); // 刷新视图
                    }
                    aggregator.SendMessage("已完成!"); // 发送消息
                }
            }
            finally
            {
                UpdateLoading(false); // 恢复加载状态
            }
        }

        // 命令定义
        public DelegateCommand<ToDo> ToDoCompltedCommand { get; private set; }
        public DelegateCommand<ToDo> EditToDoCommand { get; private set; }
        public DelegateCommand<Context.Memo> EditMemoCommand { get; private set; }
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }

        #region 属性

        private string title; // 标题

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); } // 属性更改通知
        }

        private ObservableCollection<TaskBar> taskBars; // 任务栏集合

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); } // 属性更改通知
        }

        private SummaryDto summary; // 首页统计信息

        /// <summary>
        /// 首页统计
        /// </summary>
        public SummaryDto Summary
        {
            get { return summary; }
            set { summary = value; RaisePropertyChanged(); } // 属性更改通知
        }

        #endregion

        /// <summary>
        /// 执行命令，根据操作类型进行不同的逻辑处理。
        /// </summary>
        /// <param name="obj">操作类型</param>
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办": AddToDo(null); break; // 新增待办事项
                case "新增备忘录": AddMemo(null); break; // 新增备忘录
            }
        }

        /// <summary>
        /// 添加待办事项。
        /// </summary>
        /// <param name="model">待办事项模型</param>
        async void AddToDo(ToDo model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model); // 如果模型不为空，传递模型

            var dialogResult = await dialog.ShowDialog("AddToDoView", param); // 显示对话框
            if (dialogResult.Result == ButtonResult.OK) // 如果用户点击 OK
            {
                try
                {
                    UpdateLoading(true); // 更新加载状态
                    var todo = dialogResult.Parameters.GetValue<ToDoDto>("Value"); // 获取返回的待办事项

                    if (todo.Id > 0) // 如果是更新操作
                    {
                        var updateResult = await toDoService.UpdateAsync(todo); // 更新待办事项
                        if (updateResult.Status)
                        {
                            var todoModel = summary.ToDoList.FirstOrDefault(t => t.Id.Equals(todo.Id)); // 查找待办事项
                            if (todoModel != null)
                            {
                                // 更新待办事项内容
                                todoModel.Title = todo.Title;
                                todoModel.Content = todo.Content;
                            }
                        }
                    }
                    else // 如果是新增操作
                    {
                        var addResult = await toDoService.AddAsync(todo); // 添加待办事项
                        if (addResult.Status)
                        {
                            ToDo NewT = new ToDo();
                            NewT.Title = addResult.Result.Title;
                            NewT.Content = addResult.Result.Content;
                            NewT.Status = addResult.Result.Status;

                            summary.Sum += 1; // 更新总数
                            summary.ToDoList.Add(NewT); // 添加到列表
                            summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%"); // 更新完成比例
                            this.Refresh(); // 刷新视图
                        }
                    }
                }
                finally
                {
                    UpdateLoading(false); // 恢复加载状态
                }
            }
        }

        /// <summary>
        /// 添加备忘录。
        /// </summary>
        /// <param name="model">备忘录模型</param>
        async void AddMemo(Context.Memo model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model); // 如果模型不为空，传递模型

            var dialogResult = await dialog.ShowDialog("AddMemoView", param); // 显示对话框
            if (dialogResult.Result == ButtonResult.OK) // 如果用户点击 OK
            {
                try
                {
                    UpdateLoading(true); // 更新加载状态
                    var memo = dialogResult.Parameters.GetValue<Context.Memo>("Value"); // 获取返回的备忘录

                    if (memo.Id > 0) // 如果是更新操作
                    {
                        var updateResult = await memoService.UpdateAsync(memo); // 更新备忘录
                        if (updateResult.Status)
                        {
                            var memoModel = summary.MemoList.FirstOrDefault(t => t.Id.Equals(memo.Id)); // 查找备忘录
                            if (memoModel != null)
                            {
                                // 更新备忘录内容
                                memoModel.Title = memo.Title;
                                memoModel.Content = memo.Content;
                            }
                        }
                    }
                    else // 如果是新增操作
                    {
                        var addResult = await memoService.AddAsync(memo); // 添加备忘录
                        if (addResult.Status)
                        {
                            summary.MemoList.Add(addResult.Result); // 添加到备忘录列表
                        }
                    }
                }
                finally
                {
                    UpdateLoading(false); // 恢复加载状态
                }
            }
        }

        /// <summary>
        /// 创建任务栏。
        /// </summary>
        void CreateTaskBars()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Color = "#FF1ECA3A", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Color = "#FF02C6DC", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Color = "#FFFFA000", Target = "MemoView" });
        }

        /// <summary>
        /// 导航到该视图时执行的方法。
        /// </summary>
        /// <param name="navigationContext">导航上下文</param>
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var summaryResult = await toDoService.SummaryAsync(); // 获取统计信息
            if (summaryResult.Status)
            {
                Summary = summaryResult.Result; // 更新统计数据
                Refresh(); // 刷新视图
            }
            base.OnNavigatedTo(navigationContext);
        }

        /// <summary>
        /// 刷新视图数据。
        /// </summary>
        void Refresh()
        {
            TaskBars[0].Content = summary.Sum.ToString(); // 更新总待办数
            TaskBars[1].Content = summary.CompletedCount.ToString(); // 更新已完成数
            TaskBars[2].Content = summary.CompletedRatio; // 更新完成比例
            TaskBars[3].Content = summary.MemoeCount.ToString(); // 更新备忘录数
        }
    }
}
