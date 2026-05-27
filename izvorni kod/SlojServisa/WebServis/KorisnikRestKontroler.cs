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
        private readonly KorisnikRepozitorijum _repozitorijum;

        public KorisnikRestController(KorisnikRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
        }

        [HttpPost("prijava")]
        public ActionResult Prijava([FromBody] PrijavaDTO dto)
        {
            var korisnik = _repozitorijum.DohvatiPoKorisnickomImenu(dto.KorisnickoIme);

            if (korisnik == null || !FunkcijeLozinke.ProveriLozinku(
                    dto.Lozinka, korisnik.Salt, korisnik.LozinkaHes))
                return Unauthorized("Pogrešno korisničko ime ili lozinka.");

            return Ok(korisnik.KorisnickoIme);
        }

        [HttpPost("registracija")]
        public ActionResult Registracija([FromBody] RegistracijaDTO dto)
        {
            if (_repozitorijum.PostojiKorisnickoIme(dto.KorisnickoIme))
                return BadRequest("Korisničko ime već postoji.");

            if (_repozitorijum.PostojiEmail(dto.Email))
                return BadRequest("Email adresa je već u upotrebi.");

            var salt = FunkcijeLozinke.GenerisiSalt();
            var korisnik = new Korisnik
            {
                KorisnickoIme = dto.KorisnickoIme,
                Email = dto.Email,
                Salt = salt,
                LozinkaHes = FunkcijeLozinke.IzracunajHash(dto.Lozinka, salt)
            };

            _repozitorijum.Dodaj(korisnik);
            return Ok(korisnik.KorisnickoIme);
        }
    }
}