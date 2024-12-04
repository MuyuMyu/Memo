using Memo.Context;
using Memo.Shared.Contact;
using Memo.Shared.Dtos;  // 引用 MemoDto 类型
using Memo.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
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

        private readonly MemoContext context; // 数据库上下文实例
        /// <summary>
        /// MemoService 构造函数，初始化基类 BaseService。
        /// </summary>
        /// <param name="context">数据库客户端实例。</param>
        public MemoService(MemoContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<ApiResponse<Context.Memo>> AddAsync(Context.Memo entity)
        {
            try
            {
                await context.Set<Context.Memo>().AddAsync(entity);
                await context.SaveChangesAsync();
                return new ApiResponse<Context.Memo> { Status = true, Result = entity };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Context.Memo> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var entity = await context.Set<Context.Memo>().FindAsync(id);
                if (entity == null)
                    return new ApiResponse { Status = false, Message = "Entity not found" };

                context.Set<Context.Memo>().Remove(entity);
                await context.SaveChangesAsync();
                return new ApiResponse { Status = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse<Context.Memo>> UpdateAsync(Context.Memo entity)
        {
            try
            {
                context.Set<Context.Memo>().Update(entity);
                await context.SaveChangesAsync();
                return new ApiResponse<Context.Memo> { Status = true, Result = entity };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Context.Memo> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse<PagedList<Context.Memo>>> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var query = context.Set<Context.Memo>().AsQueryable();

                // 搜索处理
                if (!string.IsNullOrEmpty(parameter.Search))
                {
                    query = query.Where(e => e.ToString().Contains(parameter.Search)); // 假设所有实体都有一个 ToString() 方法
                }

                // 分页处理
                var totalItems = await query.CountAsync();
                var items = await query.Skip((parameter.PageIndex - 1) * parameter.PageSize)
                                       .Take(parameter.PageSize)
                                       .ToListAsync();

                var pagedList = new PagedList<Context.Memo>
                {
                    Items = items,
                    TotalCount = totalItems,
                    PageIndex = parameter.PageIndex,
                    PageSize = parameter.PageSize
                };

                return new ApiResponse<PagedList<Context.Memo>> { Status = true, Result = pagedList };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PagedList<Context.Memo>> { Status = false, Message = ex.Message };
            }
        }
    }
}
