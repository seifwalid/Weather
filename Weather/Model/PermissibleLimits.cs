using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.Model;

public class PermissibleLimits
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public double MaxTemperature { get; set; } = 306.15;
    public double MaxHumidity { get; set; } = 44;
    public double MaxCarbonMonoxide { get; set; } = 172;
    public double MaxNitrogenDioxide { get; set; } = 11.9;
    public double MaxPM2_5 { get; set; } = 7.9;
    public double MaxPM10 { get; set; } = 11;
    public double MaxSulphurDioxide { get; set; } = 0.74;
    public double MaxOzone { get; set; } = 87.15;
}