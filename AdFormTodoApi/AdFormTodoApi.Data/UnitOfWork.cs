using AdFormTodoApi.Core;
using AdFormTodoApi.Core.Repositories;
using AdFormTodoApi.Data.Repositories;
using System.Threading.Tasks;

namespace AdFormTodoApi.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly TodoContext _context;
        private TodoItemRepository _todoItemRepository;
        private TodoListRepository _todoListRepository;
        private LabelRepository _labelRepository;

        public UnitOfWork(TodoContext context)
        {
            this._context = context;
        }

        public ITodoItemRepository TodoItems => _todoItemRepository = _todoItemRepository ?? new TodoItemRepository(_context);

        public ITodoListRepository TodoLists => _todoListRepository = _todoListRepository ?? new TodoListRepository(_context);

        public ILabelRepository Labels => _labelRepository = _labelRepository ?? new LabelRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}