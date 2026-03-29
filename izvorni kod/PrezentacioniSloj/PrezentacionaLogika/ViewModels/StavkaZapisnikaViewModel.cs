using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.PrezentacionaLogika.ViewModels
{
    public class StavkaZapisnikaViewModel
    {
        public int StavkaID { get; set; }

        [Required(ErrorMessage = "Minut gola je obavezan.")]
        [Range(1, 90, ErrorMessage = "Minut mora biti između 1 i 90.")]
        public int MinutGola { get; set; }

        [Required(ErrorMessage = "Ime strelca je obavezno.")]
        [StringLength(100, ErrorMessage = "Ime strelca ne može biti duže od 100 karaktera.")]
        public string ImeStrelca { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tim je obavezan.")]
        public int TimID { get; set; }
        public string NazivTima { get; set; } = string.Empty;
    }
}