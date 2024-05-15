using Weather.Model;

namespace Weather.Interfaces
{
    public interface IAirQualityRepository
    {
        Task<dynamic> GetAirQualityDataAsync();
        Task CreateAlertAsync(Alert alert);
    }
}
