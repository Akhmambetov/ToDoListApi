using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; } = false;
    }
}
