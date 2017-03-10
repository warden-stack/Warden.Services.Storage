using Warden.Common.Queries;

namespace Warden.Services.Storage.ServiceClients.Queries
{
    public class BrowseApiKeys : PagedQueryBase
    {
        public string UserId { get; set; }
    }
}