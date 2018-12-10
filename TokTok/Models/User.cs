using Microsoft.AspNetCore.Identity;

namespace TokTok.Models
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
