using TodoList.Models;

namespace TodoList.BlazorApp.Services
{
    public interface ITaskApiClient
    {
        Task<List<TasksDto>> GetTaskList(TaskListSearch taskListSearch);
        Task<TasksDto> GetTaskById(Guid Id);
    }
}
