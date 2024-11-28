using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TaskUpdateRequest
    {
        public string Name { get; set; }
        public Priority Priority { get; set; }
    }
}