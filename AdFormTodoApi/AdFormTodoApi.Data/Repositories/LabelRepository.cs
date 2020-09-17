using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormTodoApi.Data.Repositories
{
    class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(TodoContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Label>> GetAllLabelAsync()
        {
            return await TodoContext.Labels
                .ToListAsync();
        }

        public async Task<Label> GetLabelByIdAsync(long id)
        {
            return await TodoContext.Labels
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        private TodoContext TodoContext
        {
            get { return Context as TodoContext; }
        }
    }
}