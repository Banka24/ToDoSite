namespace ToDoAPI.Models.Requests
{
    public record class ToDoPostRequest(string Title, string? Description, DateTime LastDay);
}