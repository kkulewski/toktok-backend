using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TokTok.Models;
using TokTok.Repositories;
using TokTok.Services.Authentication;
using Xunit;

namespace TokTok.Tests.Integration
{
    public class AuthenticationServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private IAuthenticationService _authenticationService;

        private readonly List<User> _exampleUsers;

        public AuthenticationServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            

            _exampleUsers = new List<User>
            {
                new User {Id = 1, UserName = "user1", Token = "token1" },
                new User {Id = 2, UserName = "user2", Token = "token2" },
                new User {Id = 3, UserName = "user3", Token = "token3" }
            };

            
        }

        [Fact]
        public void AuthenticationRegistration_ReturnsSuccessTrue()
        {
            _userRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUsers);

            _authenticationService = new AuthenticationService(_userRepositoryMock.Object);

            var user = new User { Id = 4, UserName = "user6", Password = "abcd"};

            var result = _authenticationService.Register(user);

            Assert.True(result.Success);   

        }

        [Fact]
        public void AuthenticationRegistration_ReturnsTwoErrors()
        {
            _userRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUsers);

            _authenticationService = new AuthenticationService(_userRepositoryMock.Object);

            var user = new User { Id = 4, UserName = "us", Password = "ab" };

            var result = _authenticationService.Register(user);

            Assert.Contains(result.Errors, error => error == "Invalid username.");
            Assert.Contains(result.Errors, error => error == "Invalid password.");
            Assert.Equal(2, result.Errors.Count);
        }

        [Fact]
        public void AuthenticationRegistration_ReturnsErrorUsernameTaken()
        {
            var testUser = _exampleUsers
                .First();

            _userRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUsers);

            _userRepositoryMock
                .Setup(x => x.Get(It.IsAny<Func<User, bool>>()))
                .Returns(testUser);

            _authenticationService = new AuthenticationService(_userRepositoryMock.Object);

            var user = new User { Id = 4, UserName = "user1", Password = "abffdd" };

            var result = _authenticationService.Register(user);

            Assert.Contains(result.Errors, error => error == "This username is already taken.");

        }

        [Fact]
        public void AuthenticationLogin_ReturnsErrorsInvalid()
        { 
            _userRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUsers);

            _authenticationService = new AuthenticationService(_userRepositoryMock.Object);

            var user = new User { UserName = "ue", Password = "af" };

            var result = _authenticationService.Login(user);

            Assert.Contains(result.Errors, error => error == "Invalid username.");
            Assert.Contains(result.Errors, error => error == "Invalid password.");

        }

        [Fact]
        public void AuthenticationLogin_ReturnsErrorsWrongPassword()
        {
            var testUser = _exampleUsers
                .First();

            _userRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUsers);

            _userRepositoryMock
                .Setup(x => x.Get(It.IsAny<Func<User, bool>>()))
                .Returns(testUser);

            _authenticationService = new AuthenticationService(_userRepositoryMock.Object);

            var user = new User {UserName = "user1", Password = "abffdd" };

            var result = _authenticationService.Login(user);

            Assert.Contains(result.Errors, error => error == "Incorrect password.");

        }

    }
}
