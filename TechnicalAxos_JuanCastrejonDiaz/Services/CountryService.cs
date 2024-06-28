using Newtonsoft.Json;
using TechnicalAxos_JuanCastrejonDiaz.Interfaces;
using TechnicalAxos_JuanCastrejonDiaz.Models;

namespace TechnicalAxos_JuanCastrejonDiaz.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        public static CountryService instance;
        private List<Country> countries;
        public static CountryService getInstance()
        {
            if (instance == null)
            {
                instance = new CountryService();
            }
            return instance;
        }
        private CountryService()
        {
            _httpClient = new HttpClient();
            countries = new List<Country>();
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            if (countries.Count == 0)
            {
                var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                countries = JsonConvert.DeserializeObject<List<Country>>(content);
            }
            
            return countries;
        }

        public void resetCountries()
        {
            countries = new List<Country>();
        }

    }
}
