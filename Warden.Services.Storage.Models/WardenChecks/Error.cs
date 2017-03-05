
namespace Warden.Services.Storage.Models.WardenChecks
{
    public class Error
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTraceString { get; set; }
        public string Level { get; set; }
    }
}