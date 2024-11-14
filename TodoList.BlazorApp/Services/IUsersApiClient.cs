using TodoList.Models;

namespace TodoList.BlazorApp.Services
{
    public interface IUsersApiClient
    {
        Task<List<AssigneeDto>> GetAssignee();
    }
}
