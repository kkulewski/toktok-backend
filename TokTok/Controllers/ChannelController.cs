using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TokTok.DTO;
using TokTok.Models;
using TokTok.Repositories;

namespace TokTok.Controllers
{
    [Route("[controller]")]
    public class ChannelController : AuthenticatedController
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelController(
            IChannelRepository channelRepository,
            IUserRepository userRepository) : base(userRepository)
        {
            _channelRepository = channelRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChannelDto>> Get()
        {
            var users = UserRepository.GetAll();
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
                UserName = UserRepository.Get(x => x.Id == channel.UserId).UserName
            };

            return channelDto;
        }

        [HttpPost]
        public ActionResult Post([FromBody] ChannelDto channelDto)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            var channelAlreadyExists = _channelRepository.Get(x => x.Name == channelDto.Name) != null;
            if (channelAlreadyExists)
            {
                return BadRequest();
            }

            var channel = new Channel
            {
                Id = 0,
                Name = channelDto.Name,
                UserId = CurrentUser.Id,
                Hidden = false
            };

            _channelRepository.Create(channel);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ChannelDto channelDto)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            var oldChannel = _channelRepository.Get(x => x.Id == id);
            if (oldChannel == null)
            {
                return BadRequest();
            }

            if (oldChannel.UserId != CurrentUser.Id)
            {
                return Forbid();
            }

            var channel = new Channel
            {
                Id = id,
                Name = channelDto.Name,
                UserId = CurrentUser.Id,
                Hidden = false
            };

            _channelRepository.Update(id, channel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            var channel = _channelRepository.Get(x => x.Id == id);
            if (channel == null)
            {
                return BadRequest();
            }

            if (channel.UserId != CurrentUser.Id)
            {
                return Forbid();
            }

            channel.Hidden = true;
            _channelRepository.Update(id, channel);
            return Ok();
        }
    }
}
