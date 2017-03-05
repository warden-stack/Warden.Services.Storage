using System;

namespace Warden.Services.Storage.Models.WardenChecks
{
    public class CheckResult
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public DateTime CreatedAt { get; set; }
        public WardenCheckResult Result { get; set; }
    }
}