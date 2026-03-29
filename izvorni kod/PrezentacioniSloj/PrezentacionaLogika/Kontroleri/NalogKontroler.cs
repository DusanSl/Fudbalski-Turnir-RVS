using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.PrezentacionaLogika.ViewModels;
using SlojPodataka.TehnoloskeKlase;

namespace PrezentacioniSloj.PrezentacionaLogika.Kontroleri
{
    public class NalogKontroler : Controller
    {
        private readonly TurnirDbContext _kontekst;

        public NalogKontroler(TurnirDbContext kontekst)
        {
            _kontekst = kontekst;
        }

        [HttpGet]
        public IActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prijava(PrijavaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var korisnik = _kontekst.Korisnici
                .FirstOrDefault(k => k.KorisnickoIme == model.KorisnickoIme
                                  && k.Lozinka == model.Lozinka);

            if (korisnik == null)
            {
                ModelState.AddModelError("", "Pogrešno korisničko ime ili lozinka.");
                return View(model);
            }

            HttpContext.Session.SetString("KorisnickoIme", korisnik.KorisnickoIme);
            return RedirectToAction("Spisak", "Zapisnik");
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Prijava");
        }
    }
}