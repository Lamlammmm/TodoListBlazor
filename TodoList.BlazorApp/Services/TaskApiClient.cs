using TodoList.Models;

namespace TodoList.BlazorApp.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        public HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateTask(TaskCreateRequest taskRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Tasks", taskRequest);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/Tasks/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<TasksDto> GetTaskById(Guid Id)
        {
            var result = await _httpClient.GetFromJsonAsync<TasksDto>($"api/Tasks/{Id}");
            return result;
        }

        public async Task<BaseApiResult<TasksDto>> GetTaskList(TaskListSearchContext taskListSearch, PageRequest pagingRequest)
        {
            var url = $"api/Tasks?name={taskListSearch.Name}&assigneeid={taskListSearch.AssigneeId}&priority={taskListSearch.Priority}&pageindex={pagingRequest.PageIndex}&pagesize={pagingRequest.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<BaseApiResult<TasksDto>>(url);
            return result;
        }

        public async Task<bool> UpdateTask(Guid id, TaskUpdateRequest taskRequest)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Tasks/{id}", taskRequest);
            return result.IsSuccessStatusCode;
        }
    }
}
