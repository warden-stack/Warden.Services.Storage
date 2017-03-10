using System;
using System.Threading.Tasks;
using Warden.Common.ServiceClients;
using Warden.Common.Types;

namespace Warden.Services.Storage.ServiceClients
{
    public class OrganizationServiceClient : IOrganizationServiceClient
    {
        private readonly IServiceClient _serviceClient;
        private readonly string _name;

        public OrganizationServiceClient(IServiceClient serviceClient, string name)
        {
            _serviceClient = serviceClient;
            _name = name;
        }

        public async Task<Maybe<dynamic>> GetAsync(string userId, Guid organizationId)
            => await GetAsync<dynamic>(userId, organizationId);

        public async Task<Maybe<T>> GetAsync<T>(string userId, Guid organizationId) where T : class
            => await _serviceClient.GetAsync<T>(_name, $"/organizations/{organizationId}?userId={userId}");
    }
}