using Warden.Common.Queries;

namespace Warden.Services.Storage.ServiceClients.Queries
{
    public class GetApiKey : PagedQueryBase
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}