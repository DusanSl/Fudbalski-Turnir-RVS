using SlojPodataka.KlasePodataka;
using System.Xml.Linq;

namespace SlojPodataka.TehnoloskeKlase
{
    public static class PocetniPodaci
    {
        public static void PopuniSve(TurnirDbContext kontekst, string putanjaXml)
        {
            var xml = XDocument.Load(putanjaXml);

            PopuniKlubove(kontekst, xml);
            PopuniKorisnike(kontekst, xml);
            PopuniZapisnike(kontekst, xml);
            PopuniStavke(kontekst, xml);
        }

        private static void PopuniKlubove(TurnirDbContext kontekst, XDocument xml)
        {
            if (kontekst.Klubovi.Any()) return;

            var klubovi = xml.Descendants("Klub").Select(k => new Klub
            {
                NazivKluba = k.Element("NazivKluba")?.Value ?? string.Empty,
                Grad = k.Element("Grad")?.Value ?? string.Empty,
                Stadion = k.Element("Stadion")?.Value ?? string.Empty,
                Adresa = k.Element("Adresa")?.Value ?? string.Empty,
                BrojIgraca = int.Parse(k.Element("BrojIgraca")?.Value ?? "0"),
                BrojOsvojenihTitula = int.Parse(k.Element("BrojOsvojenihTitula")?.Value ?? "0"),
                GodinaOsnivanja = int.Parse(k.Element("GodinaOsnivanja")?.Value ?? "0")
            }).ToList();

            if (klubovi.Any())
            {
                kontekst.Klubovi.AddRange(klubovi);
                kontekst.SaveChanges();
            }
        }

        private static void PopuniKorisnike(TurnirDbContext kontekst, XDocument xml)
        {
            if (kontekst.Korisnici.Any()) return;

            var korisnici = xml.Descendants("Korisnik").Select(k => new Korisnik
            {
                KorisnickoIme = k.Element("KorisnickoIme")?.Value ?? "",
                Email = k.Element("Email")?.Value ?? "",
                Lozinka = k.Element("Lozinka")?.Value ?? ""
            }).ToList();

            if (korisnici.Any())
            {
                kontekst.Korisnici.AddRange(korisnici);
                kontekst.SaveChanges();
            }
        }

        private static void PopuniZapisnike(TurnirDbContext kontekst, XDocument xml)
        {
            if (kontekst.Zapisnici.Any()) return;

            var zapisnici = xml.Descendants("Zapisnik").Select(z => new Zapisnik
            {
                DatumUtakmice = DateTime.Parse(z.Element("DatumUtakmice")?.Value ?? DateTime.Now.ToString()),
                TerenNaziv = z.Element("TerenNaziv")?.Value ?? "",
                TerenMesto = z.Element("TerenMesto")?.Value ?? "",
                TerenAdresa = z.Element("TerenAdresa")?.Value ?? "",
                DomacinID = int.Parse(z.Element("DomacinID")?.Value ?? "0"),
                GostID = int.Parse(z.Element("GostID")?.Value ?? "0"),
                KonacanRezultatDomacin = int.Parse(z.Element("KonacanRezultatDomacin")?.Value ?? "0"),
                KonacanRezultatGost = int.Parse(z.Element("KonacanRezultatGost")?.Value ?? "0"),
                DatumKreiranja = DateTime.Now
            }).ToList();

            if (zapisnici.Any())
            {
                kontekst.Zapisnici.AddRange(zapisnici);
                kontekst.SaveChanges();
            }
        }

        private static void PopuniStavke(TurnirDbContext kontekst, XDocument xml)
        {
            if (kontekst.StavkeZapisnika.Any()) return;

            var stavke = xml.Descendants("StavkaZapisnika").Select(s => new StavkaZapisnika
            {
                ZapisnikID = int.Parse(s.Element("ZapisnikID")?.Value ?? "0"),
                KlubID = int.Parse(s.Element("KlubID")?.Value ?? "0"),
                MinutGola = int.Parse(s.Element("MinutGola")?.Value ?? "0"),
                ImeStrelca = s.Element("ImeStrelca")?.Value ?? "",
                DatumKreiranja = DateTime.Now
            }).ToList();

            if (stavke.Any())
            {
                kontekst.StavkeZapisnika.AddRange(stavke);
                kontekst.SaveChanges();
            }
        }
    }
}