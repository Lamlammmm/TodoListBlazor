using TodoList.api.Entities;

namespace TodoList.api.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetList();
    }
}