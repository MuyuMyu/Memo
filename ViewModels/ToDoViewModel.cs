﻿using Memo.Common; // 引入公共命名空间
using Memo.Common.Models; // 引入模型命名空间
using Memo.Extensions; // 引入扩展方法命名空间
using Memo.Service; // 引入服务命名空间
using Memo.Shared.Dtos; // 引入数据传输对象命名空间
using Memo.Shared.Parameters; // 引入参数命名空间
using Prism.Commands; // 引入 Prism 的命令功能
using Prism.Ioc; // 引入 Prism 的 IoC 容器
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using Prism.Regions; // 引入 Prism 的区域管理
using System; // 引入系统命名空间
using System.Collections.Generic; // 引入集合命名空间
using System.Collections.ObjectModel; // 引入可观察集合
using System.Linq; // 引入 LINQ
using System.Text; // 引入文本功能
using System.Threading.Tasks; // 引入异步任务功能

namespace Memo.ViewModels
{
    /// <summary>
    /// 待办事项视图模型，管理待办事项的 CRUD 操作和状态
    /// </summary>
    public class ToDoViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogHost; // 对话框服务

        public ToDoViewModel(IToDoService service, IContainerProvider provider)
            : base(provider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>(); // 初始化待办事项集合
            ExecuteCommand = new DelegateCommand<string>(Execute); // 初始化执行命令
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected); // 初始化选择命令
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete); // 初始化删除命令
            dialogHost = provider.Resolve<IDialogHostService>(); // 解析对话框服务
            this.service = service; // 依赖注入待办事项服务
        }

        // 删除待办事项
        private async void Delete(ToDoDto obj)
        {
            try
            {
                var dialogResult = await dialogHost.Question("温馨提示", $"确认删除待办事项:{obj.Title} ?"); // 弹出确认对话框
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return; // 用户未确认则返回

                UpdateLoading(true); // 更新加载状态
                var deleteResult = await service.DeleteAsync(obj.Id); // 调用服务删除待办事项
                if (deleteResult.Status)
                {
                    var model = ToDoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id)); // 查找待办事项
                    if (model != null)
                        ToDoDtos.Remove(model); // 从集合中移除
                }
            }
            finally
            {
                UpdateLoading(false); // 恢复加载状态
            }
        }

        // 执行对应的命令
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break; // 新增待办
                case "查询": GetDataAsync(); break; // 查询待办
                case "保存": Save(); break; // 保存待办
            }
        }

        // 选中状态索引
        private int selectedIndex;

        /// <summary>
        /// 下拉列表选中状态值
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        // 搜索条件
        private string search;

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        // 右侧编辑窗口状态
        private bool isRightDrawerOpen;

        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        // 当前待办事项对象
        private ToDoDto currentDto;

        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public ToDoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 添加待办事项
        /// </summary>
        private void Add()
        {
            CurrentDto = new ToDoDto(); // 初始化新的待办事项
            IsRightDrawerOpen = true; // 打开编辑窗口
        }

        // 选中待办事项
        private async void Selected(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true); // 更新加载状态
                var todoResult = await service.GetFirstOfDefaultAsync(obj.Id); // 获取待办事项详情
                if (todoResult.Status)
                {
                    CurrentDto = todoResult.Result; // 设置当前待办事项
                    IsRightDrawerOpen = true; // 打开编辑窗口
                }
            }
            catch (Exception ex)
            {
                // 处理异常
            }
            finally
            {
                UpdateLoading(false); // 恢复加载状态
            }
        }

        // 保存待办事项
        private async void Save()
        {
            // 检查待办事项标题和内容是否有效
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) ||
                string.IsNullOrWhiteSpace(CurrentDto.Content))
                return;

            UpdateLoading(true); // 更新加载状态

            try
            {
                if (CurrentDto.Id > 0) // 更新现有待办事项
                {
                    var updateResult = await service.UpdateAsync(CurrentDto); // 调用服务更新待办事项
                    if (updateResult.Status)
                    {
                        var todo = ToDoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id); // 查找待办事项
                        if (todo != null)
                        {
                            todo.Title = CurrentDto.Title; // 更新标题
                            todo.Content = CurrentDto.Content; // 更新内容
                            todo.Status = CurrentDto.Status; // 更新状态
                        }
                    }
                    IsRightDrawerOpen = false; // 关闭编辑窗口
                }
                else // 新增待办事项
                {
                    var addResult = await service.AddAsync(CurrentDto); // 调用服务添加待办事项
                    if (addResult.Status)
                    {
                        ToDoDtos.Add(addResult.Result); // 添加到集合
                        IsRightDrawerOpen = false; // 关闭编辑窗口
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
            }
            finally
            {
                UpdateLoading(false); // 恢复加载状态
            }
        }

        // 命令定义
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }
        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }

        private ObservableCollection<ToDoDto> toDoDtos; // 待办事项集合
        private readonly IToDoService service; // 待办事项服务

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        async void GetDataAsync()
        {
            UpdateLoading(true); // 更新加载状态

            // 根据选中的索引设置状态过滤
            int? Status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;

            var todoResult = await service.GetAllFilterAsync(new ToDoParameter()
            {
                PageIndex = 0, // 当前页码
                PageSize = 100, // 每页显示数量
                Search = Search, // 搜索条件
                Status = Status // 状态过滤
            });

            if (todoResult.Status)
            {
                ToDoDtos.Clear(); // 清空集合
                foreach (var item in todoResult.Result.Items)
                {
                    ToDoDtos.Add(item); // 添加待办事项到集合
                }
            }
            UpdateLoading(false); // 恢复加载状态
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext); // 调用基类方法
            // 获取导航参数
            if (navigationContext.Parameters.ContainsKey("Value"))
                SelectedIndex = navigationContext.Parameters.GetValue<int>("Value");
            else
                SelectedIndex = 0;
            GetDataAsync(); // 获取待办事项数据
        }
    }
}
