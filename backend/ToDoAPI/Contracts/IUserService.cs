using ToDoAPI.Models.Entities;

namespace ToDoAPI.Contracts
{
    public interface IUserService
    {
        Task<bool> RegestrationUser(User user, CancellationToken token);
        Task<bool> VerifyUser(User user, CancellationToken token);
        public Task<bool> UserExists(string Login);
    }
}