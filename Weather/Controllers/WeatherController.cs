using Microsoft.AspNetCore.Mvc;

namespace Weather.Controllers
{
    [ApiController]
    [Route("/api/weather")]
    public class WeatherController:ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public WeatherController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            try
            {
                var client = _clientFactory.CreateClient();
                string baseUrl = "https://api.openweathermap.org/data/2.5/weather?q=cairo&units=imperial&appid=895284fb2d2c50a520ea537456963d9c";

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
