using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Operations;

namespace Warden.Services.Storage.Repositories
{
    public interface IOperationRepository
    {
        Task<Maybe<Operation>> GetAsync(Guid requestId);
        Task AddAsync(Operation operation);
        Task UpdateAsync(Operation operation);
    }
}