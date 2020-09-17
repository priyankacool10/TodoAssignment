namespace AdFormTodoApi.v1.DTOs
{
    public class SaveTodoItemDTO
    {
        public string Description { get; set; }
        public long TodoListId { get; set; }
        public long LabelId { get; set; }
    }
}
