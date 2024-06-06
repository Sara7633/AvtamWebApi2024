using Microsoft.EntityFrameworkCore;
using Entities;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly _214346710DbContext userContext;

        public UserRepository(_214346710DbContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<User> Register(User user)
        {
            try
            {
                var existingUser = await userContext.Users.FirstOrDefaultAsync(e => e.UserName.Equals(user.UserName));
                if (existingUser != null)
                {
                    return null;
                }

                await userContext.Users.AddAsync(user);
                await userContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Update(int id, User user)
        {
         
            try
            {
                User foundUser = await userContext.Users.FirstOrDefaultAsync(u=>u.Id.Equals(id));
                if (foundUser != null)
                {
                    foundUser.UserName = user.UserName;
                    foundUser.FirstName = user.FirstName;
                    foundUser.LastName = user.LastName;
                    foundUser.Password = user.Password;
                    foundUser.Email = user.Email;

                    await userContext.SaveChangesAsync();
                }
                return foundUser;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User> Login(User user)
        {
            var userFound = await userContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(user.UserName.TrimEnd()));
            if (userFound != null && userFound.Password.TrimEnd().Equals(user.Password))
            {
                return userFound;
            }
            return null;
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                var userFound = await userContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                return userFound;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}