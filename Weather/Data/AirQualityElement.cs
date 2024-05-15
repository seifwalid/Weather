namespace Weather.Data
{
    public class AirQualityElement
    {
        public double Current { get; set; }
        public double Limit { get; set; }
        public double Difference { get; set; }
    }

    public class AirQualityResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public AirQualityElement Ozone { get; set; }
        public AirQualityElement Pm10 { get; set; }
        public AirQualityElement Pm2_5 { get; set; }
        public AirQualityElement CarbonMonoxide { get; set; }
        public AirQualityElement NitrogenDioxide { get; set; }
        public AirQualityElement SulphurDioxide { get; set; }
    }

}
