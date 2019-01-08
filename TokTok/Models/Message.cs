using System;

namespace TokTok.Models
{
    public class Message : Entity
    {
        public string Text { get; set; }

        public DateTime SentDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}
