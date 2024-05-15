namespace Weather.Data
{
    // Define the WeatherResponse class
    public class WeatherResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public AirQualityElement Temperature { get; set; }
        public AirQualityElement Humidity { get; set; }
    }

   
}
