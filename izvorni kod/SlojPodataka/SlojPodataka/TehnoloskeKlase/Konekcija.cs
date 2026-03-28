using System;
using System.Collections.Generic;
using System.Text;

namespace SlojPodataka.TehnoloskeKlase
{
    public static class Konekcija
    {
        public static string NizKonekcije { get; set; } =
            "Server=localhost;Database=FudbalskiTurnirDB;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
