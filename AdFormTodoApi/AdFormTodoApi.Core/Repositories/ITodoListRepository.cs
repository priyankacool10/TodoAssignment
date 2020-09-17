using AdFormTodoApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormTodoApi.Core.Repositories
{
    public interface ITodoListRepository : IRepository<TodoList>
    {
        Task<IEnumerable<TodoList>> GetAllTodoListAsync(PagingOptions op);
        Task<TodoList> GetTodoListByIdAsync(long id);
        Task<IEnumerable<TodoList>> FindTodoListBySearch(SearchFilter filter);


    }
}
