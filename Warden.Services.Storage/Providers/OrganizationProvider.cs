using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Organizations.Shared.Dto;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.ServiceClients;

namespace Warden.Services.Storage.Providers
{
    public class OrganizationProvider : IOrganizationProvider
    {
        private readonly IProviderClient _provider;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IOrganizationServiceClient _serviceClient;

        public OrganizationProvider(IProviderClient provider,
            IOrganizationRepository organizationRepository,
            IOrganizationServiceClient serviceClient)
        {
            _provider = provider;
            _organizationRepository = organizationRepository;
            _serviceClient = serviceClient;
        }

        public async Task<Maybe<OrganizationDto>> GetAsync(string userId, Guid organizationId)
            => await _provider.GetAsync(
                async () => await _organizationRepository.GetAsync(userId, organizationId),
                async () => await _serviceClient.GetAsync(userId, organizationId));
    }
}