using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.PrezentacionaLogika.ViewModels
{
    public class ZapisnikViewModel
    {
        public int ZapisnikID { get; set; }

        [Required(ErrorMessage = "Datum utakmice je obavezan.")]
        public DateTime DatumUtakmice { get; set; }

        [Required(ErrorMessage = "Naziv terena je obavezan.")]
        [StringLength(100, ErrorMessage = "Naziv terena ne može biti duži od 100 karaktera.")]
        public string TerenNaziv { get; set; } = string.Empty;

        [Required(ErrorMessage = "Grad terena je obavezan.")]
        [StringLength(100, ErrorMessage = "Naziv grada ne može biti duži od 100 karaktera.")]
        public string TerenGrad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa terena je obavezna.")]
        [StringLength(200, ErrorMessage = "Adresa ne može biti duža od 200 karaktera.")]
        public string TerenAdresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Domaćin je obavezan.")]
        public int DomacinID { get; set; }
        public string NazivDomacina { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gost je obavezan.")]
        public int GostID { get; set; }
        public string NazivGosta { get; set; } = string.Empty;

        public int KonacanRezultatDomacin { get; set; }
        public int KonacanRezultatGost { get; set; }
        public DateTime DatumKreiranja { get; set; }

        
        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
        public int? FilterKlubId { get; set; }
        public List<StavkaZapisnikaViewModel> Stavke { get; set; } = new List<StavkaZapisnikaViewModel>();
        public List<SelectListItem> Klubovi { get; set; } = new List<SelectListItem>();
    }
}