using System.Collections.Generic;

namespace TokTok.Services.Authentication
{
    public class LoginResult
    {
        public bool Success { get; }
        public List<string> Errors { get; }
        public string Token { get; }
        public string UserName { get; }

        public LoginResult(bool isSuccess, List<string> errors, string token, string userName)
        {
            Success = isSuccess;
            Errors = errors;
            Token = token;
            UserName = userName;
        }
    }
}
