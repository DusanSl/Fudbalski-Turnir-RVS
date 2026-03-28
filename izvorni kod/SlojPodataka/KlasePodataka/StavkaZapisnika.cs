using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka.KlasePodataka
{
    public class StavkaZapisnika : OsnovniEntitet
    {
        [Key]
        public int StavkaID { get; set; }

        [ForeignKey("Zapisnik")]
        public int ZapisnikID { get; set; }
        public Zapisnik Zapisnik { get; set; } = null!;

        public int MinutGola { get; set; }
        public string ImeStrelca { get; set; } = string.Empty;

        [ForeignKey("Tim")]
        public int TimID { get; set; }
        public Klub Tim { get; set; } = null!;
    }
}
