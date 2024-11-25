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

        public async Task<bool> CreateTask(TaskRequest taskRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Task/Create", taskRequest);
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
    }
}
