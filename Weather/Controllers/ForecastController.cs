using Microsoft.AspNetCore.Mvc;

namespace Weather.Controllers
{
    [ApiController]
    [Route("/api/forecast")]
    public class ForecastController:ControllerBase
    {

        private readonly IHttpClientFactory _clientFactory;

        public ForecastController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        [Route("/weather")]
        public async Task<IActionResult> GetTempAndHumdidity()
        {
            try
            {
                var client = _clientFactory.CreateClient();
                string baseUrl = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,relative_humidity_2m&past_days=7&forecast_days=1";

                var response = await client.GetAsync(baseUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Failed to fetch data. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("/air")]
        public async Task<IActionResult> GetAirQualityForecast()
        {
            try
            {
                var client = _clientFactory.CreateClient();
                string baseUrl = "https://air-quality-api.open-meteo.com/v1/air-quality?latitude=52.52&longitude=13.41&hourly=pm10,pm2_5,carbon_monoxide,nitrogen_dioxide,sulphur_dioxide,ozone&past_days=7&forecast_days=1";

                var response = await client.GetAsync(baseUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Failed to fetch data. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
