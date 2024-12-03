using Memo.Shared.Dtos;  // 引用用户数据传输对象
using Memo.Shared.Contact; // 引用 ApiResponse 类型
using Memo.Context;  // 引用 MemoContext
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Memo.Service
{
    /// <summary>
    /// 实现 ILoginService 接口，提供用户登录和注册功能的服务。
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly MemoContext context; // 数据库上下文，用于访问 SQLite 数据库

        /// <summary>
        /// LoginService 构造函数，初始化 MemoContext 实例。
        /// </summary>
        /// <param name="context">数据库上下文实例。</param>
        public LoginService(MemoContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 用户登录方法，检查用户凭据并返回登录结果。
        /// </summary>
        /// <param name="user">用户数据传输对象，包含用户凭据。</param>
        /// <returns>异步返回 ApiResponse 对象，包含操作结果。</returns>
        public async Task<ApiResponse> Login(UserDto user)
        {
            try
            {
                // 查找数据库中是否存在该用户
                var existingUser = await context.User
                    .Where(u => u.Account == user.Account && u.PassWord == user.PassWord)
                    .FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return new ApiResponse { Status = false, Message = "Invalid username or password" };
                }

                return new ApiResponse { Status = true, Message = "Login successful" };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Status = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// 用户注册方法，向数据库添加新用户。
        /// </summary>
        /// <param name="user">用户数据传输对象，包含注册信息。</param>
        /// <returns>异步返回 ApiResponse 对象，包含操作结果。</returns>
        public async Task<ApiResponse> Register(UserDto user)
        {
            try
            {
                // 检查用户名是否已经存在
                var existingUser = await context.User
                    .Where(u => u.UserName == user.UserName)
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return new ApiResponse { Status = false, Message = "Username already exists" };
                }

                // 创建新用户并保存到数据库
                var newUser = new User
                {
                    Account = user.Account,
                    UserName = user.UserName,
                    PassWord = user.PassWord
                };

                await context.User.AddAsync(newUser);
                await context.SaveChangesAsync();

                return new ApiResponse { Status = true, Message = "Registration successful" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return new ApiResponse { Status = false, Message = ex.Message };
            }
        }
    }
}
