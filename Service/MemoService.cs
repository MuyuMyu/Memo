using Memo.Shared.Dtos;  // 引用 MemoDto 类型
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Service
{
    /// <summary>
    /// 提供备忘录服务的实现，继承自 BaseService。
    /// </summary>
    public class MemoService : BaseService<MemoDto>, IMemoService
    {
        /// <summary>
        /// MemoService 构造函数，初始化基类 BaseService。
        /// </summary>
        /// <param name="client">HTTP REST 客户端实例。</param>
        public MemoService(HttpRestClient client) : base(client, "Memo")
        {
            // 通过调用基类构造函数传入 HTTP 客户端和服务名称 "Memo"
        }
    }
}
