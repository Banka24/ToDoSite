using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Contracts;
using ToDoAPI.Models.Entities;
using ToDoAPI.Models.Requests;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController([FromServices] IToDoService service, ILogger<ToDoController> logger) : ControllerBase
    {
        private readonly IToDoService _service = service;
        private readonly ILogger<ToDoController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllToDo(CancellationToken token)
        {
            var list = await _service.GetAllToDoAsync(token);
            return Ok(list);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetToDo(int id, CancellationToken token)
        {
            var todo = await _service.GetToDoAsync(id, token);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDo([FromBody] ToDoPostRequest request, CancellationToken token)
        {
            var toDo = new ToDo
            {
                Title = request.Title,
                Description = request.Description,
                CreateAt = DateTime.UtcNow,
                LastDay = request.LastDay,
                IsActive = true
            };

            if(!await _service.AddToDoAsync(toDo, token))
            {
                _logger.LogError("Попытка добавления не удалась");
                return BadRequest();
            }

            _logger.LogInformation("Задача была добавлена");
            return Created();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateToDo([FromBody] ToDoPatchRequest request, CancellationToken token)
        {
            var toDo = new ToDo
            {
                Id = request.Id,
                Title = request.Title ?? string.Empty,
                Description = request.Description,
                LastDay = request.LastDay ?? DateTime.MinValue,
                IsActive = request.IsActive
            };

            if (!await _service.UpdateToDoAsync(toDo, token))
            {
                _logger.LogError("Попытка замены не удалась");
                return BadRequest();
            }

            _logger.LogInformation("Произошла замена");
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteToDo([FromBody] ToDoDeleteRequest request, CancellationToken token)
        {
            bool status = await _service.DeleteToDoAsync(request.Id, token);

            if(status)
            {
                _logger.LogInformation("Удаление прошло успешно");
                return NoContent();
            }

            _logger.LogError($"Задачи под Id {request.Id} не была найдена!");
            return BadRequest();
        }
    }
}