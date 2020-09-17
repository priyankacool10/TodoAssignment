using AdFormTodoApi.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace AdFormTodoApi.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItems { get; }
        ITodoListRepository TodoLists { get; }
        ILabelRepository Labels { get;  }
        Task<int> CommitAsync();
    }
}
