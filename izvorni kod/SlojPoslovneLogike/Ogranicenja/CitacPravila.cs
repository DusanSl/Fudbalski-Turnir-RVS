using System.Net.Http.Json;

namespace SlojPoslovneLogike.Ogranicenja
{
    public class CitacPravila
    {
        private readonly HttpClient _httpClient;
        private const string _urlServisa = "http://localhost:5072/api/PravilaRest";

        public CitacPravila(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> DohvatiMinimalniRazmak()
        {
            var pravila = await _httpClient.GetFromJsonAsync<PravilaModel>(_urlServisa);
            return pravila?.MinimalniRazmakMinuta ?? 1;
        }

        public async Task<int> DohvatiMaksimalniMinut()
        {
            var pravila = await _httpClient.GetFromJsonAsync<PravilaModel>(_urlServisa);
            return pravila?.MaksimalniMinutGola ?? 90;
        }

        public async Task<int> DohvatiMinimalniMinut()
        {
            var pravila = await _httpClient.GetFromJsonAsync<PravilaModel>(_urlServisa);
            return pravila?.MinimalniMinutGola ?? 1;
        }
    }
    public class PravilaModel
    {
        public int MinimalniRazmakMinuta { get; set; }
        public int MaksimalniMinutGola { get; set; }
        public int MinimalniMinutGola { get; set; }
    }
}