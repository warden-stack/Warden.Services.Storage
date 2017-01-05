using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.WardenChecks.Shared.Dto;

namespace Warden.Services.Storage.Repositories
{
    public interface ICheckResultRepository
    {
        Task<Maybe<PagedResult<CheckResultDto>>> BrowseAsync(Guid organizationId,
            Guid wardenId, int page = 1, int results = 10);

        Task AddAsync(CheckResultDto checkResult);
        Task DeleteAsync(CheckResultDto checkResult);
    }
}