using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.api.Entities;

namespace TodoList.api.EF
{
    public class TodoListDbContext:IdentityDbContext<User, Role, Guid>
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options):base(options)
        {
        }
        public DbSet<Entities.Task> Task { get; set; }
    }
}
