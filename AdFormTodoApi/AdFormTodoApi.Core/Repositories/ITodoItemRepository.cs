using AdFormTodoApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormTodoApi.Core.Repositories
{
    public interface ITodoItemRepository :IRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync(PagingOptions op);
        Task<TodoItem> GetTodoItemByIdAsync(long id);
        Task<IEnumerable<TodoItem>> GetTodoItemByTodoListIdAsync(long todoListId);
        Task<IEnumerable<TodoItem>> FindTodoItemBySearch(SearchFilter filter);
    }
}
