using Entities;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> Register(User user);
        Task<User> Update(int id, User user);

        Task<User> Login(User user);
        Task<User> GetUserById(int id);
    }
}
