﻿using Newtonsoft.Json;
using Weather.Data;
using Weather.Interfaces;
using Weather.Model;

namespace Weather.Repository
{
    public class AirQualityRepository : IAirQualityRepository
    {
        private readonly HttpClient _httpClient;
        private readonly DataContext _dbContext;

        public AirQualityRepository(HttpClient httpClient, DataContext context)
        {
            _httpClient= httpClient;
            _dbContext= context;
        }
        public async Task CreateAlertAsync(Alert alert)
        {
            _dbContext.Alerts.Add(alert);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CurrentAirQuality> GetAirQualityDataAsync()
        {
            var url = "https://air-quality-api.open-meteo.com/v1/air-quality?latitude=52.52&longitude=13.41&current=pm10,pm2_5,carbon_monoxide,nitrogen_dioxide,sulphur_dioxide,ozone&past_days=3";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    // Log the error or handle it as needed
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var airQualityApiResponse = JsonConvert.DeserializeObject<AirQualityApiResponse>(responseContent);

                return airQualityApiResponse?.Current;
            }
            catch (Exception ex)
            {
                // Log the exception details
                return null;
            }
        }

    }
}
