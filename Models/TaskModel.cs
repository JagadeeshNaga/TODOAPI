using System.ComponentModel.DataAnnotations;

namespace TODOapi.Models
{
    public class TaskModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Task name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority is required.")]
        [Range(1, 10, ErrorMessage = "Priority must be between 1 and 10.")]
        public int Priority { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(Not Started|In Progress|Completed)$", ErrorMessage = "Invalid status.")]
        public string Status { get; set; } = "Not Started"; // Default value
    }
}
