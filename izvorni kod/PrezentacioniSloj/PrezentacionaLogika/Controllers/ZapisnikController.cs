using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrezentacioniSloj.PrezentacionaLogika.ViewModels;
using SlojServisa.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PrezentacioniSloj.PrezentacionaLogika.Kontroleri
{
    public class ZapisnikController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ZapisnikController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient KreirajKlijenta() =>
            _httpClientFactory.CreateClient("FudbalskiApi");

        private async Task<List<KlubDTO>> DohvatiKlubove()
        {
            var klijent = KreirajKlijenta();
            var odgovor = await klijent.GetAsync("api/KluboviRest");
            if (!odgovor.IsSuccessStatusCode) return new List<KlubDTO>();
            var json = await odgovor.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<KlubDTO>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<KlubDTO>();
        }

        private List<SelectListItem> KreirajDropdown(List<KlubDTO> klubovi, int? izabraniId = null)
        {
            return klubovi.Select(k => new SelectListItem
            {
                Value = k.KlubID.ToString(),
                Text = k.NazivKluba,
                Selected = k.KlubID == izabraniId
            }).ToList();
        }

        // GET: Zapisnik/Spisak
        public async Task<IActionResult> Spisak(ZapisnikViewModel filter)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            var url = $"api/ZapisnikRest/filter?";

            if (filter.DatumOd.HasValue)
                url += $"datumOd={filter.DatumOd:yyyy-MM-dd}&";
            if (filter.DatumDo.HasValue)
                url += $"datumDo={filter.DatumDo:yyyy-MM-dd}&";
            if (filter.FilterKlubId.HasValue)
                url += $"klubId={filter.FilterKlubId}";

            var odgovor = await klijent.GetAsync(url);
            var json = await odgovor.Content.ReadAsStringAsync();
            var zapisnici = JsonSerializer.Deserialize<List<ZapisnikDTO>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<ZapisnikDTO>();

            var klubovi = await DohvatiKlubove();
            filter.Klubovi = KreirajDropdown(klubovi);

            ViewBag.Zapisnici = zapisnici;
            return View(filter);
        }

        // GET: Zapisnik/Unos
        public async Task<IActionResult> Unos()
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klubovi = await DohvatiKlubove();
            var model = new ZapisnikViewModel
            {
                DatumUtakmice = DateTime.Today,
                Klubovi = KreirajDropdown(klubovi),
                Stavke = new List<StavkaZapisnikaViewModel>()
            };
            return View(model);
        }

        // POST: Zapisnik/Unos
        [HttpPost]
        public async Task<IActionResult> Unos(ZapisnikViewModel model)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            if (!ModelState.IsValid)
            {
                model.Klubovi = KreirajDropdown(await DohvatiKlubove());
                return View(model);
            }

            var dto = new ZapisnikDTO
            {
                DatumUtakmice = model.DatumUtakmice,
                TerenNaziv = model.TerenNaziv,
                TerenMesto = model.TerenMesto,
                TerenAdresa = model.TerenAdresa,
                DomacinID = model.DomacinID,
                GostID = model.GostID,
                Stavke = model.Stavke.Select(s => new StavkaDTO
                {
                    MinutGola = s.MinutGola,
                    ImeStrelca = s.ImeStrelca,
                    KlubID = s.KlubID
                }).ToList()
            };

            var klijent = KreirajKlijenta();
            var json = JsonSerializer.Serialize(dto);
            var sadrzaj = new StringContent(json, Encoding.UTF8, "application/json");
            var odgovor = await klijent.PostAsync("api/ZapisnikRest", sadrzaj);

            if (!odgovor.IsSuccessStatusCode)
            {
                var greska = await odgovor.Content.ReadAsStringAsync();
                ModelState.AddModelError("", greska);
                model.Klubovi = KreirajDropdown(await DohvatiKlubove());
                return View(model);
            }

            return RedirectToAction("Spisak");
        }

        // GET: Zapisnik/Detalji/5
        public async Task<IActionResult> Detalji(int id)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            var odgovor = await klijent.GetAsync($"api/ZapisnikRest/{id}");
            if (!odgovor.IsSuccessStatusCode)
                return NotFound();

            var json = await odgovor.Content.ReadAsStringAsync();
            var zapisnik = JsonSerializer.Deserialize<ZapisnikDTO>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(zapisnik);
        }

        // GET: Zapisnik/Izmena/5
        public async Task<IActionResult> Izmena(int id)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            var odgovor = await klijent.GetAsync($"api/ZapisnikRest/{id}");
            if (!odgovor.IsSuccessStatusCode)
                return NotFound();

            var json = await odgovor.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<ZapisnikDTO>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var klubovi = await DohvatiKlubove();
            var model = new ZapisnikViewModel
            {
                ZapisnikID = dto!.ZapisnikID,
                DatumUtakmice = dto.DatumUtakmice,
                TerenNaziv = dto.TerenNaziv,
                TerenMesto = dto.TerenMesto,
                TerenAdresa = dto.TerenAdresa,
                DomacinID = dto.DomacinID,
                GostID = dto.GostID,
                KonacanRezultatDomacin = dto.KonacanRezultatDomacin,
                KonacanRezultatGost = dto.KonacanRezultatGost,
                Klubovi = KreirajDropdown(klubovi),
                Stavke = dto.Stavke.Select(s => new StavkaZapisnikaViewModel
                {
                    StavkaID = s.StavkaID,
                    MinutGola = s.MinutGola,
                    ImeStrelca = s.ImeStrelca,
                    KlubID = s.KlubID,
                    NazivKluba = s.NazivKluba
                }).ToList()
            };

            return View(model);
        }

        // POST: Zapisnik/Izmena/5
        [HttpPost]
        public async Task<IActionResult> Izmena(int id, ZapisnikViewModel model)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            if (!ModelState.IsValid)
            {
                model.Klubovi = KreirajDropdown(await DohvatiKlubove());
                return View(model);
            }

            var dto = new ZapisnikDTO
            {
                ZapisnikID = id,
                DatumUtakmice = model.DatumUtakmice,
                TerenNaziv = model.TerenNaziv,
                TerenMesto = model.TerenMesto,
                TerenAdresa = model.TerenAdresa,
                DomacinID = model.DomacinID,
                GostID = model.GostID,
                Stavke = model.Stavke.Select(s => new StavkaDTO
                {
                    StavkaID = s.StavkaID,
                    MinutGola = s.MinutGola,
                    ImeStrelca = s.ImeStrelca,
                    KlubID = s.KlubID
                }).ToList()
            };

            var klijent = KreirajKlijenta();
            var json = JsonSerializer.Serialize(dto);
            var sadrzaj = new StringContent(json, Encoding.UTF8, "application/json");
            var odgovor = await klijent.PutAsync($"api/ZapisnikRest/{id}", sadrzaj);

            if (!odgovor.IsSuccessStatusCode)
            {
                var greska = await odgovor.Content.ReadAsStringAsync();
                ModelState.AddModelError("", greska);
                model.Klubovi = KreirajDropdown(await DohvatiKlubove());
                return View(model);
            }

            return RedirectToAction("Spisak");
        }

        // POST: Zapisnik/Obrisi/5
        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            await klijent.DeleteAsync($"api/ZapisnikRest/{id}");
            return RedirectToAction("Spisak");
        }

        // GET: Zapisnik/Stampa
        public async Task<IActionResult> Stampa(int? id, ZapisnikViewModel filter)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();

            if (id.HasValue)
            {
                var odgovor = await klijent.GetAsync($"api/ZapisnikRest/{id}");
                var json = await odgovor.Content.ReadAsStringAsync();
                var zapisnik = JsonSerializer.Deserialize<ZapisnikDTO>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.JedanZapisnik = true;
                return View(new List<ZapisnikDTO> { zapisnik! });
            }
            else
            {
                var url = $"api/ZapisnikRest/filter?";
                if (filter.DatumOd.HasValue)
                    url += $"datumOd={filter.DatumOd:yyyy-MM-dd}&";
                if (filter.DatumDo.HasValue)
                    url += $"datumDo={filter.DatumDo:yyyy-MM-dd}&";
                if (filter.FilterKlubId.HasValue)
                    url += $"klubId={filter.FilterKlubId}";

                var odgovor = await klijent.GetAsync(url);
                var json = await odgovor.Content.ReadAsStringAsync();
                var zapisnici = JsonSerializer.Deserialize<List<ZapisnikDTO>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new List<ZapisnikDTO>();

                ViewBag.JedanZapisnik = false;
                return View(zapisnici);
            }
        }
    }
}