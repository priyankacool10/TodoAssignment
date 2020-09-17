namespace AdFormTodoApi.Core.Models
{
    public class TodoItem :Trackable
    {
        public long Id { get; set; }
        public string Description { get; set; }
       
        public long LabelId { get; set; }
       
        public virtual Label Label { get; set; }
        public long TodoListId { get; set; }

        public virtual TodoList TodoList { get; set; }



    }
}
