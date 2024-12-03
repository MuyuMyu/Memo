using Memo.Shared.Contact;
using Memo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memo.Context;  // 引用 MemoContext
using Microsoft.EntityFrameworkCore;

namespace Memo.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly MemoContext context; // 用于访问 SQLite 数据库

        public BaseService(MemoContext context)
        {
            this.context = context; // 初始化 MemoContext
        }

        public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();
                return new ApiResponse<TEntity> { Status = true, Result = entity };
            }
            catch (Exception ex)
            {
                return new ApiResponse<TEntity> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var entity = await context.Set<TEntity>().FindAsync(id);
                if (entity == null)
                    return new ApiResponse { Status = false, Message = "Entity not found" };

                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
                return new ApiResponse { Status = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var query = context.Set<TEntity>().AsQueryable();

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

                var pagedList = new PagedList<TEntity>
                {
                    Items = items,
                    TotalCount = totalItems,
                    PageIndex = parameter.PageIndex,
                    PageSize = parameter.PageSize
                };

                return new ApiResponse<PagedList<TEntity>> { Status = true, Result = pagedList };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PagedList<TEntity>> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            try
            {
                var entity = await context.Set<TEntity>().FindAsync(id);
                if (entity == null)
                    return new ApiResponse<TEntity> { Status = false, Message = "Entity not found" };

                return new ApiResponse<TEntity> { Status = true, Result = entity };
            }
            catch (Exception ex)
            {
                return new ApiResponse<TEntity> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            try
            {
                context.Set<TEntity>().Update(entity);
                await context.SaveChangesAsync();
                return new ApiResponse<TEntity> { Status = true, Result = entity };
            }
            catch (Exception ex)
            {
                return new ApiResponse<TEntity> { Status = false, Message = ex.Message };
            }
        }
    }
}
