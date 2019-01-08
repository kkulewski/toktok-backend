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

        [HttpGet]
        public ActionResult<IEnumerable<Message>> Get()
        {
            return _messageRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Message> Get(int id)
        {
            return _messageRepository.Get(x => x.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] Message message)
        {
            _messageRepository.Create(message);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Message message)
        {
            _messageRepository.Update(id, message);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _messageRepository.Delete(id);
        }
    }
}
