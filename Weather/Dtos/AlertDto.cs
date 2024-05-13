using Weather.Model;

namespace Weather.Dtos
{
    public class AlertDto
    {
        public string Country { get; set; } = String.Empty;
        public Userr User { get; set; }
        public string UserID { get; set; }
        public DateTime Timestamp { get; set; }
        public double ParameterLimit { get; set; }
        public double CurrentParameterVal { get; set; }
        public double Difference { get; set; }
    }
}
