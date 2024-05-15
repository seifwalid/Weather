namespace Weather.Data
{
    public class CurrentAirQuality
    {
        public DateTime Time { get; set; }
        public double Pm10 { get; set; }
        public double Pm2_5 { get; set; }
        public double CarbonMonoxide { get; set; }
        public double NitrogenDioxide { get; set; }
        public double SulphurDioxide { get; set; }
        public double Ozone { get; set; }
    }

    public class AirQualityApiResponse
    {
        public CurrentAirQuality Current { get; set; }
    }

}
