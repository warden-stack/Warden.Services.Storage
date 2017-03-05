using System;
using System.Collections.Generic;

namespace Warden.Services.Storage.Models.WardenChecks
{
    public class WardenCheckResult
    {
        public bool IsValid { get; set; }
        public WatcherCheckResult WatcherCheckResult { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public IList<Error> Errors { get; set; }
    }
}