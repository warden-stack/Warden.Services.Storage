using System;
using System.Threading.Tasks;
using NLog;
using Warden.Common.Security;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;

namespace Warden.Services.Storage.ServiceClients
{
    public class OperationServiceClient : IOperationServiceClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceClient _serviceClient;
        private readonly ServiceSettings _settings;

        public OperationServiceClient(IServiceClient serviceClient, ServiceSettings settings)
        {
            _serviceClient = serviceClient;
            _settings = settings;
            _serviceClient.SetSettings(settings);
        }

        public async Task<Maybe<OperationDto>> GetAsync(Guid requestId)
        {
            Logger.Debug($"Requesting GetAsync, requestId:{requestId}");
            return await _serviceClient.GetAsync<OperationDto>(_settings.Url, $"/operations/{requestId}");
        }
    }
}