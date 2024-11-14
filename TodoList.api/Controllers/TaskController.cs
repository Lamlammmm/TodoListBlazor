using Microsoft.AspNetCore.Mvc;
using TodoList.api.Repositories;
using TodoList.Models;
using TodoList.Models.Enums;

namespace TodoList.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]TaskListSearch taskListSearch)
        {
            var listTask = await _taskRepository.GetList(taskListSearch);
            var taskDto = listTask.Select(x => new TasksDto()
            {
                Id = x.Id,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Status = x.Status,
                AssigneeId = x.AssigneeId,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + " " + x.Assignee.LastName : "N/A"
            });
            return Ok(taskDto);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(TaskRequest request)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }
            var task = new Entities.Task()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedDate = DateTime.Now,
                Priority = request.Priority,
                Status = Status.Open
            };
            var result = await _taskRepository.Create(task);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            var result = new TasksDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate,
                AssigneeName = task.Assignee != null ? task.Assignee.FirstName + " " + task.Assignee.LastName : "N/A"
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(TaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await _taskRepository.GetById(request.Id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = request.Name;
            item.Priority = request.Priority;
            var result = await _taskRepository.Update(item);
            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _taskRepository.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}