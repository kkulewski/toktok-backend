using System.Collections.Generic;

namespace TokTok.Services.Authentication
{
    public class LoginResult
    {
        public bool Success { get; }
        public List<string> Errors { get; }
        public string Token { get; }

        public LoginResult(bool isSuccess, List<string> errors, string token)
        {
            Success = isSuccess;
            Errors = errors;
            Token = token;
        }
    }
}
