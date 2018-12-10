using TokTok.Models;

namespace TokTok.Services.Authentication
{
    public interface IAuthenticationService
    {
        RegisterResult Register(User user);
    }
}
