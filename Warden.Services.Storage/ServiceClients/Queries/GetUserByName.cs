using Warden.Common.Queries;

namespace Warden.Services.Storage.ServiceClients.Queries
{
    public class GetUserByName : IQuery
    {
        public string Name { get; set; }
    }
}