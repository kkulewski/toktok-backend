using System;
using System.Collections.Generic;
using System.Linq;
using TokTok.Data;
using TokTok.Models;

namespace TokTok.Repositories
{
    public class SqliteMessageRepository : IMessageRepository
    {
        private readonly TokTokDbContext _context;

        public SqliteMessageRepository(TokTokDbContext context)
        {
            _context = context;
        }

        public List<Message> Get()
        {
            return _context.Messages.ToList();
        }

        public Message Get(int messageId)
        {
            return _context.Messages.FirstOrDefault(x => x.Id == messageId);
        }

        public void Create(Message message)
        {
            _context.Messages.Add(message);
            Save();
        }

        public void Update(int messageId, Message message)
        {
            _context.Messages.Update(message);
            Save();
        }

        public void Delete(int messageId)
        {
            var messageToDelete = Get(messageId);
            if (messageToDelete != null)
            {
                _context.Messages.Remove(messageToDelete);
            }

            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
