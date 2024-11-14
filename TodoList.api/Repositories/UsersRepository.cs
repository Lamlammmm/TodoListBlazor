
using Microsoft.EntityFrameworkCore;
using TodoList.api.EF;
using TodoList.api.Entities;

namespace TodoList.api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TodoListDbContext _todoListDbContext;
        public UsersRepository(TodoListDbContext todoListDbContext)
        {
            _todoListDbContext = todoListDbContext;
        }
        public async Task<IEnumerable<User>> GetList()
        {
            return await _todoListDbContext.Users.ToListAsync();
        }
    }
}
