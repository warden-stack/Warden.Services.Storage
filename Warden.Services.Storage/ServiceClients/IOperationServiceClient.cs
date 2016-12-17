using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;

namespace Warden.Services.Storage.ServiceClients
{
    public interface IOperationServiceClient
    {
        Task<Maybe<OperationDto>> GetAsync(Guid requestId);
    }
}