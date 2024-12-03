using Memo.Shared.Contact; // 引用共享的 API 响应模型
using Memo.Shared.Parameters; // 引用查询参数模型
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Service
{
    /// <summary>
    /// 定义基本服务接口，提供 CRUD 操作的通用方法。
    /// 该接口适用于任何实体类型，并提供对实体的添加、更新、删除和检索功能。
    /// </summary>
    /// <typeparam name="TEntity">服务操作的实体类型，必须是一个类。</typeparam>
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponse<TEntity>> AddAsync(TEntity entity);
        Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id);
        Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter);
    }
}
