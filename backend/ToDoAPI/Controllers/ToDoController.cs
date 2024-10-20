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

        [HttpGet()]
        public async Task<IActionResult> GetAllToDo(CancellationToken token)
        {
            var list = await _service.GetAllToDoAsync(token);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDo(int id, CancellationToken token)
        {
            var todo = await _service.GetToDoAsync(id, token);
            return todo != null ? Ok(todo) : BadRequest("Такой записи нет");
        }

        [HttpPost()]
        public async Task<IActionResult> CreateToDo([FromBody] ToDoPostRequest request, CancellationToken token)
        {
            var toDo = new ToDo
            {
                Title = request.Title,
                Description = request.Description,
                CreateAt = DateTime.UtcNow,
                LastDay = request.LastDay,
                IsComplete = false
            };

            if (!await _service.AddToDoAsync(toDo, token))
            {
                _logger.LogError("Попытка добавления не удалась");
                return BadRequest("Попытка добавления не удалась");
            }

            _logger.LogInformation("Задача была добавлена");
            return Created();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateToDo(int id, [FromBody] ToDoPatchRequest request, CancellationToken token)
        {
            var toDo = new ToDo
            {
                Id = id,
                Title = request.Title ?? string.Empty,
                Description = request.Description,
                LastDay = request.LastDay ?? DateTime.MinValue,
                IsComplete = request.IsComplete
            };

            if (!await _service.UpdateToDoAsync(toDo, token))
            {
                _logger.LogError("Попытка замены не удалась");
                return BadRequest();
            }

            _logger.LogInformation("Произошла замена");
            return Ok("Изменения сохранены");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo(int id, CancellationToken token)
        {
            bool status = await _service.DeleteToDoAsync(id, token);

            if (status)
            {
                _logger.LogInformation("Удаление прошло успешно");
                return Ok("Задание удалено");
            }

            _logger.LogError($"Задачи под Id {id} не была найдена!");
            return BadRequest();
        }
    }
}