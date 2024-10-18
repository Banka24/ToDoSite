using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models.Entities
{
    public class ToDo
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime LastDay { get; set; }
        public bool IsActive { get; set; }
    }
}