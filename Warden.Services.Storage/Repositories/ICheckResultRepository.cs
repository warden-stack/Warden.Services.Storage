using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.WardenChecks;

namespace Warden.Services.Storage.Repositories
{
    public interface ICheckResultRepository
    {
        Task<Maybe<PagedResult<CheckResult>>> BrowseAsync(Guid organizationId,
            Guid wardenId, int page = 1, int results = 10);

        Task AddAsync(CheckResult checkResult);
        Task DeleteAsync(CheckResult checkResult);
    }
}