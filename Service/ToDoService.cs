using Memo.Shared.Contact; // 引用 ApiResponse 类型
using Memo.Shared.Dtos;    // 引用 ToDoDto 和 SummaryDto 类型
using Memo.Shared.Parameters; // 引用 ToDoParameter 类型
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Service
{
    /// <summary>
    /// 提供待办事项服务的实现，继承自 BaseService。
    /// </summary>
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient client; // HTTP REST 客户端实例

        /// <summary>
        /// ToDoService 构造函数，初始化基类 BaseService。
        /// </summary>
        /// <param name="client">HTTP REST 客户端实例。</param>
        public ToDoService(HttpRestClient client) : base(client, "ToDo")
        {
            this.client = client; // 将 HTTP 客户端赋值给类成员
        }

        /// <summary>
        /// 获取待办事项的分页列表，支持过滤参数。
        /// </summary>
        /// <param name="parameter">包含分页和过滤条件的参数。</param>
        /// <returns>返回 ApiResponse 包含待办事项分页列表。</returns>
        public async Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get; // 设置 HTTP 请求方法
            request.Route = $"api/ToDo/GetAll?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}" +
                $"&status={parameter.Status}"; // 构建请求路由，包含分页和过滤参数
            return await client.ExecuteAsync<PagedList<ToDoDto>>(request); // 执行请求并返回结果
        }

        /// <summary>
        /// 获取待办事项的统计信息。
        /// </summary>
        /// <returns>返回 ApiResponse 包含统计信息的对象。</returns>
        public async Task<ApiResponse<SummaryDto>> SummaryAsync()
        {
            BaseRequest request = new BaseRequest();
            request.Route = "api/ToDo/Summary"; // 设置请求路由
            return await client.ExecuteAsync<SummaryDto>(request); // 执行请求并返回结果
        }
    }
}
