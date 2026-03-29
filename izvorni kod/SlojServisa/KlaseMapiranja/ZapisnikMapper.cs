using SlojPodataka.KlasePodataka;
using SlojServisa.DTO;

namespace SlojServisa.KlaseMapiranja
{
    public class ZapisnikMapper
    {
        private readonly StavkaMapper _stavkaMapper;

        public ZapisnikMapper()
        {
            _stavkaMapper = new StavkaMapper();
        }

        public ZapisnikDTO UDTO(Zapisnik zapisnik)
        {
            return new ZapisnikDTO
            {
                ZapisnikID = zapisnik.ZapisnikID,
                DatumUtakmice = zapisnik.DatumUtakmice,
                TerenNaziv = zapisnik.TerenNaziv,
                TerenGrad = zapisnik.TerenGrad,
                TerenAdresa = zapisnik.TerenAdresa,
                DomacinID = zapisnik.DomacinID,
                NazivDomacina = zapisnik.Domacin?.NazivKluba ?? string.Empty,
                GostID = zapisnik.GostID,
                NazivGosta = zapisnik.Gost?.NazivKluba ?? string.Empty,
                KonacanRezultatDomacin = zapisnik.KonacanRezultatDomacin,
                KonacanRezultatGost = zapisnik.KonacanRezultatGost,
                DatumKreiranja = zapisnik.DatumKreiranja,
                Stavke = zapisnik.Stavke?
                    .Select(s => _stavkaMapper.UDTO(s))
                    .ToList() ?? new List<StavkaDTO>()
            };
        }

        public Zapisnik UEntitet(ZapisnikDTO dto)
        {
            return new Zapisnik
            {
                ZapisnikID = dto.ZapisnikID,
                DatumUtakmice = dto.DatumUtakmice,
                TerenNaziv = dto.TerenNaziv,
                TerenGrad = dto.TerenGrad,
                TerenAdresa = dto.TerenAdresa,
                DomacinID = dto.DomacinID,
                GostID = dto.GostID,
                KonacanRezultatDomacin = dto.KonacanRezultatDomacin,
                KonacanRezultatGost = dto.KonacanRezultatGost,
                Stavke = dto.Stavke?
                    .Select(s => _stavkaMapper.UEntitet(s))
                    .ToList() ?? new List<StavkaZapisnika>()
            };
        }

        public List<ZapisnikDTO> UListuDTO(List<Zapisnik> zapisnici)
        {
            return zapisnici.Select(z => UDTO(z)).ToList();
        }
    }
}