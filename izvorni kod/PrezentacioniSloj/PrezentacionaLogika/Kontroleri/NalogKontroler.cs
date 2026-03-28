using Microsoft.AspNetCore.Mvc;

namespace PrezentacioniSloj.PrezentacionaLogika.Kontroler
{
    public class NalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
