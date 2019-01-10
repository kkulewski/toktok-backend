using System;

namespace TokTok.DTO
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SentDate { get; set; }
        public string UserName { get; set; }
        public string ChannelName { get; set; }
    }
}
