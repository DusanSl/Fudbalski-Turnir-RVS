namespace SlojServisa.DTO
{
    public class StavkaDTO
    {
        public int StavkaID { get; set; }
        public int ZapisnikID { get; set; }
        public int MinutGola { get; set; }
        public string ImeStrelca { get; set; } = string.Empty;
        public int KlubID { get; set; }
        public string NazivKluba { get; set; } = string.Empty;
    }
}