using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SlojPodataka.TehnoloskeKlase
{
    public class KlubRepoDBUtils : Tabela
    {
        public int IzbrojKlubove()
        {
            var dt = IzvrsiUpit("SELECT COUNT(*) FROM Klub");
            return (int)dt.Rows[0][0];
        }

        public List<string> DohvatiNaziveKlubova()
        {
            var dt = IzvrsiUpit("SELECT NazivKluba FROM Klub ORDER BY NazivKluba");
            var rezultat = new List<string>();
            foreach (DataRow red in dt.Rows)
                rezultat.Add(red[0].ToString()!);
            return rezultat;
        }
    }
}
