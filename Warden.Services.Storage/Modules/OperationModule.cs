using Warden.Services.Storage.Models.Operations;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.ServiceClients.Queries;

namespace Warden.Services.Storage.Modules
{
    public class OperationModule : ModuleBase
    {
        public OperationModule(IOperationProvider operationProvider) : base("operations")
        {
            Get("{requestId}", args => Fetch<GetOperation, Operation>
                (async x => await operationProvider.GetAsync(x.RequestId)).HandleAsync());
        }
    }
}