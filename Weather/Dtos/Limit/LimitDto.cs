namespace Weather.Dtos.Limit
{
    public class LimitDto
    {
        public int Id { get; set; }
        public double MaxTemperature { get; set; }
        public double MaxHumidity { get; set; }
        public double MaxCarbonMonoxide { get; set; }
        public double MaxNitrogenDioxide { get; set; }
        public double MaxPM2_5 { get; set; }
        public double MaxPM10 { get; set; }
        public double MaxSulphurDioxide { get; set; }
        public double MaxOzone { get; set; }
    }
}
