using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TokTok.DTO;
using TokTok.Models;
using TokTok.Repositories;

namespace TokTok.Controllers
{
    [Route("[controller]")]
    public class MessageController : AuthenticatedController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChannelRepository _channelRepository;

        public MessageController(
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            IChannelRepository channelRepository) : base(userRepository)
        {
            _messageRepository = messageRepository;
            _channelRepository = channelRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MessageDto>> Get()
        {
            var messages = _messageRepository.GetAll();
            var users = UserRepository.GetAll();
            var channels = _channelRepository.GetAll();

            var messageDtos = messages
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

        [HttpGet("{id}")]
        public ActionResult<MessageDto> Get(int id)
        {
            var message = _messageRepository.Get(x => x.Id == id);

            var messageDto = new MessageDto
            {
                Id = message.Id,
                Text = message.Text,
                SentDate = message.SentDate,
                UserName = UserRepository.Get(x => x.Id == message.UserId).UserName,
                ChannelName = _channelRepository.Get(x => x.Id == message.ChannelId).Name
            };

            return messageDto;
        }

        [HttpPost]
        public ActionResult Post([FromBody] MessageDto messageDto)
        {
            if (CurrentUser == null)
            {
                return BadRequest();
            }

            var message = new Message
            {
                Id = 0,
                Text = messageDto.Text,
                SentDate = messageDto.SentDate,
                UserId = CurrentUser.Id,
                ChannelId = _channelRepository.Get(x => x.Name == messageDto.ChannelName).Id
            };

            _messageRepository.Create(message);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] MessageDto messageDto)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            var oldMessage = _messageRepository.Get(x => x.Id == id);
            if (oldMessage == null)
            {
                return BadRequest();
            }

            if (oldMessage.UserId != CurrentUser.Id)
            {
                return Forbid();
            }

            var message = new Message
            {
                Id = id,
                Text = messageDto.Text,
                SentDate = messageDto.SentDate,
                UserId = CurrentUser.Id,
                ChannelId = _channelRepository.Get(x => x.Name == messageDto.ChannelName).Id
            };

            _messageRepository.Update(id, message);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            var message = _messageRepository.Get(x => x.Id == id);
            if (message == null)
            {
                return BadRequest();
            }

            if (message.UserId != CurrentUser.Id)
            {
                return Forbid();
            }

            _messageRepository.Delete(id);
            return Ok();
        }
    }
}
