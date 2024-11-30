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
        public async Task<IActionResult> GetAll([FromQuery] TaskListSearch taskListSearch, [FromQuery] PageRequest pageRequest)
        {
            var listTask = await _taskRepository.GetList(taskListSearch, pageRequest);
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
            int totalItem = taskDto.Count();
            int totalPage = (int)Math.Ceiling((double)totalItem / pageRequest.PageSize);
            var result = taskDto.OrderByDescending(x => x.CreatedDate)
                                .Skip((pageRequest.PageIndex - 1) * pageRequest.PageSize)
                                .Take(pageRequest.PageSize);

            return Ok(new BaseApiResult<TasksDto>()
            {
                Data = result.ToList(),
                HttpStatusCode = StatusCodes.Status200OK,
                Message = "Get List Success",
                IsSuccess = true,
                TotalPage = totalPage,
                TotalItem = totalItem
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateRequest request)
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
            if (result > 0)
            {
                return Ok(new BaseApiResult<Entities.Task>()
                {
                    HttpStatusCode = 200,
                    Message = $"Create {result} item is Success",
                    IsSuccess = true,
                });
            }
            else
            {
                return BadRequest(new BaseApiResult<Entities.Task>()
                {
                    HttpStatusCode = 400,
                    Message = "Create Failed",
                    IsSuccess = false,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, TaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await _taskRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = request.Name;
            item.Priority = request.Priority;
            var result = await _taskRepository.Update(item);
            if (result > 0)
            {
                return Ok(new BaseApiResult<Entities.Task>()
                {
                    HttpStatusCode = 200,
                    Message = $"Update {result} item is Success",
                    IsSuccess = true,
                });
            }
            else
            {
                return BadRequest(new BaseApiResult<Entities.Task>()
                {
                    HttpStatusCode = 400,
                    Message = "Update Failed",
                    IsSuccess = false,
                });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
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
            if (result > 0)
            {
                return Ok(new BaseApiResult<Entities.Task>()
                {
                    HttpStatusCode = 200,
                    Message = $"Delete {result} item is Success",
                    IsSuccess = true,
                });
            }
            else
            {
                return BadRequest(new BaseApiResult<Entities.Task>()
                {
                    HttpStatusCode = 400,
                    Message = "Delete Failed",
                    IsSuccess = false,
                });
            }
        }
    }
}