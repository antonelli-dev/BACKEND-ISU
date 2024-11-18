using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage ="Title can not be empty!")]
        [MaxLength(20, ErrorMessage = "The max Length of the Title is 20 characters")]
        public string? Title { get; set; }

        [MaxLength(30, ErrorMessage = "The max length of the description is 30 characters")]
        [Required(ErrorMessage = "Description can not be empty!")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Due Date must contain a valid date")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "IsCompleted should be completed and contain true or false value ")]
        public bool IsCompleted { get; set; } = false;
    }
}
