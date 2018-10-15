using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokTok.Models;
using TokTok.Repositories;

namespace TokTok.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Message>> Get()
        {
            return _messageRepository.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Message> Get(int id)
        {
            return _messageRepository.Get(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Message message)
        {
            _messageRepository.Create(message);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Message message)
        {
            _messageRepository.Update(id, message);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _messageRepository.Delete(id);
        }
    }
}
