﻿using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<ToDo> ToDos { get; set; }
    }
}