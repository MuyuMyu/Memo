using Memo.Shared.Dtos;  // 引用用户数据传输对象
using Memo.Shared.Contact; // 引用 ApiResponse 类型
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Service
{
    /// <summary>
    /// 实现 ILoginService 接口，提供用户登录和注册功能的服务。
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient client; // HTTP REST 客户端，用于发送请求
        private readonly string serviceName = "Login"; // 服务名称

        /// <summary>
        /// LoginService 构造函数，初始化 HttpRestClient 实例。
        /// </summary>
        /// <param name="client">HTTP REST 客户端实例。</param>
        public LoginService(HttpRestClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 用户登录方法，向服务端发送登录请求。
        /// </summary>
        /// <param name="user">用户数据传输对象，包含用户凭据。</param>
        /// <returns>异步返回 ApiResponse 对象，包含操作结果。</returns>
        public async Task<ApiResponse> Login(UserDto user)
        {
            // 创建基础请求对象
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post; // 请求方法为 POST
            request.Route = $"api/{serviceName}/Login"; // 设置请求路径
            request.Parameter = user; // 设置请求参数为用户对象

            // 发送请求并返回响应
            return await client.ExecuteAsync(request);
        }

        /// <summary>
        /// 用户注册方法，向服务端发送注册请求。
        /// </summary>
        /// <param name="user">用户数据传输对象，包含注册信息。</param>
        /// <returns>异步返回 ApiResponse 对象，包含操作结果。</returns>
        public async Task<ApiResponse> Resgiter(UserDto user)
        {
            // 创建基础请求对象
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post; // 请求方法为 POST
            request.Route = $"api/{serviceName}/Resgiter"; // 设置请求路径
            request.Parameter = user; // 设置请求参数为用户对象

            // 发送请求并返回响应
            return await client.ExecuteAsync(request);
        }
    }
}
