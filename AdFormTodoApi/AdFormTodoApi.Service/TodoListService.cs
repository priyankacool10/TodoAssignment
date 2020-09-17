using AdFormTodoApi.Core;
using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormTodoApi.Service
{
    public class TodoListService : ITodoListService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TodoListService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<TodoList> CreateTodoList(TodoList newTodoList)
        {
            newTodoList.CreatedDate = DateTime.UtcNow;
            await _unitOfWork.TodoLists.AddAsync(newTodoList);
            await _unitOfWork.CommitAsync();
            return newTodoList;
        }

        public async Task DeleteTodoList(long id)
        {
            var todoItemToBeDeleted = await _unitOfWork.TodoLists
                .GetTodoListByIdAsync(id);
            _unitOfWork.TodoLists.Remove(todoItemToBeDeleted);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TodoList>> GetAllTodoList(PagingOptions op)
        {
            return await _unitOfWork.TodoLists
                .GetAllTodoListAsync(op);
        }

        public async Task<TodoList> GetTodoListById(long id)
        {
            return await _unitOfWork.TodoLists
                .GetTodoListByIdAsync(id);
        }

        public async Task UpdateTodoList(long id, TodoList newTodoList)
        {
            var todoItemToBeUpdated = await _unitOfWork.TodoLists
                .GetTodoListByIdAsync(id);
            todoItemToBeUpdated.Description = newTodoList.Description;
            todoItemToBeUpdated.UpdatedDate = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TodoList>> SearchTodoList(SearchFilter filter)
        {
            return await _unitOfWork.TodoLists.FindTodoListBySearch(filter);

        }
    }

}
