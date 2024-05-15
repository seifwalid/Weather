using Weather.Data;
using Weather.Model;

namespace Weather.Interfaces
{
    public interface IWeatherRepository
    {
        Task<WeatherData> GetWeatherDataAsync();
        Task CreateAlertAsync(Alert alert);
    }

}
