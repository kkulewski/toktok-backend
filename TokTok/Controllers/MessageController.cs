using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TokTok.DTO;
using TokTok.Models;
using TokTok.Repositories;

namespace TokTok.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChannelRepository _channelRepository;

        public MessageController(
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            IChannelRepository channelRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _channelRepository = channelRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MessageDto>> Get()
        {
            var messages = _messageRepository.GetAll();
            var users = _userRepository.GetAll();
            var channels = _channelRepository.GetAll();

            var messageDtos = messages
                .Select(msg => new MessageDto
                {
                    Id = msg.Id,
                    Text = msg.Text,
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
                UserName = _userRepository.Get(x => x.Id == message.UserId).UserName,
                ChannelName = _channelRepository.Get(x => x.Id == message.ChannelId).Name
            };

            return messageDto;
        }

        [HttpPost]
        public void Post([FromBody] MessageDto messageDto)
        {
            var message = new Message
            {
                Id = 0,
                Text = messageDto.Text,
                SentDate = messageDto.SentDate,
                UserId = _userRepository.Get(x => x.UserName == messageDto.UserName).Id,
                ChannelId = _channelRepository.Get(x => x.Name == messageDto.ChannelName).Id
            };

            _messageRepository.Create(message);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MessageDto messageDto)
        {
            var message = new Message
            {
                Id = id,
                Text = messageDto.Text,
                SentDate = messageDto.SentDate,
                UserId = _userRepository.Get(x => x.UserName == messageDto.UserName).Id,
                ChannelId = _channelRepository.Get(x => x.Name == messageDto.ChannelName).Id
            };

            _messageRepository.Update(id, message);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _messageRepository.Delete(id);
        }
    }
}
