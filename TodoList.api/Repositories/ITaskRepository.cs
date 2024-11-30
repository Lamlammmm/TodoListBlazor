using TodoList.Models;

namespace TodoList.api.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Entities.Task>> GetList(TaskListSearch taskListSearch, PageRequest pagingRequest);
        Task<Entities.Task> Create(Entities.Task task);
        Task<Entities.Task> Update(Entities.Task task);
        Task<Entities.Task> Delete(Guid Id);
        Task<Entities.Task> GetById(Guid Id);
    }
}
