using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Common
{
    // 定义一个静态类，用于管理应用程序中的全局会话信息
    public static class AppSession
    {
        // 静态属性，用于存储当前登录用户的用户名
        public static string UserName { get; set; }
    }
}
