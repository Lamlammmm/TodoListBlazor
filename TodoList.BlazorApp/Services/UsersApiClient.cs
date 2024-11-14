using TodoList.Models;

namespace TodoList.BlazorApp.Services
{
    public class UsersApiClient : IUsersApiClient
    {
        public HttpClient _httpClient;

        public UsersApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AssigneeDto>> GetAssignee()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AssigneeDto>>("api/Users");
            return result;
        }
    }
}
