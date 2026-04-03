using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.PrezentacionaLogika.ViewModels
{
    public class RegistracijaViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [StringLength(50, ErrorMessage = "Korisničko ime ne može biti duže od 50 karaktera.")]
        public string KorisnickoIme { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email je obavezan.")]
        [StringLength(100, ErrorMessage = "Email ne može biti duži od 100 karaktera.")]
        [EmailAddress(ErrorMessage = "Email nije ispravan.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Lozinka mora imati najmanje 6 karaktera.")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; } = string.Empty;

        [Required(ErrorMessage = "Potvrda lozinke je obavezna.")]
        [DataType(DataType.Password)]
        [Compare("Lozinka", ErrorMessage = "Lozinke se ne poklapaju.")]
        public string PotvrdaLozinke { get; set; } = string.Empty;
    }
}