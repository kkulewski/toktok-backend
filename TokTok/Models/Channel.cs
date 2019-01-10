namespace TokTok.Models
{
    public class Channel : Entity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool Hidden { get; set; }
    }
}
