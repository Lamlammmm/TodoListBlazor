using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoList.BlazorApp.Services;
using TodoList.Models;

namespace TodoList.BlazorApp.Components.Pages
{
    public partial class TaskList
    {
        [Inject] 
        private ITaskApiClient taskApiClient { get; set; }
        [Inject] 
        private IUsersApiClient usersApiClient { get; set; }

        private List<TasksDto> Tasks;

        [SupplyParameterFromForm]
        private TaskListSearch? TaskListSearch { get; set; }

        private List<AssigneeDto> Assignees;

        protected override async Task OnInitializedAsync()
        {
            TaskListSearch ??= new();
            await GetTask();

            Assignees = await usersApiClient.GetAssignee();
        }

        private async Task SearchForm(EditContext context)
        {
            await GetTask();
        }

        private async Task DeleteTask(Guid id)
        {
            if (id != null)
            {
                var result = await taskApiClient.DeleteTask(id);
                if (result)
                {
                    await GetTask();
                }
            }
        }

        private async Task GetTask()
        {
            Tasks = await taskApiClient.GetTaskList(TaskListSearch);
        }
    }
}