using Memo.Common; // 引入公共命名空间
using Memo.Common.Models; // 引入公共模型
using Memo.Extensions; // 引入扩展方法
using Memo.Service; // 引入服务层
using Memo.Shared.Dtos; // 引入共享 DTOs
using Memo.Shared.Parameters; // 引入共享参数
using Prism.Commands; // 引入 Prism 的命令功能
using Prism.Ioc; // 引入 Prism 的 IoC 容器
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using Prism.Regions; // 引入 Prism 的区域管理
using System; // 引入系统命名空间
using System.Collections.Generic; // 引入集合
using System.Collections.ObjectModel; // 引入可观察集合
using System.Linq; // 引入 LINQ
using System.Text; // 引入文本
using System.Threading.Tasks; // 引入异步任务

namespace Memo.ViewModels
{
    /// <summary>
    /// 备忘录视图模型，负责备忘录的管理和操作逻辑。
    /// </summary>
    public class MemoViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogHost; // 对话框服务
        private readonly IMemoService service; // 备忘录服务
        private ObservableCollection<Context.Memo> memoDtos; // 备忘录 DTO 集合

        public MemoViewModel(IMemoService service, IContainerProvider provider)
           : base(provider)
        {
            MemoDtos = new ObservableCollection<Context.Memo>(); // 初始化备忘录 DTO 集合
            ExecuteCommand = new DelegateCommand<string>(Execute); // 初始化执行命令
            SelectedCommand = new DelegateCommand<Context.Memo>(Selected); // 初始化选中命令
            DeleteCommand = new DelegateCommand<Context.Memo>(Delete); // 初始化删除命令
            dialogHost = provider.Resolve<IDialogHostService>(); // 解析对话框服务
            this.service = service; // 依赖注入备忘录服务
        }

        private async void Delete(Context.Memo obj)
        {
            try
            {
                // 弹出确认对话框
                var dialogResult = await dialogHost.Question("温馨提示", $"确认删除备忘录:{obj.Title} ?");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true); // 开始加载状态
                var deleteResult = await service.DeleteAsync(obj.Id); // 调用删除服务
                if (deleteResult.Status)
                {
                    var model = MemoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                        MemoDtos.Remove(model); // 从集合中移除已删除的备忘录
                }
            }
            finally
            {
                UpdateLoading(false); // 结束加载状态
            }
        }

        private void Execute(string obj)
        {
            // 根据命令执行相应操作
            switch (obj)
            {
                case "新增": Add(); break; // 调用添加方法
                case "查询": GetDataAsync(); break; // 调用获取数据方法
                case "保存": Save(); break; // 调用保存方法
            }
        }

        private string search; // 搜索条件

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); } // 属性更改通知
        }

        private bool isRightDrawerOpen; // 右侧编辑窗口是否展开

        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); } // 属性更改通知
        }

        private Context.Memo currentDto; // 当前编辑的备忘录 DTO

        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public Context.Memo CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); } // 属性更改通知
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        private void Add()
        {
            CurrentDto = new Context.Memo(); // 创建新的备忘录对象
            IsRightDrawerOpen = true; // 打开右侧编辑窗口
        }

        private async void Selected(Context.Memo obj)
        {
            try
            {
                UpdateLoading(true); // 开始加载状态
                var todoResult = await service.GetFirstOfDefaultAsync(obj.Id); // 获取选中的备忘录
                if (todoResult.Status)
                {
                    Context.Memo NewM = new Context.Memo();
                    NewM.Id = todoResult.Result.Id;
                    NewM.Content = todoResult.Result.Content;
                    NewM.Title = todoResult.Result.Title;

                    CurrentDto = NewM; // 设置当前 DTO
                    IsRightDrawerOpen = true; // 打开右侧编辑窗口
                }
            }
            catch (Exception ex)
            {
                // 处理异常
            }
            finally
            {
                UpdateLoading(false); // 结束加载状态
            }
        }

        private async void Save()
        {
            // 验证必填字段
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) ||
                string.IsNullOrWhiteSpace(CurrentDto.Content))
                return;

            UpdateLoading(true); // 开始加载状态

            try
            {
                // 判断是否为更新操作
                if (CurrentDto.Id > 0)
                {
                    var updateResult = await service.UpdateAsync(CurrentDto); // 调用更新服务
                    if (updateResult.Status)
                    {
                        var todo = MemoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentDto.Title; // 更新备忘录标题
                            todo.Content = CurrentDto.Content; // 更新备忘录内容
                        }
                    }
                    IsRightDrawerOpen = false; // 关闭右侧编辑窗口
                }
                else // 新增备忘录
                {
                    var addResult = await service.AddAsync(CurrentDto); // 调用添加服务
                    if (addResult.Status)
                    {
                        Context.Memo NewM = new Context.Memo();
                        NewM.Content = addResult.Result.Content;
                        NewM.Title = addResult.Result.Title;

                        MemoDtos.Add(NewM); // 将新备忘录添加到集合中
                        IsRightDrawerOpen = false; // 关闭右侧编辑窗口
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
            }
            finally
            {
                UpdateLoading(false); // 结束加载状态
            }
        }

        // 命令定义
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<Context.Memo> SelectedCommand { get; private set; }
        public DelegateCommand<Context.Memo> DeleteCommand { get; private set; }

        public ObservableCollection<Context.Memo> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); } // 属性更改通知
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        async void GetDataAsync()
        {
            UpdateLoading(true); // 开始加载状态

            var todoResult = await service.GetAllAsync(new QueryParameter()
            {
                PageIndex = 0, // 设置页码
                PageSize = 100, // 设置每页数量
                Search = Search, // 传递搜索条件
            });

            if (todoResult.Status)
            {
                MemoDtos.Clear(); // 清空当前备忘录集合
                foreach (var item in todoResult.Result.Items)
                {


                    MemoDtos.Add(item); // 添加新获取的备忘录
                }
            }
            UpdateLoading(false); // 结束加载状态
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext); // 调用基类方法
            GetDataAsync(); // 获取数据
        }
    }
}
