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

        public async Task<(bool Uspesno, string Poruka)> ValidirajMinutGola(int noviMinut, List<int> postojeciMinuti)
        {
            int minimalniMinut = await _citacPravila.DohvatiMinimalniMinut();
            int maksimalniMinut = await _citacPravila.DohvatiMaksimalniMinut();
            int minimalniRazmak = await _citacPravila.DohvatiMinimalniRazmak();

            if (noviMinut < minimalniMinut || noviMinut > maksimalniMinut)
                return (false, $"Minut gola mora biti između {minimalniMinut} i {maksimalniMinut}.");

            if (postojeciMinuti.Count > 0)
            {
                int poslednjiMinut = postojeciMinuti.Last();
                if (noviMinut < poslednjiMinut + minimalniRazmak)
                    return (false, $"Minut gola ({noviMinut}) mora biti veći od prethodnog ({poslednjiMinut}) za barem {minimalniRazmak} min.");
            }

            return (true, "Validacija uspešna.");
        }
        public (int RezultatDomacin, int RezultatGost) IzracunajRezultat( int domacinId, int gostId, List<SlojPodataka.KlasePodataka.StavkaZapisnika> stavke)
        {
            int golDomacin = stavke.Count(s => s.KlubID == domacinId);
            int golGost = stavke.Count(s => s.KlubID == gostId);
            return (golDomacin, golGost);
        }
    }
}
