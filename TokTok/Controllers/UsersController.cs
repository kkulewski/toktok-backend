using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokTok.Models;
using TokTok.Repositories;
using TokTok.Services.Authentication;

namespace TokTok.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authService;

        public UsersController(IUserRepository userRepository, IAuthenticationService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _userRepository.GetAll();
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
