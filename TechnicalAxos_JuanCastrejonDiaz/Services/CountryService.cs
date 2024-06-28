using Newtonsoft.Json;
using TechnicalAxos_JuanCastrejonDiaz.Interfaces;
using TechnicalAxos_JuanCastrejonDiaz.Models;

namespace TechnicalAxos_JuanCastrejonDiaz.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private List<Country> countries; // Lista para almacenar los países obtenidos
        public static CountryService instance; // Instancia estática del servicio

        // Método estático para obtener la instancia única del servicio
        public static CountryService getInstance()
        {
            if (instance == null)
            {
                instance = new CountryService();
            }
            return instance;
        }

        // Constructor privado para inicializar el HttpClient y la lista de países
        private CountryService()
        {
            _httpClient = new HttpClient();
            countries = new List<Country>();
        }

        // Método asincrónico para obtener la lista de países
        public async Task<List<Country>> GetCountriesAsync()
        {
            if (countries.Count == 0) // Verifica si la lista de países está vacía
            {
                var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                countries = JsonConvert.DeserializeObject<List<Country>>(content); // Deserializa la respuesta JSON en una lista de países
            }

            return countries; // Devuelve la lista de países
        }

        // Método para reiniciar la lista de países
        public void resetCountries()
        {
            countries = new List<Country>(); // Crea una nueva lista vacía
        }

    }
}
