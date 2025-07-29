using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.DTOs.Request;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService) 
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Creates new item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTodoItemAsync(CreateTodoItemRequest request)
        {
            var response = await _todoService.CreateTodoItemAsync(request);
            return Ok(response);

        }

        /// <summary>
        ///Gets the item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTodoItemAsync(Guid id)
        {
            var response = await _todoService.GetTodoItemAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Gets the list of all todo items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _todoService.GetTodoListItemAsync();
            return Ok(response);
        }

        /// <summary>
        /// Updates the item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPut]
        public async Task<IActionResult> UpdateTodoItemAsync(UpdateTodoItemRequest request)
        {
            var response = await _todoService.UpdateTodoItemAsync(request);
            return Ok(response);

        }

        /// <summary>
        /// Deletes the TodoItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTodoItemAsync(Guid id)
        {
            var response = await _todoService.DeleteTodoItemAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Marks the item as complete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("markcomplete/{id}")]
        public async Task<IActionResult> MarkCompleteTodoItemAsync(Guid id)
        {
            var response = await _todoService.MarkTodoItemAsCompleteAsync(id);
            return Ok(response);
        }
    }
}
