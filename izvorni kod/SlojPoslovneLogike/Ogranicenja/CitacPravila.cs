using System.Xml.Linq;

namespace SlojPoslovneLogike.Ogranicenja
{
    public class CitacPravila
    {
        private readonly string _putanjaXml;

        public CitacPravila(string putanjaXml)
        {
            _putanjaXml = putanjaXml;
        }

        public int DohvatiMinimalniRazmak()
        {
            var xml = XDocument.Load(_putanjaXml);
            var vrednost = xml.Root?.Element("MinimalniRazmakMinuta")?.Value;
            return int.TryParse(vrednost, out int rezultat) ? rezultat : 1;
        }

        public int DohvatiMaksimalniMinut()
        {
            var xml = XDocument.Load(_putanjaXml);
            var vrednost = xml.Root?.Element("MaksimalniMinutGola")?.Value;
            return int.TryParse(vrednost, out int rezultat) ? rezultat : 90;
        }

        public int DohvatiMinimalniMinut()
        {
            var xml = XDocument.Load(_putanjaXml);
            var vrednost = xml.Root?.Element("MinimalniMinutGola")?.Value;
            return int.TryParse(vrednost, out int rezultat) ? rezultat : 1;
        }
    }
}