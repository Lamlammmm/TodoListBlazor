using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TaskListSearchContext : PageRequest
    {
        public string? Name { get; set; }
        public Guid? AssigneeId { get; set; }
        public Priority? Priority { get; set; }

    }
}