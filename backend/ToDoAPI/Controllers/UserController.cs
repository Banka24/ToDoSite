using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Contracts;
using ToDoAPI.Models.Entities;
using ToDoAPI.Models.Requests;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController([FromServices] IUserService service, [FromServices] ISecurityService securityService, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserService _userService = service;
        private readonly ILogger<UserController> _logger = logger;
        private readonly ISecurityService _securityService = securityService;

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] UserPostRequest request, CancellationToken token)
        {
            string error = _securityService.ValidatePassword(request.Password);

            if (string.IsNullOrEmpty(error))
            {
                var user = new User
                {
                    Login = request.Login,
                    Password = request.Password
                };

                bool registrationStatus = await _userService.RegestrationUser(user, token);

                if (!registrationStatus)
                {
                    _logger.LogError("Не удалось зарегестрировать пользователя");
                    return BadRequest();
                }

                _logger.LogInformation("Новый пользователь зарегистрирован");
                return NoContent();
            }

            return BadRequest(error);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] UserPostRequest request, CancellationToken token)
        {
            var user = new User
            {
                Login = request.Login,
                Password = request.Password
            };

            bool verificationStatus = await _userService.VerifyUser(user, token);

            if (verificationStatus)
            {
                _logger.LogInformation($"Пользователь {user.Login} авторизован");
                return NoContent();
            }

            _logger.LogError($"Пользователя с логином {user.Login} нет в БД");
            return verificationStatus ? NoContent() : Unauthorized();
        }
    }
}
