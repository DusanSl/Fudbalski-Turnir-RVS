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
        public DateTime DatumUtakmice { get; set; }
        public string TerenNaziv { get; set; } = string.Empty;
        public string TerenMesto { get; set; } = string.Empty;
        public string TerenAdresa { get; set; } = string.Empty;

        [ForeignKey("Domacin")]
        public int DomacinID { get; set; }
        public Klub Domacin { get; set; } = null!;

        [ForeignKey("Gost")]
        public int GostID { get; set; }
        public Klub Gost { get; set; } = null!;

        public int KonacanRezultatDomacin { get; set; }
        public int KonacanRezultatGost { get; set; }
        public ICollection<StavkaZapisnika> Stavke { get; set; } = new List<StavkaZapisnika>();
    }
}
