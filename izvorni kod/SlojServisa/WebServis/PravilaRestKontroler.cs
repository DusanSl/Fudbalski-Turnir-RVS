using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace SlojServisa.WebServis
{
    [ApiController]
    [Route("api/[controller]")]
    public class PravilaRestController : ControllerBase
    {
        [HttpGet]
        public IActionResult DohvatiPravila()
        {
            try
            {
                var putanja = Path.Combine(AppContext.BaseDirectory, "Ogranicenja", "pravila_hronologije.xml");

                if (!System.IO.File.Exists(putanja))
                {
                    putanja = Path.Combine(Directory.GetCurrentDirectory(), "Ogranicenja", "pravila_hronologije.xml");
                }

                if (!System.IO.File.Exists(putanja))
                {
                    return StatusCode(500, $"Fajl nije nađen na lokaciji: {putanja}");
                }

                var xml = XDocument.Load(putanja);

                return Ok(new
                {
                    MinimalniRazmakMinuta = int.Parse(xml.Root.Element("MinimalniRazmakMinuta").Value),
                    MaksimalniMinutGola = int.Parse(xml.Root.Element("MaksimalniMinutGola").Value),
                    MinimalniMinutGola = int.Parse(xml.Root.Element("MinimalniMinutGola").Value)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška pri čitanju XML-a: {ex.Message}");
            }
        }
    }
}
