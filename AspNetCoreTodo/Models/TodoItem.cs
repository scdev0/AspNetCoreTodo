using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    /* The to-do entity model; defines what the db needs for each to-do item  */
    public class TodoItem
    {
        public Guid Id { get; set; }
        public bool IsDone { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTimeOffset? DueAt { get; set; }
    }
}