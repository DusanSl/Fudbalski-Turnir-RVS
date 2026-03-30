using SlojPodataka.KlasePodataka;
using System.Xml.Linq;

namespace SlojPodataka.TehnoloskeKlase
{
    public static class PocetniPodaci
    {
        public static void PopuniKlubove(TurnirDbContext kontekst, string putanjaXml)
        {
            if (kontekst.Klubovi.Any()) return;

            var xml = XDocument.Load(putanjaXml);
            var klubovi = xml.Root?.Elements("Klub").Select(k => new Klub
            {
                NazivKluba = k.Element("NazivKluba")?.Value ?? string.Empty,
                Grad = k.Element("Grad")?.Value ?? string.Empty,
                Stadion = k.Element("Stadion")?.Value ?? string.Empty,
                Adresa = k.Element("Adresa")?.Value ?? string.Empty,
                BrojIgraca = int.Parse(k.Element("BrojIgraca")?.Value ?? "0"),
                BrojOsvojenihTitula = int.Parse(k.Element("BrojOsvojenihTitula")?.Value ?? "0"),
                GodinaOsnivanja = int.Parse(k.Element("GodinaOsnivanja")?.Value ?? "0")
            }).ToList();

            if (klubovi != null && klubovi.Any())
            {
                kontekst.Klubovi.AddRange(klubovi);
                kontekst.SaveChanges();
            }
        }
    }
}