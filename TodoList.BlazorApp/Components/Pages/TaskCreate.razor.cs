using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using TodoList.Models;

namespace TodoList.BlazorApp.Components.Pages
{
    public partial class TaskCreate
    {
        [SupplyParameterFromForm]
        private TaskCreateRequest taskRequest { get; set; }
        protected override async Task OnInitializedAsync()
        {
            taskRequest ??= new();
        }
        private async Task SubmitTask(EditContext editContext)
        {
            var result = await TaskApiClient.CreateTask(taskRequest);
            if (result)
            {
                NavigationManager.NavigateTo("/TaskList");
            }
        }
    }
}
