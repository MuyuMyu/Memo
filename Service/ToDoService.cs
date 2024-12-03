using Memo.Shared.Contact; // 引用 ApiResponse 类型
using Memo.Shared.Dtos;    // 引用 ToDoDto 和 SummaryDto 类型
using Memo.Shared.Parameters; // 引用 ToDoParameter 类型
using Memo.Context; // 引用 MemoContext
using Microsoft.EntityFrameworkCore; // 引用 EF Core
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Memo.Service
{
    /// <summary>
    /// 提供待办事项服务的实现。
    /// </summary>
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly MemoContext context; // 数据库上下文实例

        /// <summary>
        /// ToDoService 构造函数，初始化数据库上下文。
        /// </summary>
        /// <param name="context">数据库上下文实例。</param>
        public ToDoService(MemoContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<ApiResponse<ToDo>> AddAsync(ToDo entity)
        {
            try
            {
                await context.Set<ToDo>().AddAsync(entity);
                await context.SaveChangesAsync();
                return new ApiResponse<ToDo> { Status = true, Result = entity };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ToDo> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse<ToDo>> UpdateAsync(ToDo entity)
        {
            try
            {
                context.Set<ToDo>().Update(entity);
                await context.SaveChangesAsync();
                return new ApiResponse<ToDo> { Status = true, Result = entity };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ToDo> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var entity = await context.Set<ToDo>().FindAsync(id);
                if (entity == null)
                    return new ApiResponse { Status = false, Message = "Entity not found" };

                context.Set<ToDo>().Remove(entity);
                await context.SaveChangesAsync();
                return new ApiResponse { Status = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Status = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// 获取待办事项的分页列表，支持过滤参数。
        /// </summary>
        /// <param name="parameter">包含分页和过滤条件的参数。</param>
        /// <returns>返回 ApiResponse 包含待办事项分页列表。</returns>
        public async Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter parameter)
        {
            try
            {
                var query = context.ToDo.AsQueryable();

                // 应用搜索和过滤条件
                if (!string.IsNullOrEmpty(parameter.Search))
                {
                    query = query.Where(t => t.Title.Contains(parameter.Search));
                }

                if (parameter.Status.HasValue)
                {
                    query = query.Where(t => t.Status == parameter.Status.Value);
                }

                // 获取总记录数
                var totalCount = await query.CountAsync();

                // 分页
                var items = await query
                    .Skip((parameter.PageIndex - 1) * parameter.PageSize)
                    .Take(parameter.PageSize)
                    .Select(t => new ToDoDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Status = t.Status,
                    })
                    .ToListAsync();

                var pagedList = new PagedList<ToDoDto>
                {
                    Items = items,
                    PageIndex = parameter.PageIndex,
                    PageSize = parameter.PageSize,
                    TotalCount = totalCount
                };

                return new ApiResponse<PagedList<ToDoDto>> { Status = true, Result = pagedList };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PagedList<ToDoDto>> { Status = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// 获取待办事项的统计信息。
        /// </summary>
        /// <returns>返回 ApiResponse 包含统计信息的对象。</returns>
        public async Task<ApiResponse<SummaryDto>> SummaryAsync()
        {
            try
            {
                var total = await context.ToDo.CountAsync();
                var completed = await context.ToDo.CountAsync(t => t.Status == 1);
                var pending = (total - completed);

                var summary = new SummaryDto
                {
                    Sum = total,
                    CompletedCount = completed,
                };

                return new ApiResponse<SummaryDto> { Status = true, Result = summary };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SummaryDto> { Status = false, Message = ex.Message };
            }
        }
    }
}
