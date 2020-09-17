using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Services;
using AdFormTodoApi.v1.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AdFormTodoApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;
        public TodoItemsController(ITodoItemService todoItemService, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _mapper = mapper;


        }

        /// <summary>
        /// Method to get List of All TodoItems
        /// </summary>
        /// <param></param>
        /// <returns>List of TodoItems</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems([FromQuery]PagingOptions options)
        {
           var todoItems= await _todoItemService.GetAllTodoItem(options);
           var todoItemsDTO = _mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemDTO>>(todoItems);
            if (todoItems == null)
            {
                return NotFound(new { message = "TodoItem does not exists" });
            }
            return Ok(todoItemsDTO);
            
        } 

        /// <summary>
        /// Method to get TodoItem based on given ID
        /// </summary>
        /// <param name="id">Id of TodoItem</param>
        /// <returns>TodoItem</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemService.GetTodoItemById(id);
            var todoItemDTO = _mapper.Map<TodoItem,TodoItemDTO>(todoItem);

            if (todoItem == null)
            {
                return NotFound(new { message = "Todo Item does not exists" });
            }
            return Ok(todoItemDTO);
        }

        /// <summary>
        /// Method to search TodoItem based on Search Filter
        /// </summary>
        /// <param name="id">Id of TodoItem</param>
        /// <returns>TodoItem</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TodoItem), (int)HttpStatusCode.OK)]
        [Route("SearchToDoItem")]
        public async Task<ActionResult<TodoItemDTO>> SearchTodoItem([FromQuery] SearchFilter filter)
        {
            var result = await _todoItemService.SearchTodoItem(filter);
            var final = _mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemDTO>>(result);
            return Ok(final);
        }


        /// <summary>
        /// Method to Update TodoItem based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItemDTO, TodoItem>(todoItemDTO);
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            await _todoItemService.UpdateTodoItem(id,todoItem);
            return NoContent();
        }

        /// <summary>
        /// Method to Patch TodoItem based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("PatchToDoItem")]
        public async Task<IActionResult> JsonPatchTodoItem(long id, [FromBody] JsonPatchDocument<TodoItem> todoItem)
        {
            await _todoItemService.PatchTodoItem(id, todoItem);
            return NoContent();
            
        }
        /// <summary>
        /// Method to create a TodoItem
        /// </summary>
        /// <param name="todoItemDTO"></param>
        /// <returns>TodoItem</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoItem>> PostTodoItem(SaveTodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<SaveTodoItemDTO, TodoItem>(todoItemDTO);
            if (todoItem.Description == null) 
                return BadRequest(new {message="TodoItem Description mandatory" });
            
            await _todoItemService.CreateTodoItem(todoItem);
            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        /// <summary>
        /// Method to delete TodoItem of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoItemService.DeleteTodoItem(id);
            return NoContent();
        }

    }
}
