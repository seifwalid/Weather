using Newtonsoft.Json;
using Weather.Data;
using Weather.Interfaces;
using Weather.Model;

namespace Weather.Repository
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly HttpClient _httpClient;
        private readonly DataContext _dbContext;

        public WeatherRepository(HttpClient httpClient, DataContext context)
        {
            _httpClient = httpClient;
            _dbContext = context;
        }

        public async Task<WeatherData> GetWeatherDataAsync()
        {
            

            var url = "https://api.openweathermap.org/data/2.5/weather?q=cairo&units=imperial&appid=895284fb2d2c50a520ea537456963d9c";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var weatherApiResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return new WeatherData
            {
                Temp = weatherApiResponse.main.temp,
                Humidity = weatherApiResponse.main.humidity
            };
        }

        public async Task CreateAlertAsync(Alert alert)
        {
            _dbContext.Alerts.Add(alert);
            await _dbContext.SaveChangesAsync();
        }
    }

}
