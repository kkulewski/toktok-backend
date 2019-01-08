using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokTok.Models;
using TokTok.Repositories;
using TokTok.Services.Authentication;

namespace TokTok.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authService;

        public UserController(IUserRepository userRepository, IAuthenticationService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _userRepository.GetAll();
        }

        [HttpGet("user/{token}")]
        public ActionResult<User> GetUserByToken(string token)
        {
            return _userRepository.Get(x => x.Token == token);
        }

        [HttpGet("username/{id}")]
        public ActionResult<string> GetUsernameById(int id)
        {
            return _userRepository.Get(x => x.Id == id).UserName;
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
