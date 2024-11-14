using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.api.Repositories;
using TodoList.Models;

namespace TodoList.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _UsersRepository;

        public UsersController(IUsersRepository UsersRepository)
        {
            _UsersRepository = UsersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listUsers = await _UsersRepository.GetList();
            var AssigneesDto = listUsers.Select(x => new AssigneeDto()
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName
            });
            return Ok(AssigneesDto);
        }
    }
}
