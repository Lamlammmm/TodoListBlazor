using Microsoft.AspNetCore.Components;
using TodoList.BlazorApp.Services;
using TodoList.Models;

namespace TodoList.BlazorApp.Components.Pages
{
    public partial class TaskDetail
    {
        [Parameter]
        public string TaskId { get; set; }
        private TasksDto task { get; set; }
        [Inject] private ITaskApiClient TaskApiClient { get; set; }
        protected override async Task OnInitializedAsync()
        {
            task = await TaskApiClient.GetTaskById(Guid.Parse(TaskId));
        }
    }
}
 