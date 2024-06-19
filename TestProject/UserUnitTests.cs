using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{

    public class UserUnitTests
    {
        [Fact]
        public async Task TestRegister_NewUser_Success()
        {
            // Arrange
            var mockDbContext = new Mock<AvtamWebApi2024Context>();

            var user = new User { UserName = "newuser", Password = "password123", FirstName = "aaa", LastName = "aaa" };
            var userReg = new User { UserName = "aaa", Password = "aaa", FirstName = "aaa", LastName = "aaa" };
            mockDbContext.Setup(m => m.Users).ReturnsDbSet(new List<User> { user });
            var userRepository = new UserRepository(mockDbContext.Object);

            // Act
            var result = await userRepository.Register(userReg);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userReg.UserName, result.UserName);
        }

        [Fact]
        public async Task TestRegister_NewUser_InSuccess()
        {
            // Arrange
            var mockDbContext = new Mock<AvtamWebApi2024Context>();
            var user = new User { UserName = "aaa", Password = "aaa", FirstName = "aaa", LastName = "aaa" };
            var userReg = new User { UserName = "aaa", Password = "aaa", FirstName = "aaa", LastName = "aaa" };
            mockDbContext.Setup(m => m.Users).ReturnsDbSet(new List<User> { user });
            var service = new UserRepository(mockDbContext.Object);

            // Act
            var result = await service.Register(userReg);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task TestLogin_Successful()
        {
            // Arrange
            var mockUserContext = new Mock<AvtamWebApi2024Context>();
            var users = new List<User>
            {
                new User { UserName = "testuser", Password = "password" }
            };

            mockUserContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockUserContext.Object);

            // Act
            var user = await userRepository.Login(new User { UserName = "testuser", Password = "password" });

            // Assert
            Assert.NotNull(user);
            Assert.Equal("testuser", user.UserName);
        }

        [Fact]
        public async Task TestLogin_Failed()
        {
            // Arrange
            var mockUserContext = new Mock<AvtamWebApi2024Context>();
            var users = new List<User>
            {
            new User { UserName = "testuser", Password = "password" }
            };

            mockUserContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userService = new UserRepository(mockUserContext.Object);

            // Act
            var user = await userService.Login(new User { UserName = "testuser", Password = "wrongpassword" });

            // Assert
            Assert.Null(user);
        }


        [Fact]
        public async Task Register_ExceptionThrown_ExceptionIsThrown()
        {
            // Arrange
            var userContextMock = new Mock<AvtamWebApi2024Context>();
            var user = new User { UserName = "testuser", Password = "password123" };
            userContextMock.Setup(x => x.Users.AddAsync(It.IsAny<User>(), default)).ThrowsAsync(new Exception("Simulated exception"));

            var userRepository = new UserRepository(userContextMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await userRepository.Register(user));
        }

        [Fact]
        public async Task Update_ExistingUser_SuccessfullyUpdated()
        {
            // Arrange
            var id = 1;
            var existingUser = new User { Id = id, UserName = "existinguser", Password = "password123" };
            var updatedUser = new User { Id = id, UserName = "updateduser", Password = "updated123" };

            var dbContextMock = new Mock<AvtamWebApi2024Context>();
            dbContextMock.Setup(m => m.Users).ReturnsDbSet(new List<User> { existingUser });

            var userRepository = new UserRepository(dbContextMock.Object);

            // Act;
            var result = await userRepository.Update(id, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.UserName, result.UserName);
            Assert.Equal(updatedUser.Password, result.Password);
        }
        [Fact]

        public async Task Update_NonExistingUser_ReturnsNull()
        {
            // Arrange
            var id = 1;
            var nonExistingUser = new User { Id = id + 1, UserName = "nonexistinguser", Password = "password123" };
            var existingUser = new User { Id = id, UserName = "existinguser", Password = "password123" };
            var dbContextMock = new Mock<AvtamWebApi2024Context>();
            dbContextMock.Setup(m => m.Users).ReturnsDbSet(new List<User> { existingUser });

            var userRepository = new UserRepository(dbContextMock.Object);

            // Act
            var result = await userRepository.Update(id + 1, nonExistingUser);

            // Assert
            Assert.Null(result);
        }


    }
}
