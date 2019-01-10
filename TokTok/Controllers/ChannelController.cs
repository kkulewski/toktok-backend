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
    public class ChannelController : ControllerBase
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IUserRepository _userRepository;

        public ChannelController(
            IChannelRepository channelRepository,
            IUserRepository userRepository)
        {
            _channelRepository = channelRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChannelDto>> Get()
        {
            var users = _userRepository.GetAll();
            var channels = _channelRepository.GetAll();

            return channels
                .Select(ch => new ChannelDto
                {
                    Name = ch.Name,
                    UserName = users.FirstOrDefault(usr => usr.Id == ch.UserId)?.UserName ?? "NULL",
                }).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ChannelDto> Get(int id)
        {
            var channel = _channelRepository.Get(x => x.Id == id);

            var channelDto = new ChannelDto
            {
                Id = channel.Id,
                Name = channel.Name,
                UserName = _userRepository.Get(x => x.Id == channel.UserId).UserName
            };

            return channelDto;
        }

        [HttpPost]
        public void Post([FromBody] ChannelDto channelDto)
        {
            var channel = new Channel
            {
                Id = 0,
                Name = channelDto.Name,
                UserId = _userRepository.Get(x => x.UserName == channelDto.UserName).Id,
            };

            _channelRepository.Create(channel);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ChannelDto channelDto)
        {
            var channel = new Channel
            {
                Id = id,
                Name = channelDto.Name,
                UserId = _userRepository.Get(x => x.UserName == channelDto.UserName).Id,
            };

            _channelRepository.Update(id, channel);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _channelRepository.Delete(id);
        }
    }
}
