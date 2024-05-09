namespace Weather.Model;

public class PermissibleLimits
{   public int Id { get; set; }
    public double MaxTemperature { get; set; }
    public double MaxHumidity { get; set; }
    public double MaxTVOC { get; set; }
    public double MaxCO2 { get; set; }
    public double MaxPM1 { get; set; }
    public double MaxPM2_5 { get; set; }
    public double MaxPM10 { get; set; }
}