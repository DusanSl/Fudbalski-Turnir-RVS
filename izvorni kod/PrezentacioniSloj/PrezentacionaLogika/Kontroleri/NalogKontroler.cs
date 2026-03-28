using Microsoft.AspNetCore.Mvc;

namespace PrezentacioniSloj.PrezentacionaLogika.Kontroler
{
    public class NalogKontroler : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
