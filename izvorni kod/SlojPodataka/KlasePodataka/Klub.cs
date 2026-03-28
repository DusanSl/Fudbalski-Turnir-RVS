using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SlojPodataka.KlasePodataka
{
    public class Klub
    {
        [Key]
        public int KlubID { get; set; }
        public string NazivKluba { get; set; } = string.Empty;
        public string Grad { get; set; } = string.Empty;
        public string Stadion { get; set; } = string.Empty;
        public string Adresa { get; set; } = string.Empty;
        public int BrojIgraca { get; set; }
        public int BrojOsvojenihTitula { get; set; }
        public int GodinaOsnivanja { get; set; }
        public ICollection<Zapisnik> DomacinUtakmice { get; set; } = new List<Zapisnik>();
        public ICollection<Zapisnik> GostUtakmice { get; set; } = new List<Zapisnik>();
        public ICollection<StavkaZapisnika> Golovi { get; set; } = new List<StavkaZapisnika>();
    }
}
