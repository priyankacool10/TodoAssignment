using AdFormTodoApi.Core.Models;
using AdFormTodoApi.v1.DTOs;
using AutoMapper;

namespace AdFormTodoApi.v1.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Model to DTO
            CreateMap<TodoItem, TodoItemDTO>();
            CreateMap<TodoItem, SaveTodoItemDTO>();
            CreateMap<TodoList, TodoListDTO>();
            CreateMap<Label, LabelDTO>();
            // DTO to Model
            CreateMap<TodoItemDTO, TodoItem>();
            CreateMap<SaveTodoItemDTO, TodoItem>();
            CreateMap<TodoListDTO, TodoList>();
            CreateMap<LabelDTO, Label>();
        }
    }
}
