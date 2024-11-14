
using Microsoft.EntityFrameworkCore;
using TodoList.api.EF;
using TodoList.Models;

namespace TodoList.api.Repositories
{
    public class TaskRepositoy : ITaskRepository
    {
        private readonly TodoListDbContext _todoListDbContext;
        public TaskRepositoy(TodoListDbContext todoListDbContext)
        {
            _todoListDbContext = todoListDbContext;
        }
        public async Task<Entities.Task> Create(Entities.Task task)
        {
            await _todoListDbContext.Task.AddAsync(task);
            await _todoListDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> Delete(Guid Id)
        {
            var item = await _todoListDbContext.Task.FindAsync(Id);
            _todoListDbContext.Task.Remove(item);
            await _todoListDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Entities.Task> GetById(Guid Id)
        {
            return await _todoListDbContext.Task.FindAsync(Id);
        }

        public async Task<IEnumerable<Entities.Task>> GetList(TaskListSearch taskListSearch)
        {
            var query = _todoListDbContext.Task.Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(taskListSearch.Name))
            {
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));
            }
            if (taskListSearch.AssigneeId.HasValue)
            {
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);
            }
            if (taskListSearch.Priority.HasValue)
            {
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<Entities.Task> Update(Entities.Task task)
        {
            _todoListDbContext.Task.Update(task);
            await _todoListDbContext.SaveChangesAsync();
            return task;
        }
    }
}
