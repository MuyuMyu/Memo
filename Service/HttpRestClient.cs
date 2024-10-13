using Memo.Shared.Contact; // 引用共享的 API 响应模型
using Newtonsoft.Json; // 引用 JSON 处理库
using RestSharp; // 引用 REST 客户端库
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//用于发送 HTTP 请求并处理响应。它主要用于与 REST API 交互，通过封装常见的 HTTP 操作（如 GET、POST、DELETE 等），为应用程序提供了简单的接口
namespace Memo.Service
{
    /// <summary>
    /// HttpRestClient 类用于发送 HTTP 请求并处理响应。
    /// 它主要用于与 REST API 交互，通过封装常见的 HTTP 操作（如 GET、POST、DELETE 等），
    /// 为应用程序提供了简单的接口。
    /// </summary>
    public class HttpRestClient
    {
        private readonly string apiUrl; // API 基础 URL
        protected readonly RestClient client; // RestSharp 的客户端实例

        /// <summary>
        /// 初始化 HttpRestClient 类的新实例。
        /// </summary>
        /// <param name="apiUrl">API 的基础 URL。</param>
        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl; // 初始化 API URL
            client = new RestClient(); // 创建 RestClient 实例
        }

        /// <summary>
        /// 执行一个不返回实体的 API 请求，返回 ApiResponse。
        /// </summary>
        /// <param name="baseRequest">包含请求信息的 BaseRequest 对象。</param>
        /// <returns>表示异步操作的任务，包含 ApiResponse。</returns>
        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            // 创建 RestRequest 实例，指定请求路由和方法
            var request = new RestRequest(baseRequest.Route, (Method)Enum.Parse(typeof(Method), baseRequest.Method.ToString(), true));
            request.AddHeader("Content-Type", baseRequest.ContentType); // 添加请求头

            // 如果请求参数不为空，将其序列化为 JSON 并添加到请求体
            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);

            // 发送异步请求并获取响应
            var response = await client.ExecuteAsync(request);
            // 检查响应状态
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse>(response.Content); // 如果成功，反序列化响应内容为 ApiResponse 对象
            else
                return new ApiResponse() // 如果失败，返回包含错误信息的 ApiResponse 对象
                {
                    Status = false,
                    Result = null,
                    Message = response.ErrorMessage
                };
        }

        /// <summary>
        /// 执行一个返回实体的 API 请求，返回 ApiResponse<T>。
        /// </summary>
        /// <typeparam name="T">返回的实体类型。</typeparam>
        /// <param name="baseRequest">包含请求信息的 BaseRequest 对象。</param>
        /// <returns>表示异步操作的任务，包含 ApiResponse<T>。</returns>
        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            // 创建 RestRequest 实例，指定请求方法
            var request = new RestRequest(baseRequest.Method.ToString());
            request.AddHeader("Content-Type", baseRequest.ContentType); // 添加请求头

            // 如果请求参数不为空，将其序列化为 JSON 并添加到请求体
            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);

            // 发送异步请求并获取响应
            var response = await client.ExecuteAsync(request);
            // 检查响应状态
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content); // 如果成功，反序列化响应内容为 ApiResponse<T> 对象
            else
                return new ApiResponse<T>() // 如果失败，返回包含错误信息的 ApiResponse<T> 对象
                {
                    Status = false,
                    Message = response.ErrorMessage
                };
        }
    }
}
