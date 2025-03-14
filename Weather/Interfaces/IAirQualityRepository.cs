﻿using Weather.Data;
using Weather.Model;

namespace Weather.Interfaces
{
    public interface IAirQualityRepository
    {
        Task<CurrentAirQuality> GetAirQualityDataAsync();
        Task CreateAlertAsync(Alert alert);
    }
}
