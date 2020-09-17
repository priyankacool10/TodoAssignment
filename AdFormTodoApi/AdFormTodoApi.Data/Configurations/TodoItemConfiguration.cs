using AdFormTodoApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdFormTodoApi.Data.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(m => m.TodoList)
                .WithMany(a => a.TodoItems)
                .HasForeignKey(m => m.TodoListId);

            builder
                .ToTable("TodoItems");
        }
    }
}
