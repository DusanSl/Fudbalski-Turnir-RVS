using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SlojPodataka.KlasePodataka
{
    [Table("Zapisnik")]
    public class Zapisnik : OsnovniEntitet
    {
        [Key]
        public int ZapisnikID { get; set; }
        [Required]
        public DateTime DatumUtakmice { get; set; }
        [Required]
        [StringLength(100)]
        public string TerenNaziv { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string TerenMesto { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string TerenAdresa { get; set; } = string.Empty;

        [ForeignKey("Domacin")]
        [Required]
        public int DomacinID { get; set; }
        public Klub Domacin { get; set; } = null!;

        [ForeignKey("Gost")]
        [Required]
        public int GostID { get; set; }
        public Klub Gost { get; set; } = null!;

        [Required]
        public int KonacanRezultatDomacin { get; set; }
        [Required]
        public int KonacanRezultatGost { get; set; }
        public ICollection<StavkaZapisnika> Stavke { get; set; } = new List<StavkaZapisnika>();
    }
}
