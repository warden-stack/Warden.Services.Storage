using Warden.Common.Queries;

namespace Warden.Services.Storage.Queries
{
    public class BrowseApiKeys : PagedQueryBase
    {
        public string UserId { get; set; }
    }
}