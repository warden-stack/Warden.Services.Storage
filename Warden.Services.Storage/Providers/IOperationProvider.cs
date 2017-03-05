using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Operations;

namespace Warden.Services.Storage.Providers
{
    public interface IOperationProvider
    {
        Task<Maybe<Operation>> GetAsync(Guid requestId);
    }
}