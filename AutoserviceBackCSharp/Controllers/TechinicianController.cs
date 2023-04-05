using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    public class TechinicianController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
