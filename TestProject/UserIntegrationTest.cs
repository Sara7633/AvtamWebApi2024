using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly _214346710DbContext _dbContext;
        private readonly UserRepository userRepository;
        public UserIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            userRepository = new UserRepository(_dbContext);
        }
        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            //Arrange
            var userName = "sari";
            var password = "password";
            var user = new User() { UserName = userName, Password = password, FirstName = "sara", LastName = "alter" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await userRepository.Login(user);

            //Assert
            Assert.NotNull(result);
        }

    }
}
