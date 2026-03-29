namespace SlojServisa.DTO
{
    public class KlubDTO
    {
        public int KlubID { get; set; }
        public string NazivKluba { get; set; } = string.Empty;
        public string Grad { get; set; } = string.Empty;
        public string Stadion { get; set; } = string.Empty;
        public string Adresa { get; set; } = string.Empty;
        public int BrojIgraca { get; set; }
        public int BrojOsvojenihTitula { get; set; }
        public int GodinaOsnivanja { get; set; }
    }
}