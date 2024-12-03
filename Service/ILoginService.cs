using Memo.Shared.Dtos; // 引用用户数据传输对象 (DTO)
using Memo.Shared.Contact; // 引用共享的 API 响应模型
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memo.Context;

namespace Memo.Service
{
    /// <summary>
    /// 定义登录服务接口，用于处理用户登录和注册操作。
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// 异步处理用户登录请求。
        /// </summary>
        /// <param name="user">要登录的用户信息，包括用户名和密码。</param>
        /// <returns>表示异步操作的任务，包含 ApiResponse 对象，指示登录结果。</returns>
        Task<ApiResponse> Login(UserDto user);

        /// <summary>
        /// 异步处理用户注册请求。
        /// </summary>
        /// <param name="user">要注册的用户信息，包括用户名和密码。</param>
        /// <returns>表示异步操作的任务，包含 ApiResponse 对象，指示注册结果。</returns>
        Task<ApiResponse> Register(UserDto user);
    }
}
