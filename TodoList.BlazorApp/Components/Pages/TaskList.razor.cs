using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoList.BlazorApp.Services;
using TodoList.Models;

namespace TodoList.BlazorApp.Components.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient taskApiClient { get; set; }
        [Inject] private IUsersApiClient usersApiClient { get; set; }
        private List<TasksDto> Tasks;
        private TaskListSearch TaskListSearch = new TaskListSearch();
        private List<AssigneeDto> Assignees;

        protected override async Task OnInitializedAsync()
        {
            Tasks = await taskApiClient.GetTaskList(TaskListSearch);
            Assignees = await usersApiClient.GetAssignee();
        }

        private void SearchForm(EditContext context)
        {
            if (context.GetValidationMessages().Any())
            {
                // Có lỗi xác thực, không thực hiện gì
                return;
            }

            var name = TaskListSearch.Name;
        }
    }
}