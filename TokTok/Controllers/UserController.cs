using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokTok.Models;
using TokTok.Repositories;
using TokTok.Services.Authentication;

namespace TokTok.Controllers
{
    [Route("[controller]")]
    public class UserController : AuthenticatedController
    {
        private readonly IAuthenticationService _authService;

        public UserController(IUserRepository userRepository, IAuthenticationService authService) : base(userRepository)
        {
            _authService = authService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return UserRepository.GetAll();
        }

        [HttpGet("token/{token}")]
        public ActionResult<User> GetUserByToken(string token)
        {
            return UserRepository.Get(x => x.Token == token);
        }

        [HttpGet("name/{id}")]
        public ActionResult<string> GetUsernameById(int id)
        {
            return UserRepository.Get(x => x.Id == id).UserName;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<RegisterResult> Register([FromBody] User user)
        {
            return Ok(_authService.Register(user));
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<LoginResult> Login([FromBody] User user)
        {
            return Ok(_authService.Login(user));
        }
    }
}
