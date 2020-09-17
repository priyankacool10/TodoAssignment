using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormTodoApi.v1.GraphiQL.Queries
{
    /// <summary>
    /// Queries for GraphQL
    /// </summary>
    public class Query 
    {
        private readonly ITodoItemService _todoItemService;
        private readonly ITodoListService _todoListService;
        private readonly ILabelService _labelService;
        public Query(ITodoItemService todoItemService, ITodoListService todoListService, ILabelService labelService)
        {
            _todoItemService = todoItemService;
            _todoListService = todoListService;
            _labelService = labelService;
        }

        /// <summary>
        /// Query to get all TodoItems
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public Task<IEnumerable<TodoItem>> TodoItems => _todoItemService.GetAllTodoItem(new PagingOptions() { PageNumber = 1, PageSize = 5 });
        
        /// <summary>
        /// Query to get TodoItem by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TodoItem> TodoItem(long id) => _todoItemService.GetTodoItemById(id);

        /// <summary>
        /// Query to get all TodoList
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public Task<IEnumerable<TodoList>> TodoLists => _todoListService.GetAllTodoList(new PagingOptions() { PageNumber = 1, PageSize = 5 });

        /// <summary>
        /// Query to get TodoList by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TodoList> TodoList(long id) => _todoListService.GetTodoListById(id);

        /// <summary>
        /// Query to get all Labels
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public Task<IEnumerable<Label>> Labels => _labelService.GetAllLabel();
       
        /// <summary>
        /// Query to get Label by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Task<Label> Label(long id) => _labelService.GetLabelById(id);


    }
}