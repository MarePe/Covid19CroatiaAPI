using Covid19CroatiaAPI.Entities;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Services
{
    /// <summary>
    /// A service that fetches the data from the external server.
    /// </summary>
    public class CovidStatsService : ICovidStatsService
    {
        private HttpClient _httpClient;

        public CovidStatsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetInitialAsync()
        {
            var response = await _httpClient.GetAsync("total/dayone/country/croatia");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<DailyCovidOverview[]> GetInitalPocosAsync()
        {
            return await _httpClient.GetFromJsonAsync<DailyCovidOverview[]>("total/dayone/country/croatia");
        }

        public async Task<string> GetStringByDateAsync(DateTime dateUTC)
        {
            IEnumerable<KeyValuePair<string, string>> queryPairList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("to", dateUTC.Date.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("from", dateUTC.AddDays(-1).Date.ToString("yyyy-MM-dd"))
            };

            
            string url = QueryHelpers.AddQueryString(_httpClient.BaseAddress.ToString() + "total/country/croatia", queryPairList);

            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();

        }

        public async Task<DailyCovidOverview[]> GetPocosByDateAsync(DateTime dateUTC)
        {
            string stringResult = await GetStringByDateAsync(dateUTC);

            return JsonSerializer.Deserialize<DailyCovidOverview[]>(stringResult);

        }

    }
}
