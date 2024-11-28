using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoList.BlazorApp.Services;
using TodoList.Models;

namespace TodoList.BlazorApp.Components.Pages
{
    public partial class TaskEdit
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ITaskApiClient TaskApiClient { get; set; }
        [Parameter]
        public string TaskId { get; set; }
        [SupplyParameterFromForm]
        TaskUpdateRequest TaskUpdateRequest { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            TaskUpdateRequest ??= new();
            if (TaskUpdateRequest.Name == null)
            {
                var taskDto = await TaskApiClient.GetTaskById(Guid.Parse(TaskId));
                if (taskDto != null)
                {
                    TaskUpdateRequest = new TaskUpdateRequest();
                    TaskUpdateRequest.Name = taskDto.Name;
                    TaskUpdateRequest.Priority = taskDto.Priority;
                }
            }
        }

        private async Task UpdateTask(EditContext editContext)
        {
            var result = await TaskApiClient.UpdateTask(Guid.Parse(TaskId), TaskUpdateRequest);
            if (result)
            {
                NavigationManager.NavigateTo("/taskList");
            }
        }
    }
}
