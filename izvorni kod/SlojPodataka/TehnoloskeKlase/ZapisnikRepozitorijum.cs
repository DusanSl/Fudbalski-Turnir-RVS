using System;
using System.Collections.Generic;
using System.Text;
﻿using Microsoft.EntityFrameworkCore;
using SlojPodataka.KlasePodataka;
using SlojPodataka.TehnoloskeKlase;

namespace SlojPodataka.TehnoloskeKlase
{
    public class ZapisnikRepozitorijum
    {
        private readonly TurnirDbContext _kontekst;

        public ZapisnikRepozitorijum(TurnirDbContext kontekst)
        {
            _kontekst = kontekst;
        }

        public List<Zapisnik> DohvatiSve()
        {
            return _kontekst.Zapisnici
                .Include(z => z.Domacin)
                .Include(z => z.Gost)
                .Include(z => z.Stavke)
                    .ThenInclude(s => s.Tim)
                .ToList();
        }

        public Zapisnik? DohvatiPoId(int id)
        {
            return _kontekst.Zapisnici
                .Include(z => z.Domacin)
                .Include(z => z.Gost)
                .Include(z => z.Stavke)
                    .ThenInclude(s => s.Tim)
                .FirstOrDefault(z => z.ZapisnikID == id);
        }

        public void Dodaj(Zapisnik zapisnik)
        {
            using var transakcija = _kontekst.Database.BeginTransaction();
            try
            {
                _kontekst.Zapisnici.Add(zapisnik);
                _kontekst.SaveChanges();
                transakcija.Commit();
            }
            catch
            {
                transakcija.Rollback();
                throw;
            }
        }

        public void Izmeni(Zapisnik zapisnik)
        {
            using var transakcija = _kontekst.Database.BeginTransaction();
            try
            {
                var postojeci = _kontekst.Zapisnici
                    .Include(z => z.Stavke)
                    .FirstOrDefault(z => z.ZapisnikID == zapisnik.ZapisnikID);

                if (postojeci == null) throw new Exception("Zapisnik nije pronađen.");

                postojeci.DatumUtakmice = zapisnik.DatumUtakmice;
                postojeci.TerenNaziv = zapisnik.TerenNaziv;
                postojeci.TerenGrad = zapisnik.TerenGrad;
                postojeci.TerenAdresa = zapisnik.TerenAdresa;
                postojeci.DomacinID = zapisnik.DomacinID;
                postojeci.GostID = zapisnik.GostID;
                postojeci.KonacanRezultatDomacin = zapisnik.KonacanRezultatDomacin;
                postojeci.KonacanRezultatGost = zapisnik.KonacanRezultatGost;

                _kontekst.StavkeZapisnika.RemoveRange(postojeci.Stavke);
                foreach (var stavka in zapisnik.Stavke)
                {
                    stavka.ZapisnikID = postojeci.ZapisnikID;
                    _kontekst.StavkeZapisnika.Add(stavka);
                }

                _kontekst.SaveChanges();
                transakcija.Commit();
            }
            catch
            {
                transakcija.Rollback();
                throw;
            }
        }

        public void Obrisi(int id)
        {
            using var transakcija = _kontekst.Database.BeginTransaction();
            try
            {
                var zapisnik = _kontekst.Zapisnici
                    .Include(z => z.Stavke)
                    .FirstOrDefault(z => z.ZapisnikID == id);

                if (zapisnik == null) throw new Exception("Zapisnik nije pronađen.");

                _kontekst.StavkeZapisnika.RemoveRange(zapisnik.Stavke);
                _kontekst.Zapisnici.Remove(zapisnik);
                _kontekst.SaveChanges();
                transakcija.Commit();
            }
            catch
            {
                transakcija.Rollback();
                throw;
            }
        }

        public List<Zapisnik> Filtriraj(DateTime? datumOd, DateTime? datumDo, int? klubId)
        {
            var upit = _kontekst.Zapisnici
                .Include(z => z.Domacin)
                .Include(z => z.Gost)
                .Include(z => z.Stavke)
                .AsQueryable();

            if (datumOd.HasValue)
                upit = upit.Where(z => z.DatumUtakmice >= datumOd.Value);

            if (datumDo.HasValue)
                upit = upit.Where(z => z.DatumUtakmice <= datumDo.Value);

            if (klubId.HasValue)
                upit = upit.Where(z => z.DomacinID == klubId.Value || z.GostID == klubId.Value);

            return upit.ToList();
        }

        public List<int> DohvatiMinuteGolova(int zapisnikId)
        {
            return _kontekst.StavkeZapisnika
                .Where(s => s.ZapisnikID == zapisnikId)
                .OrderBy(s => s.MinutGola)
                .Select(s => s.MinutGola)
                .ToList();
        }

        public List<Klub> DohvatiKlubove()
        {
            return _kontekst.Klubovi.OrderBy(k => k.NazivKluba).ToList();
        }
    }
}