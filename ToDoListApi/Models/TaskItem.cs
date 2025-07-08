using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(200)]
        public required string Title { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
