using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SlojPodataka.KlasePodataka
{
    [Table("Korisnik")]
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }
        [Required]
        [StringLength(50)]
        public string KorisnickoIme { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Lozinka { get; set; } = string.Empty;
    }
}
