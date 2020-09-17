using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Services;
using AdFormTodoApi.v1.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AdFormTodoApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _labelService;
        private readonly IMapper _mapper;

        public LabelsController(ILabelService labelService, IMapper mapper)
        {
            _labelService = labelService;
            _mapper = mapper;
        }


        /// <summary>
        /// Method to get List of All Labels
        /// </summary>
        /// <param></param>
        /// <returns>List of Label</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Label>>> GetLabels()
        {
            var labels = await _labelService.GetAllLabel();
            var labelDTO = _mapper.Map<IEnumerable<Label>, IEnumerable<LabelDTO>>(labels);
            if (labels == null)
            {
                return NotFound(new { message = "Labels does not exist" });
            }
            return Ok(labelDTO);
        }

        /// <summary>
        /// Method to get Label based on given ID
        /// </summary>
        /// <param name="id">Id of Label</param>
        /// <returns>Label</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Label>> GetLabel(long id)
        {
            var label = await _labelService.GetLabelById(id);
            var labelDTO = _mapper.Map<Label, LabelDTO>(label);
            if (label == null)
            {
                return NotFound(new { message = "Label with id : {0} does not exist", id });
            }
            return Ok(labelDTO);
        }

        /// <summary>
        /// Method to create a Label
        /// </summary>
        /// <param name="labelDTO"></param>
        /// <returns>Label</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Label>> PostLabel([FromBody]LabelDTO labelDTO)
        {
            if (labelDTO.Name == null)
                return BadRequest(new { message = "Label Name mandatory" });
            var label = _mapper.Map<LabelDTO, Label>(labelDTO);
            await _labelService.CreateLabel(label);
            return CreatedAtAction("GetLabel", new { id = label.Id }, label);
        }

        /// <summary>
        /// Method to delete Label of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Label>> DeleteLabel(long id)
        {
            await _labelService.DeleteLabel(id);
            return NoContent();
        }

    }
}
