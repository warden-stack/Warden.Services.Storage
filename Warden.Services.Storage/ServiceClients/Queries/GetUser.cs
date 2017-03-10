using Warden.Common.Queries;

namespace Warden.Services.Storage.ServiceClients.Queries
{
    public class GetUser : IQuery
    {
        public string Id { get; set; }
    }
}