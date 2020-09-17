using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Services;
using AdFormTodoApi.v1.DTOs;
using AutoMapper;
using System.Threading.Tasks;

namespace AdFormTodoApi.v1.GraphiQL.Mutation
{
    public class Mutation
    {
        private readonly ITodoItemService _todoItemService;
        private readonly ITodoListService _todoListService;
        private readonly ILabelService _labelService;
        private readonly IMapper _mapper;
        public Mutation(ITodoItemService todoItemService, ITodoListService todoListService, ILabelService labelService, IMapper mapper) 
        {
            _todoItemService = todoItemService;
            _todoListService = todoListService;
            _labelService = labelService;
            _mapper = mapper;
        }
        public async Task<Label> AddLabel(LabelDTO label)
        {
            var labelInput = _mapper.Map<LabelDTO,Label>(label);
            return await _labelService.CreateLabel(labelInput);
        }

        public async Task<TodoItem> AddTodoItem(TodoItemDTO todoItem)
        {
            var todoItemInput = _mapper.Map<TodoItemDTO, TodoItem>(todoItem);
            return await _todoItemService.CreateTodoItem(todoItemInput);
        }
        public async Task<TodoList> AddTodoList(TodoListDTO todoList)
        {
            var todoListInput = _mapper.Map<TodoListDTO, TodoList>(todoList);
            return await _todoListService.CreateTodoList(todoListInput);
        }
    }
}
