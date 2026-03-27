using Microsoft.AspNetCore.Mvc;

namespace PrezentacioniSloj.Kontroler
{
    public class NalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
