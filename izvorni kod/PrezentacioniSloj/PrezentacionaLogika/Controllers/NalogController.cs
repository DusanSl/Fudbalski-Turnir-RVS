using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.PrezentacionaLogika.ViewModels;
using SlojPodataka.TehnoloskeKlase;

namespace PrezentacioniSloj.PrezentacionaLogika.Kontroleri
{
    public class NalogController : Controller
    {
        private readonly TurnirDbContext _kontekst;

        public NalogController(TurnirDbContext kontekst)
        {
            _kontekst = kontekst;
        }

        [HttpGet]
        public IActionResult Registracija()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registracija(RegistracijaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var postojiKorisnik = _kontekst.Korisnici
                .Any(k => k.KorisnickoIme == model.KorisnickoIme);

            if (postojiKorisnik)
            {
                ModelState.AddModelError("", "Korisničko ime već postoji.");
                return View(model);
            }

            var postojiEmail = _kontekst.Korisnici
                .Any(k => k.Email == model.Email);

            if (postojiEmail)
            {
                ModelState.AddModelError("", "Email adresa je već u upotrebi.");
                return View(model);
            }

            var salt = FunkcijeLozinke.GenerisiSalt();

            var korisnik = new SlojPodataka.KlasePodataka.Korisnik
            {
                KorisnickoIme = model.KorisnickoIme,
                Email = model.Email,
                Salt = salt,
                LozinkaHes = FunkcijeLozinke.IzracunajHash(model.Lozinka, salt)
            };

            _kontekst.Korisnici.Add(korisnik);
            _kontekst.SaveChanges();

            HttpContext.Session.SetString("KorisnickoIme", korisnik.KorisnickoIme);
            return RedirectToAction("Spisak", "Zapisnik");
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

            var salt = FunkcijeLozinke.GenerisiSalt();

            var korisnik = _kontekst.Korisnici
                .FirstOrDefault(k => k.KorisnickoIme == model.KorisnickoIme);

            if (korisnik == null || !FunkcijeLozinke.ProveriLozinku(model.Lozinka, korisnik.Salt, korisnik.LozinkaHes))
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