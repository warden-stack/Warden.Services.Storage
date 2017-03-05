namespace Warden.Services.Storage.Models.Stats
{
    public class Stats
    {
        public double TotalUptime { get; set; }
        public double TotalDowntime { get; set; }
        public double TotalResults { get; set; }
        public double TotalValidResults { get; set; }
        public double TotalInvalidResults { get; set; }
    }
}