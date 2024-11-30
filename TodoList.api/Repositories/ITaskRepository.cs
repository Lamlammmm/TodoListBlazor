using TodoList.Models;

namespace TodoList.api.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Entities.Task>> GetList(TaskListSearch taskListSearch, PageRequest pagingRequest);
        Task<int> Create(Entities.Task task);
        Task<int> Update(Entities.Task task);
        Task<int> Delete(Guid Id);
        Task<Entities.Task> GetById(Guid Id);
    }
}
