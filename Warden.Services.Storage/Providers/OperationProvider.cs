using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.ServiceClients;

namespace Warden.Services.Storage.Providers
{
    public class OperationProvider : IOperationProvider
    {
        private readonly IProviderClient _provider;
        private readonly IOperationRepository _operationRepository;
        private readonly IOperationServiceClient _serviceClient;

        public OperationProvider(IProviderClient provider,
            IOperationRepository operationRepository,
            IOperationServiceClient serviceClient)
        {
            _provider = provider;
            _operationRepository = operationRepository;
            _serviceClient = serviceClient;
        }

        public async Task<Maybe<OperationDto>> GetAsync(Guid requestId)
            => await _provider.GetAsync(
                async () => await _operationRepository.GetAsync(requestId),
                async () => await _serviceClient.GetAsync(requestId));
    }
}