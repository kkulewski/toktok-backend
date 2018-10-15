using Microsoft.AspNetCore.Mvc;

namespace TokTok.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult<string> Index()
        {
            return "TokTok is running...";
        }
    }
}
