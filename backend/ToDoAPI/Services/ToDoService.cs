using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Contracts;
using ToDoAPI.DataAccess;
using ToDoAPI.Models.Entities;
using ToDoAPI.Models.Responses;

namespace ToDoAPI.Services
{
    public class ToDoService([FromServices] ToDoDbContext context, ILogger<ToDoService> logger) : IToDoService
    {
        private readonly ToDoDbContext _context = context;
        private readonly ILogger<ToDoService> _logger = logger;

        public async Task<bool> AddToDoAsync(ToDo toDo, CancellationToken token)
        {
            await _context.ToDos.AddAsync(toDo, token);
            return await _context.TrySaveChangesAsync(token);
        }

        public async Task<bool> UpdateToDoAsync(ToDo inputToDo, CancellationToken token)
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(i => i.Id == inputToDo.Id, token);

            if (todo is null)
            {
                _logger.LogError($"Произошла ошибка! Запись с id {inputToDo.Id} не была найдена");
                return false;
            }

            todo.Title = inputToDo.Title;
            todo.Description = inputToDo.Description;
            todo.IsComplete = inputToDo.IsComplete;

            if (inputToDo.LastDay != DateTime.MinValue)
            {
                todo.LastDay = inputToDo.LastDay;
            }

            return await _context.TrySaveChangesAsync(token);
        }

        public async Task<bool> DeleteToDoAsync(int toDoId, CancellationToken token)
        {
            var todo = _context.ToDos.FirstOrDefault(i => i.Id == toDoId);
            if (todo is null)
            {
                _logger.LogInformation($"Был найден пустой объект! Id объекта: {toDoId}");
                return false;
            }
            _context.ToDos.Remove(todo);
            return await _context.TrySaveChangesAsync(token);
        }

        public async Task<ICollection<ToDoResponse>> GetAllToDoAsync(CancellationToken token)
        {
            return await _context.ToDos.Select(i => new ToDoResponse(i.Id, i.Title, i.Description, i.LastDay, i.IsComplete)).ToArrayAsync(token);
        }

        public async Task<ToDoResponse?> GetToDoAsync(int id, CancellationToken token)
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(i => i.Id == id, token);
            if (todo is null)
            {
                _logger.LogError($"Нет записи с id: {id}");
                return null;
            }
            var answer = new ToDoResponse(todo.Id, todo.Title, todo.Description, todo.LastDay, todo.IsComplete);
            return answer;
        }
    }
}