using AdFormTodoApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormTodoApi.Core.Repositories
{
    public interface ILabelRepository : IRepository<Label>
    {
        Task<IEnumerable<Label>> GetAllLabelAsync();
        Task<Label> GetLabelByIdAsync(long id);

    }
}
