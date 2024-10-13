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
        /// <summary>
        /// 异步添加新的实体。
        /// </summary>
        /// <param name="entity">要添加的实体对象。</param>
        /// <returns>表示异步操作的任务，包含添加后的 ApiResponse<TEntity> 对象。</returns>
        Task<ApiResponse<TEntity>> AddAsync(TEntity entity);

        /// <summary>
        /// 异步更新现有实体。
        /// </summary>
        /// <param name="entity">要更新的实体对象。</param>
        /// <returns>表示异步操作的任务，包含更新后的 ApiResponse<TEntity> 对象。</returns>
        Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// 异步删除指定 ID 的实体。
        /// </summary>
        /// <param name="id">要删除的实体的 ID。</param>
        /// <returns>表示异步操作的任务，包含 ApiResponse 对象。</returns>
        Task<ApiResponse> DeleteAsync(int id);

        /// <summary>
        /// 异步获取指定 ID 的实体，如果不存在则返回默认值。
        /// </summary>
        /// <param name="id">要检索的实体的 ID。</param>
        /// <returns>表示异步操作的任务，包含 ApiResponse<TEntity> 对象。</returns>
        Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id);

        /// <summary>
        /// 异步获取所有实体，并支持分页和搜索。
        /// </summary>
        /// <param name="parameter">查询参数，包括分页信息和搜索条件。</param>
        /// <returns>表示异步操作的任务，包含 ApiResponse<PagedList<TEntity>> 对象。</returns>
        Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter);
    }
}
