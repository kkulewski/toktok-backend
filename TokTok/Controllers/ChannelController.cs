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
                .Where(c => !c.Hidden)
                .Select(ch => new ChannelDto
                {
                    Id = ch.Id,
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
        public ActionResult Post([FromBody] ChannelDto channelDto)
        {
            var channelAlreadyExists = _channelRepository.Get(x => x.Name == channelDto.Name) != null;
            if (channelAlreadyExists)
            {
                return BadRequest();
            }

            var channel = new Channel
            {
                Id = 0,
                Name = channelDto.Name,
                UserId = _userRepository.Get(x => x.UserName == channelDto.UserName).Id,
                Hidden = false
            };

            _channelRepository.Create(channel);
            return Ok();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ChannelDto channelDto)
        {
            var channel = new Channel
            {
                Id = id,
                Name = channelDto.Name,
                UserId = _userRepository.Get(x => x.UserName == channelDto.UserName).Id,
                Hidden = false
            };

            _channelRepository.Update(id, channel);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var channel = _channelRepository.Get(x => x.Id == id);
            if (channel != null)
            {
                channel.Hidden = true;
                _channelRepository.Update(id, channel);
            }
        }
    }
}
