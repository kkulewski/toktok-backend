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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _userRepository.GetUsers();
        }

        [HttpPost]
        public ActionResult<RegisterResult> Register([FromBody] User newUser)
        {
            return Ok(_authService.Register(newUser));
        }
    }
}
