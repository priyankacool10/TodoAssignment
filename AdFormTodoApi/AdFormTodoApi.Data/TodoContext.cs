using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AdFormTodoApi.Data
{
    public class TodoContext:DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new TodoItemConfiguration());

            builder
                .ApplyConfiguration(new TodoListConfiguration());

            builder
                .ApplyConfiguration(new LabelConfiguration());

            builder
                .ApplyConfiguration(new UserConfiguration());

        }

    }
}
