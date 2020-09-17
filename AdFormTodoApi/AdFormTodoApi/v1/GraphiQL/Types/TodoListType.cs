using AdFormTodoApi.Core.Models;
using HotChocolate.Types;
namespace AdFormTodoApi.v1.GraphiQL.Types
{
    public class TodoListType : ObjectType<TodoList>
    {
        protected override void Configure(IObjectTypeDescriptor<TodoList> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Description).Type<StringType>();
            
        }
    }
}
