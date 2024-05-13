namespace Weather.Model
{
    public class Alert
    {
        public int Id { get; set; }
        public string Country { get; set ; }=String.Empty;
        public Userr User { get; set; }
        public int UserID { get; set; }
        public DateTime Timestamp { get; set; }
        public double ParameterLimit { get; set; }
        public double CurrentParameterVal { get; set; }
        public double Difference { get; set; }

       





    }
}
