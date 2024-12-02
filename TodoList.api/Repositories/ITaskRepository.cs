using TodoList.Models;

namespace TodoList.api.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Entities.Task>> GetList(TaskListSearchContext taskListSearch);
        Task<int> Create(Entities.Task task);
        Task<int> Update(Entities.Task task);
        Task<int> Delete(Guid Id);
        Task<Entities.Task> GetById(Guid Id);
    }
}
