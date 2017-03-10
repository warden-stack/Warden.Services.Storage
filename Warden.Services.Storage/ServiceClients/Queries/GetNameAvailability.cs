using Warden.Common.Queries;

namespace Warden.Services.Storage.ServiceClients.Queries
{
    public class GetNameAvailability : IQuery
    {
        public string Name { get; set; }
    }
}