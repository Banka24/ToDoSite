namespace ToDoAPI.Models.Responses
{
    public class ToDoResponse(int id, string title, string? description, DateTime lastDay, bool isComplete)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string? Description { get; set; } = description;
        public DateTime LastDay { get; set; } = lastDay.ToLocalTime();
        public bool IsComplete { get; set; } = isComplete;
    }
}