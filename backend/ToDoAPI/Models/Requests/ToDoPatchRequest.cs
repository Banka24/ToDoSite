namespace ToDoAPI.Models.Requests
{
    public record class ToDoPatchRequest(string Title, string? Description, DateTime? LastDay, bool IsComplete);
}