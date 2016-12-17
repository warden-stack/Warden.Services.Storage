using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;

namespace Warden.Services.Storage.Providers
{
    public interface IOperationProvider
    {
        Task<Maybe<OperationDto>> GetAsync(Guid requestId);
    }
}