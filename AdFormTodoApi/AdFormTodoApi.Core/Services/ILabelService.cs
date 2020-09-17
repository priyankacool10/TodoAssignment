using AdFormTodoApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormTodoApi.Core.Services
{
    public interface ILabelService
    {
        Task<IEnumerable<Label>> GetAllLabel();
        Task<Label> GetLabelById(long id);
        Task<Label> CreateLabel(Label newLabel);
        Task DeleteLabel(long id);
    }

}
