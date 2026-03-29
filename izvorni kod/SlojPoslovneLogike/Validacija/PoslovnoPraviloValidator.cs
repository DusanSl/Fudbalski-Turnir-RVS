using System;
using System.Collections.Generic;
using System.Text;
using SlojPoslovneLogike.Ogranicenja;

namespace SlojPoslovneLogike.Validacija
{
    public class PoslovnoPraviloValidator
    {
        private readonly CitacPravila _citacPravila;

        public PoslovnoPraviloValidator(CitacPravila citacPravila)
        {
            _citacPravila = citacPravila;
        }

        public (bool Uspesno, string Poruka) ValidirajMinutGola(int noviMinut, List<int> postojeciMinuti)
        {
            int minimalniMinut = _citacPravila.DohvatiMinimalniMinut();
            int maksimalniMinut = _citacPravila.DohvatiMaksimalniMinut();
            int minimalniRazmak = _citacPravila.DohvatiMinimalniRazmak();

            if (noviMinut < minimalniMinut || noviMinut > maksimalniMinut)
                return (false, $"Minut gola mora biti između {minimalniMinut} i {maksimalniMinut}.");

            if (postojeciMinuti.Count > 0)
            {
                int posledniMinut = postojeciMinuti.Last();
                if (noviMinut < posledniMinut + minimalniRazmak)
                    return (false, $"Minut gola ({noviMinut}) mora biti veći od prethodnog minuta ({posledniMinut}).");
            }

            return (true, "Validacija uspešna.");
        }
        public (int RezultatDomacin, int RezultatGost) IzracunajRezultat( int domacinId, int gostId, List<SlojPodataka.KlasePodataka.StavkaZapisnika> stavke)
        {
            int golDomacin = stavke.Count(s => s.TimID == domacinId);
            int golGost = stavke.Count(s => s.TimID == gostId);
            return (golDomacin, golGost);
        }
    }
}
