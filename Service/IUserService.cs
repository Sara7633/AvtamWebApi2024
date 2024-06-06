using Entities;
using System.Collections.Specialized;


namespace Service
{
    public interface IUserService
    {

        Task<User> Register(User user);
        Task<User> Update(int id, User user);
        Task<User> Login(User user);
        int checkPassword(string password);
        Task<User> GetUserById(int id);
        string generateJwtToken(User user);
    }
}
