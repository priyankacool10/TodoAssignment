namespace AdFormTodoApi.v1.DTOs
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long TodoListId { get; set; }
        public long LabelId { get; set; }
    }
}
