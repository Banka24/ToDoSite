using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Contracts;
using ToDoAPI.DataAccess;
using ToDoAPI.Models.Entities;

namespace ToDoAPI.Services
{
    public class UserService([FromServices] ToDoDbContext context, [FromServices] ToDoRedisDbContext redisContext, [FromServices] ISecurityService securityService, ILogger<UserService> logger) : IUserService
    {
        private readonly ILogger<UserService> _logger = logger;
        private readonly ISecurityService _securityService = securityService;
        private readonly ToDoDbContext _context = context;
        private readonly ToDoRedisDbContext _redisContext = redisContext;

        public async Task<bool> RegestrationUser(User user, CancellationToken token)
        {
            user.Password = _securityService.HashPasswordUser(user.Password);
            await _context.Users.AddAsync(user, token);
            bool operationStatus = await _context.TrySaveChangesAsync(token);
            if (!operationStatus)
            {
                _logger.LogError($"Произошла ошибка добавления в БД нового пользователя. Пользователь: {user.Login}");
                return false;
            }

            operationStatus = await UpdateInfoAboutUsers(user);

            if (!operationStatus)
            {
                _logger.LogError("Не удалось добавить в Redis");
                return false;
            }

            _logger.LogInformation($"Новый пользователь {user.Login} добавлен в Redis");
            return true;
        }

        public async Task<bool> VerifyUser(User user, CancellationToken token)
        {
            string? password = string.Empty;
            bool check = await _redisContext.Redis.KeyExistsAsync(user.Login);

            if (check)
            {
                password = await _redisContext.Redis.StringGetAsync(user.Login);
                return _securityService.VerifyUser(user.Password, password!);
            }

            password = await _context.Users.Where(i => i.Login == user.Login).Select(i => i.Password).FirstOrDefaultAsync(token);

            if (string.IsNullOrEmpty(password)) return false;

            await UpdateInfoAboutUsers(user);
            return _securityService.VerifyUser(user.Password, password);
        }

        public Task<bool> UserExists(string Login)
        {
            return _redisContext.Redis.KeyExistsAsync(Login);
        }

        private Task<bool> UpdateInfoAboutUsers(User user)
        {
            return _redisContext.Redis.StringSetAsync(user.Login, user.Password);
        }
    }
}