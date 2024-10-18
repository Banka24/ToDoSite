namespace ToDoAPI.Models.Requests
{
    public record class ToDoPatchRequest(int Id, string Title, string? Description, DateTime? LastDay, bool IsActive);
}