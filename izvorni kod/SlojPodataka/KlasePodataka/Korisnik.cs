using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace SlojPodataka.KlasePodataka
{
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }
        public string KorisnickoIme { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Lozinka { get; set; } = string.Empty;
    }
}
