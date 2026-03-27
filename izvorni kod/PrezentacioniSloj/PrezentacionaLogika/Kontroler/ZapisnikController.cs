using Microsoft.AspNetCore.Mvc;

namespace PrezentacioniSloj.Kontroler
{
    public class ZapisnikController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
