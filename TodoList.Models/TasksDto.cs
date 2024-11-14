using System.ComponentModel.DataAnnotations;
using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TasksDto
    {
        public Guid Id { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        public Guid? AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}