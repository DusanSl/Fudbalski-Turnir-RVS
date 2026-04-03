using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SlojPodataka.KlasePodataka
{
    [Table("Klub")]
    public class Klub
    {
        [Key]
        public int KlubID { get; set; }
        [Required]
        [StringLength(100)]
        public string NazivKluba { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Grad { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Stadion { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Adresa { get; set; } = string.Empty;
        [Required]
        public int BrojIgraca { get; set; }
        [Required]
        public int BrojOsvojenihTitula { get; set; }
        [Required]
        public int GodinaOsnivanja { get; set; }
        public ICollection<Zapisnik> DomacinUtakmice { get; set; } = new List<Zapisnik>();
        public ICollection<Zapisnik> GostUtakmice { get; set; } = new List<Zapisnik>();
        public ICollection<StavkaZapisnika> Golovi { get; set; } = new List<StavkaZapisnika>();
    }
}
