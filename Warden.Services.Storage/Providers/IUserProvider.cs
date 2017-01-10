using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Queries;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Providers
{
    public interface IUserProvider
    {
        Task<Maybe<AvailableResourceDto>> IsAvailableAsync(string name);
        Task<Maybe<PagedResult<UserDto>>> BrowseAsync(BrowseUsers query);
        Task<Maybe<UserDto>> GetAsync(string userId);
        Task<Maybe<UserDto>> GetByNameAsync(string name);
        Task<Maybe<UserSessionDto>> GetSessionAsync(Guid id);
    }
}