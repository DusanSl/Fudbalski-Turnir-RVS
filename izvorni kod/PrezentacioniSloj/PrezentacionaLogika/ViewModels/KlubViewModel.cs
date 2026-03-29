using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.PrezentacionaLogika.ViewModels
{
    public class KlubViewModel
    {
        public int KlubID { get; set; }

        [Required(ErrorMessage = "Naziv kluba je obavezan.")]
        [StringLength(100, ErrorMessage = "Naziv ne može biti duži od 100 karaktera.")]
        public string NazivKluba { get; set; } = string.Empty;

        [Required(ErrorMessage = "Grad je obavezan.")]
        [StringLength(100, ErrorMessage = "Grad ne može biti duži od 100 karaktera.")]
        public string Grad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stadion je obavezan.")]
        [StringLength(100, ErrorMessage = "Stadion ne može biti duži od 100 karaktera.")]
        public string Stadion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa je obavezna.")]
        [StringLength(200, ErrorMessage = "Adresa ne može biti duža od 200 karaktera.")]
        public string Adresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Broj igrača je obavezan.")]
        [Range(1, 100, ErrorMessage = "Broj igrača mora biti između 1 i 100.")]
        public int BrojIgraca { get; set; }

        [Required(ErrorMessage = "Broj osvojenih titula je obavezan.")]
        [Range(0, 1000, ErrorMessage = "Broj titula mora biti između 0 i 1000.")]
        public int BrojOsvojenihTitula { get; set; }

        [Required(ErrorMessage = "Godina osnivanja je obavezna.")]
        [Range(1800, 2100, ErrorMessage = "Godina osnivanja mora biti između 1800 i 2100.")]
        public int GodinaOsnivanja { get; set; }
    }
}