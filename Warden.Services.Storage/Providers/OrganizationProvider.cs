using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Organizations;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.ServiceClients;
using Warden.Services.Storage.ServiceClients.Queries;

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

        public async Task<Maybe<PagedResult<Organization>>> BrowseAsync(BrowseOrganizations query)
            => await _provider.GetAsync(
                async () => await _organizationRepository.BrowseAsync(query),
                async () => await _serviceClient.BrowseAsync<Organization>(query));

        public async Task<Maybe<Organization>> GetAsync(string userId, Guid organizationId)
                => await _provider.GetAsync(
                    async () => await _organizationRepository.GetAsync(userId, organizationId),
                    async () => await _serviceClient.GetAsync<Organization>(userId, organizationId));
    }
}