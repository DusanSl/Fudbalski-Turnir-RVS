using System;
using System.Collections.Generic;
using System.Text;

namespace SlojPodataka.TehnoloskeKlase
{
    public class KlubRepoDBUtils : Tabela
    {
        public int IzbrojKlubove()
        {
            var dt = IzvrsiUpit("SELECT COUNT(*) FROM Klubovi");
            return (int)dt.Rows[0][0];
        }
    }
}
