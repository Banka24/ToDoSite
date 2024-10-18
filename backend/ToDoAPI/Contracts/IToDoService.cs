using ToDoAPI.Models.Entities;

namespace ToDoAPI.Contracts
{
    public interface IToDoService
    {
        public Task<bool> AddToDoAsync(ToDo toDo, CancellationToken token);
        public Task<bool> UpdateToDoAsync(ToDo toDo, CancellationToken token);
        public Task<bool> DeleteToDoAsync(int toDoId, CancellationToken token);
        public Task<ICollection<ToDo>> GetAllToDoAsync(CancellationToken token);
        public Task<ToDo?> GetToDoAsync(int id, CancellationToken token);
    }
}