using SlojPodataka.KlasePodataka;
using SlojServisa.DTO;

namespace SlojServisa.KlaseMapiranja
{
    public class StavkaMapper
    {
        public StavkaDTO UDTO(StavkaZapisnika stavka)
        {
            return new StavkaDTO
            {
                StavkaID = stavka.StavkaID,
                ZapisnikID = stavka.ZapisnikID,
                MinutGola = stavka.MinutGola,
                ImeStrelca = stavka.ImeStrelca,
                KlubID = stavka.KlubID,
                NazivKluba = stavka.Klub?.NazivKluba ?? string.Empty
            };
        }

        public StavkaZapisnika UEntitet(StavkaDTO dto)
        {
            return new StavkaZapisnika
            {
                StavkaID = dto.StavkaID,
                ZapisnikID = dto.ZapisnikID,
                MinutGola = dto.MinutGola,
                ImeStrelca = dto.ImeStrelca,
                KlubID = dto.KlubID
            };
        }
    }
}