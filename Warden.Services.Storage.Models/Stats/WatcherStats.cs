namespace Warden.Services.Storage.Models.Stats
{
    public class WatcherStats
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double TotalUptime { get; set; }
        public double TotalDowntime { get; set; }
        public double TotalChecks { get; set; }
        public double TotalValidChecks { get; set; }
        public double TotalInvalidChecks { get; set; }
    }
}