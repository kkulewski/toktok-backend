namespace TokTok.Models
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
