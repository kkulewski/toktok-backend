using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TokTok.DTO;
using TokTok.Models;
using TokTok.Repositories;

namespace TokTok.Controllers
{
    public class ChannelUserController : AuthenticatedController
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IUserInChannelRepository _userInChannelRepository;
        private readonly IMessageRepository _messageRepository;

        public ChannelUserController(
            IChannelRepository channelRepository,
            IUserInChannelRepository userInChannelRepository,
            IMessageRepository messageRepository,
            IUserRepository userRepository) : base(userRepository)
        {
            _channelRepository = channelRepository;
            _userInChannelRepository = userInChannelRepository;
            _messageRepository = messageRepository;
        }

        [Route("channels")]
        [HttpGet]
        public ActionResult<IEnumerable<ChannelDto>> GetAllowedChannels()
        {
            var user = CurrentUser;
            if (user == null)
            {
                return Unauthorized();
            }

            var userAllowedChannels = GetAllowedChannelsForGivenUser(user);
            var users = UserRepository.GetAll();

            return userAllowedChannels
                .Where(c => !c.Hidden)
                .Select(ch => new ChannelDto
                {
                    Id = ch.Id,
                    Name = ch.Name,
                    UserName = users.FirstOrDefault(usr => usr.Id == ch.UserId)?.UserName ?? "NULL",
                }).ToList();
        }

        [Route("messages")]
        [HttpGet]
        public ActionResult<IEnumerable<MessageDto>> GetAllowedChannelsMessages()
        {
            var user = CurrentUser;
            if (user == null)
            {
                return Unauthorized();
            }

            var messages = _messageRepository.GetAll();
            var users = UserRepository.GetAll();
            var channels = GetAllowedChannelsForGivenUser(user);
            var allowedMessages = messages.Where(msg => channels.Any(ch => ch.Id == msg.ChannelId));
            var messageDtos = allowedMessages
                .Select(msg => new MessageDto
                {
                    Id = msg.Id,
                    Text = msg.Text,
                    SentDate = msg.SentDate,
                    UserName = users.FirstOrDefault(usr => usr.Id == msg.UserId)?.UserName ?? "NULL",
                    ChannelName = channels.FirstOrDefault(chn => chn.Id == msg.ChannelId)?.Name ?? "NULL"
                })
                .ToList();
            
            return messageDtos;
        }

        [Route("invitations/{channelId}")]
        [HttpGet]
        public ActionResult<IEnumerable<UserInChannelDto>> UsersInChannel(int channelId)
        {
            var channels = _channelRepository.GetAll();
            var users = UserRepository.GetAll();

            var usersInChannel = _userInChannelRepository
                .GetAll()
                .Where(x => x.ChannelId == channelId)
                .Select(uc => new UserInChannelDto
                {
                    Id = uc.Id,
                    ChannelName = channels.FirstOrDefault(ch => ch.Id == uc.ChannelId)?.Name ?? "NULL",
                    UserName = users.FirstOrDefault(u => u.Id == uc.UserId)?.UserName ?? "NULL"
                })
                .ToList();

            return usersInChannel;
        }

        [Route("invitations")]
        [HttpPost]
        public ActionResult AddUserToChannel([FromBody] UserInChannelDto userInChannelDto)
        {
            var user = UserRepository.Get(x => x.UserName == userInChannelDto.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var channel = _channelRepository.Get(x => x.Name == userInChannelDto.ChannelName);
            if (channel == null)
            {
                return BadRequest();
            }

            var mapping = _userInChannelRepository.Get(x => x.UserId == user.Id && x.ChannelId == channel.Id);
            if (mapping != null)
            {
                return BadRequest();
            }

            var newMapping = new UserInChannel
            {
                Id = 0,
                UserId = user.Id,
                ChannelId = channel.Id
            };

            _userInChannelRepository.Create(newMapping);
            return Ok();
        }

        [Route("invitations/{invitationId}")]
        [HttpDelete]
        public ActionResult RemoveUserFromChannel(int invitationId)
        {
            var mapping = _userInChannelRepository.Get(x => x.Id == invitationId);
            if (mapping == null)
            {
                return BadRequest();
            }

            _userInChannelRepository.Delete(mapping.Id);
            return Ok();
        }

        private List<Channel> GetAllowedChannelsForGivenUser(User user)
        {
            if (user == null)
            {
                return new List<Channel>();
            }

            var channels = _channelRepository.GetAll();

            var idsOfChannelsUserHasCreated = channels
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Id);

            var idsOfChannelsUserIsInvitedTo = _userInChannelRepository
                .GetAll()
                .Where(u => u.UserId == user.Id)
                .Select(x => x.ChannelId);

            var allowedChannelIds = idsOfChannelsUserHasCreated
                .Concat(idsOfChannelsUserIsInvitedTo)
                .Distinct();

            return channels
                .Where(x => allowedChannelIds.Contains(x.Id))
                .ToList();
        }
    }
}
