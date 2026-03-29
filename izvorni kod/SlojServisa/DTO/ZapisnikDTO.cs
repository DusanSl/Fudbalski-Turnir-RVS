namespace SlojServisa.DTO
{
    public class ZapisnikDTO
    {
        public int ZapisnikID { get; set; }
        public DateTime DatumUtakmice { get; set; }
        public string TerenNaziv { get; set; } = string.Empty;
        public string TerenGrad { get; set; } = string.Empty;
        public string TerenAdresa { get; set; } = string.Empty;
        public int DomacinID { get; set; }
        public string NazivDomacina { get; set; } = string.Empty;
        public int GostID { get; set; }
        public string NazivGosta { get; set; } = string.Empty;
        public int KonacanRezultatDomacin { get; set; }
        public int KonacanRezultatGost { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public List<StavkaDTO> Stavke { get; set; } = new List<StavkaDTO>();
    }
}