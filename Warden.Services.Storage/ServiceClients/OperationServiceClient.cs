using System;
using System.Threading.Tasks;
using NLog;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;
using Warden.Services.Storage.Settings;

namespace Warden.Services.Storage.ServiceClients
{
    public class OperationServiceClient : IOperationServiceClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceClient _serviceClient;
        private readonly ProviderSettings _settings;

        public OperationServiceClient(IServiceClient serviceClient, ProviderSettings settings)
        {
            _serviceClient = serviceClient;
            _settings = settings;
        }

        public async Task<Maybe<OperationDto>> GetAsync(Guid requestId)
        {
            Logger.Debug($"Requesting GetAsync, requestId:{requestId}");
            return await _serviceClient.GetAsync<OperationDto>(_settings.OperationsApiUrl, $"/operations/{requestId}");
        }
    }
}