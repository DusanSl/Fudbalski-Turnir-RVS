using Microsoft.AspNetCore.Mvc;
using SlojPodataka.KlasePodataka;
using SlojPodataka.TehnoloskeKlase;
using SlojServisa.DTO;
using SlojServisa.KlaseMapiranja;

namespace SlojServisa.Webservis
{
    [ApiController]
    [Route("api/[controller]")]
    public class KluboviRestController : ControllerBase
    {
        private readonly ZapisnikRepozitorijum _repozitorijum;
        private readonly KlubMapper _mapper;
        private readonly KlubRepoDBUtils _dbUtils;

        public KluboviRestController(ZapisnikRepozitorijum repozitorijum, KlubRepoDBUtils dbUtils)
        {
            _repozitorijum = repozitorijum;
            _mapper = new KlubMapper();
            _dbUtils = dbUtils;
        }


        [HttpGet("broj")]
        public ActionResult<int> DohvatiBrojKlubova()
        {
            int broj = _dbUtils.IzbrojKlubove();
            return Ok(broj);
        }

        [HttpGet]
        public ActionResult<List<KlubDTO>> DohvatiSve()
        {
            var klubovi = _repozitorijum.DohvatiKlubove();
            return Ok(_mapper.UListuDTO(klubovi));
        }

        [HttpGet("nazivi")]
        public ActionResult<List<string>> DohvatiNazive()
        {
            var nazivi = _dbUtils.DohvatiNaziveKlubova();
            return Ok(nazivi);
        }
    }
}