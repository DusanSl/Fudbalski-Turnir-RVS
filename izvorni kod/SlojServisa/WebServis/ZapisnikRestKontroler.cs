using Microsoft.AspNetCore.Mvc;
using SlojPodataka.KlasePodataka;
using SlojPodataka.TehnoloskeKlase;
using SlojPoslovneLogike.Ogranicenja;
using SlojPoslovneLogike.Stanje;
using SlojPoslovneLogike.Validacija;
using SlojServisa.DTO;
using SlojServisa.KlaseMapiranja;

namespace SlojServisa.Webservis
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZapisnikRestController : ControllerBase
    {
        private readonly ZapisnikRepozitorijum _repozitorijum;
        private readonly ZapisnikMapper _mapper;
        private readonly PoslovnoPraviloValidator _validator;
        private readonly PrikupljanjeStanja _stanje;

        public ZapisnikRestController(ZapisnikRepozitorijum repozitorijum, CitacPravila citacPravila)
        {
            _repozitorijum = repozitorijum;
            _mapper = new ZapisnikMapper();
            _validator = new PoslovnoPraviloValidator(citacPravila);
            _stanje = new PrikupljanjeStanja(repozitorijum);
        }

        [HttpGet]
        public ActionResult<List<ZapisnikDTO>> DohvatiSve()
        {
            var zapisnici = _repozitorijum.DohvatiSve();
            return Ok(_mapper.UListuDTO(zapisnici));
        }

        [HttpGet("{id}")]
        public ActionResult<ZapisnikDTO> DohvatiPoId(int id)
        {
            var zapisnik = _repozitorijum.DohvatiPoId(id);
            if (zapisnik == null)
                return NotFound($"Zapisnik sa ID-em {id} nije pronađen.");

            return Ok(_mapper.UDTO(zapisnik));
        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] ZapisnikDTO dto)
        {
            var postojiZapisnik = _repozitorijum.DohvatiSve()
                .Any(z => z.DomacinID == dto.DomacinID
                       && z.GostID == dto.GostID
                       && z.DatumUtakmice.Date == dto.DatumUtakmice.Date);

            if (postojiZapisnik)
                return BadRequest("Već postoji zapisnik za ovu utakmicu na isti datum.");

            var postojeciMinuti = new List<int>();
            foreach (var stavka in dto.Stavke)
            {
                var (uspesno, poruka) = _validator.ValidirajMinutGola(stavka.MinutGola, postojeciMinuti);
                if (!uspesno)
                    return BadRequest(poruka);

                postojeciMinuti.Add(stavka.MinutGola);
            }

            var zapisnik = _mapper.UEntitet(dto);

            var (resDomacin, resGost) = _validator.IzracunajRezultat(
                dto.DomacinID, dto.GostID, zapisnik.Stavke.ToList());
            zapisnik.KonacanRezultatDomacin = resDomacin;
            zapisnik.KonacanRezultatGost = resGost;

            _repozitorijum.Dodaj(zapisnik);
            return Ok("Zapisnik je uspešno dodat.");
        }

        [HttpPut("{id}")]
        public ActionResult Izmeni(int id, [FromBody] ZapisnikDTO dto)
        {
            dto.ZapisnikID = id;

            var postojeciMinuti = new List<int>();

            foreach (var stavka in dto.Stavke)
            {
                var (uspesno, poruka) = _validator.ValidirajMinutGola(stavka.MinutGola, postojeciMinuti);
                if (!uspesno)
                    return BadRequest(poruka);

                postojeciMinuti.Add(stavka.MinutGola);
            }

            var zapisnik = _mapper.UEntitet(dto);

            var (resDomacin, resGost) = _validator.IzracunajRezultat(
                dto.DomacinID, dto.GostID, zapisnik.Stavke.ToList());
            zapisnik.KonacanRezultatDomacin = resDomacin;
            zapisnik.KonacanRezultatGost = resGost;

            _repozitorijum.Izmeni(zapisnik);
            return Ok("Zapisnik je uspešno izmenjen.");
        }

        [HttpDelete("{id}")]
        public ActionResult Obrisi(int id)
        {
            _repozitorijum.Obrisi(id);
            return Ok("Zapisnik je uspešno obrisan.");
        }

        [HttpGet("filter")]
        public ActionResult<List<ZapisnikDTO>> Filtriraj(
            [FromQuery] DateTime? datumOd,
            [FromQuery] DateTime? datumDo,
            [FromQuery] int? klubId)
        {
            var zapisnici = _repozitorijum.Filtriraj(datumOd, datumDo, klubId);
            return Ok(_mapper.UListuDTO(zapisnici));
        }
    }
}