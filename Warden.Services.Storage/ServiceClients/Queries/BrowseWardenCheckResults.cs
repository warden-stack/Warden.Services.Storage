using System;
using Warden.Common.Queries;

namespace Warden.Services.Storage.ServiceClients.Queries
{
    public class BrowseWardenCheckResults : PagedQueryBase
    {
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
    }
}