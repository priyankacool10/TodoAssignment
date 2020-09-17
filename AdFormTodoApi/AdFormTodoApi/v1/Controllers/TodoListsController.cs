using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Services;
using AdFormTodoApi.v1.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AdFormTodoApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly IMapper _mapper;
        public TodoListsController(ITodoListService todoListService, IMapper mapper)
        {
            _todoListService = todoListService;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get List of All TodoList
        /// </summary>
        /// <param></param>
        /// <returns>All TodoList</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists([FromQuery]PagingOptions op)
        {
            var todoList = await _todoListService.GetAllTodoList(op);
            var todoListDTO = _mapper.Map<IEnumerable<TodoList>, IEnumerable<TodoListDTO>>(todoList);
            if (todoList == null)
            {
                return NotFound(new { message = "TodoList does not exists" });
            }
            return Ok(todoListDTO);
        }


        /// <summary>
        /// Method to get TodoList based on given ID
        /// </summary>
        /// <param name="id">Id of TodoList</param>
        /// <returns>TodoList</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoList>> GetTodoList(long id)
        {
            var todoList = await _todoListService.GetTodoListById(id);
            var todoListDTO = _mapper.Map<TodoList,TodoListDTO>(todoList);

            if (todoList == null)
            {
                return NotFound(new { message = "Todo List does not exists" });
            }
            return Ok(todoListDTO);
        }

        /// <summary>
        /// Method to search TodoList based on Search Filter
        /// </summary>
        /// <param name="filter">Filter for Search</param>
        /// <returns>TodoListDTO</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TodoItem), (int)HttpStatusCode.OK)]
        [Route("SearchTodoList")]
        public async Task<ActionResult<TodoListDTO>> SearchTodoList([FromQuery] SearchFilter filter)
        {
            var result = await _todoListService.SearchTodoList(filter);
            var final = _mapper.Map<IEnumerable<TodoList>, IEnumerable<TodoListDTO>>(result);
            return Ok(final);
        }


        /// <summary>
        /// Method to Update TodoList based on given ID
        /// </summary>
        /// <param name="id,todoListDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoList(long id, TodoListDTO todoListDTO)
        {
            var todoList = _mapper.Map<TodoListDTO, TodoList>(todoListDTO);
            if (id != todoList.Id)
            {
                return BadRequest();
            }

            await _todoListService.UpdateTodoList(id, todoList);
            return NoContent();
        }

        /// <summary>
        /// Method to create a TodoList
        /// </summary>
        /// <param name="todoListDTO"></param>
        /// <returns>TodoList</returns>
       [HttpPost]
        public async Task<ActionResult<TodoList>> PostTodoList(TodoListDTO todoListDTO)
        {
            var todoList = _mapper.Map<TodoListDTO,TodoList>(todoListDTO);
            if (todoList.Description == null)
                return BadRequest(new { message = "TodoList Description mandatory" });

            await _todoListService.CreateTodoList(todoList);
            return CreatedAtAction("GetTodoList", new { id = todoList.Id }, todoList);
        }

        /// <summary>
        /// Method to delete TodoList of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoList>> DeleteTodoList(long id)
        {
            await _todoListService.DeleteTodoList(id);
            return NoContent();
        }

    }
}
