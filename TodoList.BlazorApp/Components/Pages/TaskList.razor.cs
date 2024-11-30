using Blazored.Toast.Services;
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

        [Inject]
        private IToastService toastService { get; set; }

        private List<TasksDto> Tasks;

        [SupplyParameterFromForm]
        private TaskListSearch? TaskListSearch { get; set; }

        private PageRequest PageRequest { get; set; }

        private List<AssigneeDto> Assignees;

        private BaseApiResult<TasksDto> apiResult;

        const int pageSize = 10;

        [Parameter]
        public string currentPage { get; set; } = "1";

        protected override async Task OnInitializedAsync()
        {
            TaskListSearch ??= new();
            PageRequest = new PageRequest()
            {
                PageIndex = int.Parse(currentPage),
                PageSize = pageSize
            };
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
            apiResult = await taskApiClient.GetTaskList(TaskListSearch, PageRequest);
            Tasks = apiResult.Data;
        }

        //private async Task ChangePage(int pageIndex)
        //{
        //    currentPage = pageIndex.ToString();
        //}
    }
}