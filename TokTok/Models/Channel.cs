using System.Collections.Generic;

namespace TokTok.Models
{
    public class Channel : Entity
    {
        public string Name { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Message> Messages { get; set; }
    }
}
