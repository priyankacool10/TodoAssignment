using AdFormTodoApi.Core;
using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdFormTodoApi.Service
{
    public class LabelService:ILabelService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LabelService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Label> CreateLabel(Label newLabel)
        {
            newLabel.CreatedDate = DateTime.UtcNow;
            await _unitOfWork.Labels.AddAsync(newLabel);
            await _unitOfWork.CommitAsync();
            return newLabel;
        }

        public async Task DeleteLabel(long id)
        {
            var labelToBeDeleted = await _unitOfWork.Labels
                .GetLabelByIdAsync(id);
            _unitOfWork.Labels.Remove(labelToBeDeleted);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Label>> GetAllLabel()
        {
            return await _unitOfWork.Labels
                .GetAllLabelAsync();
        }

        public async Task<Label> GetLabelById(long id)
        {
            return await _unitOfWork.Labels
                .GetLabelByIdAsync(id);
        }

    }

}
