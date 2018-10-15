using Microsoft.AspNetCore.Mvc;

namespace TokTok.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "TokTok is running...";
        }
    }
}
