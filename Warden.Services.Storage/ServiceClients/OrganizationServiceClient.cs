using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Organizations.Shared.Dto;
using Warden.Services.Storage.Settings;

namespace Warden.Services.Storage.ServiceClients
{
    public class OrganizationServiceClient : IOrganizationServiceClient
    {
        private readonly IServiceClient _serviceClient;
        private readonly ProviderSettings _settings;

        public OrganizationServiceClient(IServiceClient serviceClient, ProviderSettings settings)
        {
            _serviceClient = serviceClient;
            _settings = settings;
        }

        public async Task<Maybe<OrganizationDto>> GetAsync(string userId, Guid organizationId)
        {
            return await _serviceClient.GetAsync<OrganizationDto>(_settings.OrganizationsApiUrl, 
                $"/organizations/{organizationId}?userId={userId}");
        }
    }
}