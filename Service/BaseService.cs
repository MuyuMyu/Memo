using Memo.Shared.Contact;
using Memo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//用于处理与特定实体类型 TEntity 相关的基本 CRUD（创建、读取、更新、删除）操作。利用了 HttpRestClient 进行 HTTP 请求，以与 API 进行交互
namespace Memo.Service
{
    /// <summary>
    /// 1. BaseService<TEntity>
    /// 泛型类型： TEntity 表示服务将处理的实体类型，必须是一个类（where TEntity : class）。
    /// 构造函数：BaseService(HttpRestClient client, string serviceName)：接受一个 HttpRestClient 实例和服务名称，用于初始化。
    /// 
    /// 2. CRUD 方法
    /// AddAsync(TEntity entity)：
    /// 创建一个新的实体，并通过 POST 请求将其发送到 API。返回一个包含实体响应的 ApiResponse<TEntity>。
    /// 
    /// DeleteAsync(int id)：
    /// 删除指定 ID 的实体。通过 DELETE 请求发送到 API，返回一个 ApiResponse。
    /// 
    /// GetAllAsync(QueryParameter parameter)：
    /// 获取所有实体，并支持分页和搜索。通过 GET 请求发送到 API，返回一个包含分页列表的 ApiResponse<PagedList<TEntity>>。
    /// 
    /// GetFirstOfDefaultAsync(int id)：
    /// 根据 ID 获取实体。如果找到，返回包含实体的 ApiResponse<TEntity>。
    /// 
    /// UpdateAsync(TEntity entity)：
    /// 更新指定的实体，通过 POST 请求发送到 API。返回一个包含更新后实体的 ApiResponse<TEntity>。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient client; // 用于发送 HTTP 请求的客户端
        private readonly string serviceName; // 服务名称，通常对应 API 的一部分路径

        public BaseService(HttpRestClient client, string serviceName)
        {
            this.client = client; // 初始化 HttpRestClient
            this.serviceName = serviceName; // 初始化服务名称
        }

        public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest(); // 创建基本请求对象
            request.Method = RestSharp.Method.Post; // 设置请求方法为 POST
            request.Route = $"api/{serviceName}/Add"; // 设置请求路由
            request.Parameter = entity; // 设置请求参数为要添加的实体
            return await client.ExecuteAsync<TEntity>(request); // 执行请求并返回结果
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest(); // 创建基本请求对象
            request.Method = RestSharp.Method.Delete; // 设置请求方法为 DELETE
            request.Route = $"api/{serviceName}/Delete?id={id}"; // 设置请求路由
            return await client.ExecuteAsync(request); // 执行请求并返回结果
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest(); // 创建基本请求对象
            request.Method = RestSharp.Method.Get; // 设置请求方法为 GET
            request.Route = $"api/{serviceName}/GetAll?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}"; // 设置请求路由和查询参数
            return await client.ExecuteAsync<PagedList<TEntity>>(request); // 执行请求并返回结果
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest(); // 创建基本请求对象
            request.Method = RestSharp.Method.Get; // 设置请求方法为 GET
            request.Route = $"api/{serviceName}/Get?id={id}"; // 设置请求路由
            return await client.ExecuteAsync<TEntity>(request); // 执行请求并返回结果
        }

        public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest(); // 创建基本请求对象
            request.Method = RestSharp.Method.Post; // 设置请求方法为 POST
            request.Route = $"api/{serviceName}/Update"; // 设置请求路由
            request.Parameter = entity; // 设置请求参数为要更新的实体
            return await client.ExecuteAsync<TEntity>(request); // 执行请求并返回结果
        }
    }
}
