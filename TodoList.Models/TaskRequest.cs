using System.ComponentModel.DataAnnotations;
using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TaskRequest
    {
        public Guid Id { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        [Required]
        public Priority Priority { get; set; }
    }
}