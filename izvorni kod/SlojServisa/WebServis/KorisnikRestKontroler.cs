using Microsoft.AspNetCore.Mvc;
using SlojPodataka.KlasePodataka;
using SlojPodataka.TehnoloskeKlase;
using SlojServisa.DTO;

namespace SlojServisa.Webservis
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikRestKontroler : ControllerBase
    {
        private readonly TurnirDbContext _kontekst;

        public KorisnikRestKontroler(TurnirDbContext kontekst)
        {
            _kontekst = kontekst;
        }

        [HttpPost("prijava")]
        public ActionResult Prijava([FromBody] PrijavaDTO dto)
        {
            var korisnik = _kontekst.Korisnici
                .FirstOrDefault(k => k.KorisnickoIme == dto.KorisnickoIme);

            if (korisnik == null)
                return Unauthorized($"Korisnik '{dto.KorisnickoIme}' nije pronađen u bazi.");

            var hashProvera = FunkcijeLozinke.IzracunajHash(dto.Lozinka, korisnik.Salt);

            if (hashProvera != korisnik.LozinkaHes)
                return Unauthorized($"Lozinka ne odgovara. Hash: {hashProvera} | Baza: {korisnik.LozinkaHes}");

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