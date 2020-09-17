using AdFormTodoApi.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdFormTodoApi.Core.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAllTodoItem(PagingOptions op);
        Task<TodoItem> GetTodoItemById(long id);
        Task<IEnumerable<TodoItem>> SearchTodoItem(SearchFilter filter);
        Task<IEnumerable<TodoItem>> GetTodoItemByTodoListId(long todoListId);
        Task<TodoItem> CreateTodoItem(TodoItem newTodoItem);
        Task UpdateTodoItem(long todoItemId, TodoItem todoItemToBeUpdated);
        Task PatchTodoItem(long id, JsonPatchDocument<TodoItem> todoItem);
        Task DeleteTodoItem(long id);
    }
}
