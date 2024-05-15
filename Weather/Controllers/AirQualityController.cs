using Microsoft.AspNetCore.Mvc;

namespace Weather.Controllers
{
    [ApiController]
    [Route("/api/air")]
    public class AirQualityController:ControllerBase
    {
            private readonly IHttpClientFactory _clientFactory;

            public AirQualityController(IHttpClientFactory clientFactory)
            {
                _clientFactory = clientFactory;
            }

            [HttpGet]
            public async Task<IActionResult> GetAirQuality()
            {
                try
                {
                    var client = _clientFactory.CreateClient();
                    string baseUrl = "https://air-quality-api.open-meteo.com/v1/air-quality?latitude=52.52&longitude=13.41&current=pm10,pm2_5,carbon_monoxide,nitrogen_dioxide,sulphur_dioxide,ozone&past_days=3";

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

