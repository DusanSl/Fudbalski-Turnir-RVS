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

        public KluboviRestController(ZapisnikRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
            _mapper = new KlubMapper();
        }

        [HttpGet]
        public ActionResult<List<KlubDTO>> DohvatiSve()
        {
            var klubovi = _repozitorijum.DohvatiKlubove();
            return Ok(_mapper.UListuDTO(klubovi));
        }
    }
}