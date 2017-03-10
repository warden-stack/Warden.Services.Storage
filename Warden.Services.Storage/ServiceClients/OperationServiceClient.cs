using System;
using System.Threading.Tasks;
using NLog;
using Warden.Common.ServiceClients;
using Warden.Common.Types;

namespace Warden.Services.Storage.ServiceClients
{
    public class OperationServiceClient : IOperationServiceClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceClient _serviceClient;
        private readonly string _name;

        public OperationServiceClient(IServiceClient serviceClient, string name)
        {
            _serviceClient = serviceClient;
            _name = name;
        }

        public async Task<Maybe<dynamic>> GetAsync(Guid requestId)
            => await GetAsync<dynamic>(requestId);

        public async Task<Maybe<T>> GetAsync<T>(Guid requestId) where T : class
            => await _serviceClient.GetAsync<T>(_name, $"/operations/{requestId}");
    }
}