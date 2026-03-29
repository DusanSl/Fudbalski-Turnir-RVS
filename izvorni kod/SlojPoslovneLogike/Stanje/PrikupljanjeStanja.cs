using System;
using System.Collections.Generic;
using System.Text;
using SlojPodataka.TehnoloskeKlase;

namespace SlojPoslovneLogike.Stanje
{
    public class PrikupljanjeStanja
    {
        private readonly ZapisnikRepozitorijum _repozitorijum;

        public PrikupljanjeStanja(ZapisnikRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
        }

        public List<int> DohvatiPostojeceMinute(int zapisnikId)
        {
            return _repozitorijum.DohvatiMinuteGolova(zapisnikId);
        }
    }
}
