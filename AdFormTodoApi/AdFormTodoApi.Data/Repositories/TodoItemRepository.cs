using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdFormTodoApi.Data.Repositories
{
    class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext context)
            : base(context)
        { }

        public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync(PagingOptions op)
        {
            return await TodoContext.TodoItems
                .Include(m => m.TodoList)
                .Skip((op.PageNumber - 1) * op.PageSize)
                .Take(op.PageSize)
                .ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(long id)
        {
            return await TodoContext.TodoItems
                .Include(m => m.TodoList)
                .SingleOrDefaultAsync(m => m.Id == id); ;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemByTodoListIdAsync(long todoListId)
        {
            return await TodoContext.TodoItems
                .Include(m => m.TodoList)
                .Where(m => m.TodoListId == todoListId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> FindTodoItemBySearch(SearchFilter filter)
        {
            return await TodoContext.TodoItems
                .Include(m => m.TodoList)
                .Where(m=>m.Description.Contains(filter.ItemName))
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        private TodoContext TodoContext
        {
            get { return Context as TodoContext; }
        }
    }
}