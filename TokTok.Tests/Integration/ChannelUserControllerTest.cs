using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TokTok.Controllers;
using TokTok.Models;
using TokTok.Repositories;
using Xunit;

namespace TokTok.Tests.Integration
{
    public class ChannelUserControllerTest
    {
        private readonly Mock<IUserRepository>_userRepositoryMock;
        private readonly Mock<IUserInChannelRepository> _userInChannelRepositoryMock;
        private readonly Mock<IChannelRepository> _channelRepositoryMock;
        private readonly Mock<IMessageRepository> _messageRepositoryMock;

        private readonly List<User> _exampleUsers;
        private readonly List<Channel> _exampleChannels;
        private readonly List<UserInChannel> _exampleUserInChannels;

        public ChannelUserControllerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userInChannelRepositoryMock = new Mock<IUserInChannelRepository>();
            _channelRepositoryMock = new Mock<IChannelRepository>();
            _messageRepositoryMock = new Mock<IMessageRepository>();

            _exampleUsers = new List<User>
            {
                new User {Id = 1, UserName = "user1"},
                new User {Id = 2, UserName = "user2"},
                new User {Id = 3, UserName = "user3"}
            };

            _exampleChannels = new List<Channel>
            {
                new Channel {Id = 1, Name = "user1_ch1", UserId = 1, Hidden = false},
                new Channel {Id = 2, Name = "user1_ch2", UserId = 1, Hidden = false},
                new Channel {Id = 3, Name = "user2_ch1", UserId = 2, Hidden = false},
                new Channel {Id = 4, Name = "user3_ch1", UserId = 3, Hidden = false},
                new Channel {Id = 5, Name = "user3_ch2", UserId = 3, Hidden = false}
            };

            _exampleUserInChannels = new List<UserInChannel>
            {
                new UserInChannel {Id = 1, ChannelId = 3, UserId = 1},
                new UserInChannel {Id = 2, ChannelId = 5, UserId = 1}
            };
        }


        [Fact]
        public void GetAllowedChannels_ReturnsExpectedNumberOfChannels()
        {
            // Arrange
            var testUser = _exampleUsers
                .First();

            _userRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUsers);

            _userRepositoryMock
                .Setup(x => x.Get(It.IsAny<Func<User, bool>>()))
                .Returns(testUser);

            _channelRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleChannels);

            _userInChannelRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUserInChannels);

            var controller = new ChannelUserController(
                _channelRepositoryMock.Object,
                _userInChannelRepositoryMock.Object,
                _messageRepositoryMock.Object,
                _userRepositoryMock.Object);

            // Act
            var result = controller
                .GetAllowedChannels("user1")
                .Value
                .ToList();

            // Assert
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void GetAllowedChannels_ReturnsChannels_IncludingTheseCreatedByUser()
        {
            // Arrange
            var testUser = _exampleUsers
                .First();

            _userRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUsers);

            _userRepositoryMock
                .Setup(x => x.Get(It.IsAny<Func<User, bool>>()))
                .Returns(testUser);

            _channelRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleChannels);

            _userInChannelRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(_exampleUserInChannels);

            var controller = new ChannelUserController(
                _channelRepositoryMock.Object,
                _userInChannelRepositoryMock.Object,
                _messageRepositoryMock.Object,
                _userRepositoryMock.Object);

            // Act
            var result = controller
                .GetAllowedChannels("user1")
                .Value
                .ToList();

            Assert.Contains(result, channel => channel.Name == "user1_ch1");
            Assert.Contains(result, channel => channel.Name == "user1_ch2");
        }
    }
}
