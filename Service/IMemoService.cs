using Memo.Shared.Dtos; // 引用 MemoDto 数据传输对象
using Memo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memo.Shared.Contact;
using Memo.Shared.Parameters;

namespace Memo.Service
{
    /// <summary>
    /// 定义备忘录服务接口，继承自基础服务接口。
    /// 提供对备忘录 (Memo) 的 CRUD 操作。
    /// </summary>
    public interface IMemoService : IBaseService<MemoDto>
    {
        Task<ApiResponse<Context.Memo>> AddAsync(Context.Memo entity);
        Task<ApiResponse<Context.Memo>> UpdateAsync(Context.Memo entity);
        Task<ApiResponse> DeleteAsync(int id);
        // 该接口继承自 IBaseService<MemoDto>，并不需要额外定义任何方法
        // 因此，所有的 CRUD 操作 (添加、更新、删除、获取等) 将自动继承自 IBaseService

        Task<ApiResponse<PagedList<Context.Memo>>> GetAllAsync(QueryParameter parameter);
    }
}
