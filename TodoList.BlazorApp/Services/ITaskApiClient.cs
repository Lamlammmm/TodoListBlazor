using TodoList.Models;

namespace TodoList.BlazorApp.Services
{
    public interface ITaskApiClient
    {
        Task<BaseApiResult<TasksDto>> GetTaskList(TaskListSearchContext taskListSearch, PageRequest pagingRequest);
        Task<TasksDto> GetTaskById(Guid Id);
        Task<bool> CreateTask(TaskCreateRequest taskRequest);
        Task<bool> UpdateTask(Guid id, TaskUpdateRequest taskRequest);
        Task<bool> DeleteTask(Guid id);
    }
}
