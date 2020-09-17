using AdFormTodoApi.Core.Models;
using HotChocolate.Types;

namespace AdFormTodoApi.v1.GraphiQL.Types
{
    public class TodoItemType : ObjectType<TodoItem>
    {
        protected override void Configure(IObjectTypeDescriptor<TodoItem> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Description).Type<StringType>();
            descriptor.Field(a => a.LabelId).Type<LongType>();
            descriptor.Field(a => a.TodoListId).Type<LongType>();
           // descriptor.Field<TodoListResolver>(t => t.GetAuthor(default, default));
        }
        
    }
}
