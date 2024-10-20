using ToDoAPI.Models.Entities;
using ToDoAPI.Models.Responses;

namespace ToDoAPI.Contracts
{
    public interface IToDoService
    {
        public Task<bool> AddToDoAsync(ToDo toDo, CancellationToken token);
        public Task<bool> UpdateToDoAsync(ToDo toDo, CancellationToken token);
        public Task<bool> DeleteToDoAsync(int toDoId, CancellationToken token);
        public Task<ICollection<ToDoResponse>> GetAllToDoAsync(CancellationToken token);
        public Task<ToDoResponse?> GetToDoAsync(int id, CancellationToken token);
    }
}