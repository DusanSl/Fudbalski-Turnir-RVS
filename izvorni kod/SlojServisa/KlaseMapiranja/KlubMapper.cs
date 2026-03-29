using SlojPodataka.KlasePodataka;
using SlojServisa.DTO;

namespace SlojServisa.KlaseMapiranja
{
    public class KlubMapper
    {
        public KlubDTO UEntitet(Klub klub)
        {
            return new KlubDTO
            {
                KlubID = klub.KlubID,
                NazivKluba = klub.NazivKluba,
                Grad = klub.Grad,
                Stadion = klub.Stadion,
                Adresa = klub.Adresa,
                BrojIgraca = klub.BrojIgraca,
                BrojOsvojenihTitula = klub.BrojOsvojenihTitula,
                GodinaOsnivanja = klub.GodinaOsnivanja
            };
        }

        public Klub UDTO(KlubDTO dto)
        {
            return new Klub
            {
                KlubID = dto.KlubID,
                NazivKluba = dto.NazivKluba,
                Grad = dto.Grad,
                Stadion = dto.Stadion,
                Adresa = dto.Adresa,
                BrojIgraca = dto.BrojIgraca,
                BrojOsvojenihTitula = dto.BrojOsvojenihTitula,
                GodinaOsnivanja = dto.GodinaOsnivanja
            };
        }

        public List<KlubDTO> UListuDTO(List<Klub> klubovi)
        {
            return klubovi.Select(k => UEntitet(k)).ToList();
        }
    }
}