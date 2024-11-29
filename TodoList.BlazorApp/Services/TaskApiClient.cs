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
            var result = await _httpClient.PostAsJsonAsync("api/Task/Create", taskRequest);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/Task/Delete/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<TasksDto> GetTaskById(Guid Id)
        {
            var result = await _httpClient.GetFromJsonAsync<TasksDto>($"api/Task/{Id}");
            return result;
        }

        public async Task<List<TasksDto>> GetTaskList(TaskListSearch taskListSearch)
        {
            var url = $"api/Task?name={taskListSearch.Name}&assigneeid={taskListSearch.AssigneeId}&priority={taskListSearch.Priority}";
            var result = await _httpClient.GetFromJsonAsync<List<TasksDto>>(url);
            return result;
        }

        public async Task<bool> UpdateTask(Guid id, TaskUpdateRequest taskRequest)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Task/Update/{id}", taskRequest);
            return result.IsSuccessStatusCode;
        }
    }
}
