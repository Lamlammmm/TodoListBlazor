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

        private PageRequest PageRequest { get; set; }

        private List<AssigneeDto> Assignees;

        private BaseApiResult<TasksDto> apiResult;

        //const int pageSize = 10;

        [Parameter]
        public string currentPageString { get; set; }

        TaskListSearchContext searchContext { get; set; }

        public int currentPage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            searchContext ??= new();
            if (currentPageString != null)
            {
                currentPage = int.Parse(currentPageString) - 1;
            }
            PageRequest = new PageRequest()
            {
                PageIndex = currentPage,
                //PageSize = pageSize
            };
            await GetTask();
            Assignees = await usersApiClient.GetAssignee();
        }

        private async Task SearchForm(TaskListSearchContext taskListSearchContext)
        {
            searchContext = taskListSearchContext;
            
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
            apiResult = await taskApiClient.GetTaskList(searchContext, PageRequest);
            Tasks = apiResult.Data;
        }

        //private async Task ChangePage(int pageIndex)
        //{
        //    currentPage = pageIndex.ToString();
        //}
    }
}