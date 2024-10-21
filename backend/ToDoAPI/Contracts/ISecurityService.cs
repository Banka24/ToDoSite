namespace ToDoAPI.Contracts
{
    public interface ISecurityService
    {
        public string HashPasswordUser(string inputPassword);
        public bool VerifyUser(string inputPassword, string hashPassword);
        public string ValidatePassword(string password);
    }
}