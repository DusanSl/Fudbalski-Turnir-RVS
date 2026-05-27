using Microsoft.AspNetCore.Mvc;
using SlojPodataka.KlasePodataka;
using SlojPodataka.TehnoloskeKlase;
using SlojServisa.DTO;

namespace SlojServisa.Webservis
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikRestController : ControllerBase
    {
        private readonly TurnirDbContext _kontekst;

        public KorisnikRestController(TurnirDbContext kontekst)
        {
            _kontekst = kontekst;
        }

        [HttpPost("prijava")]
        public ActionResult Prijava([FromBody] PrijavaDTO dto)
        {
            var korisnik = _kontekst.Korisnici
                .FirstOrDefault(k => k.KorisnickoIme == dto.KorisnickoIme);

            if (korisnik == null || !FunkcijeLozinke.ProveriLozinku(
                    dto.Lozinka, korisnik.Salt, korisnik.LozinkaHes))
                return Unauthorized("Pogrešno korisničko ime ili lozinka.");

            return Ok(korisnik.KorisnickoIme);
        }

        [HttpPost("registracija")]
        public ActionResult Registracija([FromBody] RegistracijaDTO dto)
        {
            if (_kontekst.Korisnici.Any(k => k.KorisnickoIme == dto.KorisnickoIme))
                return BadRequest("Korisničko ime već postoji.");

            if (_kontekst.Korisnici.Any(k => k.Email == dto.Email))
                return BadRequest("Email adresa je već u upotrebi.");

            var salt = FunkcijeLozinke.GenerisiSalt();
            var korisnik = new Korisnik
            {
                KorisnickoIme = dto.KorisnickoIme,
                Email = dto.Email,
                Salt = salt,
                LozinkaHes = FunkcijeLozinke.IzracunajHash(dto.Lozinka, salt)
            };

            _kontekst.Korisnici.Add(korisnik);
            _kontekst.SaveChanges();
            return Ok(korisnik.KorisnickoIme);
        }
    }
}