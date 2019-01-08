using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokTok.Models;
using TokTok.Repositories;

namespace TokTok.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelController(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Channel>> Get()
        {
            return _channelRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Channel> Get(int id)
        {
            return _channelRepository.Get(x => x.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] Channel channel)
        {
            _channelRepository.Create(channel);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Channel channel)
        {
            _channelRepository.Update(id, channel);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _channelRepository.Delete(id);
        }
    }
}
