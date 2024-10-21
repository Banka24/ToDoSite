using ToDoAPI.Contracts;
using static BCrypt.Net.BCrypt;

namespace ToDoAPI.Services
{
    public class SecurityService : ISecurityService
    {
        public string HashPasswordUser(string inputPassword)
        {
            return HashPassword(inputPassword);
        }

        public bool VerifyUser(string inputPassword, string hashPassword)
        {
            return Verify(inputPassword, hashPassword);
        }

        public string ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return "Пароль должен содержать как минимум 8 символов.";
            }

            if (!password.Any(char.IsDigit))
            {
                return "Пароль должен содержать хотя бы одну цифру.";
            }

            if (!password.Any(char.IsUpper))
            {
                return "Пароль должен содержать хотя бы одну заглавную букву.";
            }

            if (!password.Any(char.IsLower))
            {
                return "Пароль должен содержать хотя бы одну строчную букву.";
            }

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return "Пароль должен содержать хотя бы один специальный символ.";
            }

            return string.Empty;
        }
    }
}