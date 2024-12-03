using Memo.Context;
using Memo.Shared.Contact; // 引用 ApiResponse 和 PagedList 类型
using Memo.Shared.Dtos;    // 引用 ToDoDto 和 SummaryDto 数据传输对象
using Memo.Shared.Parameters; // 引用 ToDoParameter 类型
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Service
{
    /// <summary>
    /// 定义待办事项服务接口，继承自基础服务接口。
    /// 提供对待办事项 (ToDo) 的 CRUD 操作及额外功能。
    /// </summary>
    public interface IToDoService : IBaseService<ToDoDto>
    {
        Task<ApiResponse<ToDo>> AddAsync(ToDo entity);
        Task<ApiResponse<ToDo>> UpdateAsync(ToDo entity);
        Task<ApiResponse> DeleteAsync(int id);

        /// <summary>
        /// 根据指定参数获取所有待办事项，并支持过滤和分页。
        /// </summary>
        /// <param name="parameter">用于过滤和分页的待办事项参数。</param>
        /// <returns>包含过滤后的待办事项的分页结果。</returns>
        Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter parameter);

        /// <summary>
        /// 获取待办事项的汇总信息。
        /// </summary>
        /// <returns>包含待办事项汇总信息的响应。</returns>
        Task<ApiResponse<SummaryDto>> SummaryAsync();
    }
}
