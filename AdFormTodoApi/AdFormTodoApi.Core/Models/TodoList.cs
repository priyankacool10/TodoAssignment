using System.Collections.Generic;

namespace AdFormTodoApi.Core.Models
{
    public class TodoList : Trackable
    {
        public TodoList() 
        {
            TodoItems = new List<TodoItem>();

        }
        public long Id { get; set; }
        
        public string Description { get; set; }
        public IList<TodoItem> TodoItems { get; set; }
    }

}

