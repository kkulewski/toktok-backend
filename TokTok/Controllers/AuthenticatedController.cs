using Microsoft.AspNetCore.Mvc;
using TokTok.Models;
using TokTok.Repositories;

namespace TokTok.Controllers
{
    [ApiController]
    public abstract class AuthenticatedController : ControllerBase
    {
        protected IUserRepository UserRepository;
        private User _user;

        protected AuthenticatedController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        protected string CurrentUserToken => Request.Headers.ContainsKey("Authorization") ? Request.Headers["Authorization"].ToString() : null;

        protected User CurrentUser => _user ?? (_user = UserRepository.Get(u => u.Token == CurrentUserToken));
    }
}
