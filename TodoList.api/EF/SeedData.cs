using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.api.Entities;
using TodoList.Models.Enums;

namespace TodoList.api.EF
{
    public class SeedData
    {
        private static readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public static void Initialize(IServiceProvider services)
        {
            using (var context = new TodoListDbContext(services.GetRequiredService<DbContextOptions<TodoListDbContext>>()))
            {
                if (!context.Task.Any())
                {
                    context.Task.Add(new Entities.Task()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Same task 1",
                        CreatedDate = DateTime.Now,
                        Priority = Priority.High,
                        Status = Status.Open,
                    });
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    var user = new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Mr",
                        LastName = "A",
                        Email = "admin1@gmail.com",
                        NormalizedEmail = "ADMIN1@GMAIL.COM",
                        PhoneNumber = "032132131",
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123");
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }
        
    }
}
