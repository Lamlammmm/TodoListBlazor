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
        private TaskListSearch TaskListSearchModel = new TaskListSearch();

        [SupplyParameterFromForm]
        private TaskListSearch? TaskListSearch { get; set; }

        private List<AssigneeDto> Assignees;

        protected override async Task OnInitializedAsync()
        {
            TaskListSearch ??= new();

            Tasks = await taskApiClient.GetTaskList(TaskListSearch);
            Assignees = await usersApiClient.GetAssignee();
        }

        private async Task SearchForm(EditContext context)
        {
            Tasks = await taskApiClient.GetTaskList(TaskListSearch);
        }
    }
}