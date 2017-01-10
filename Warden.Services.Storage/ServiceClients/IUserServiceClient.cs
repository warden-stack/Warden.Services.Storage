using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Queries;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.ServiceClients
{
    public interface IUserServiceClient
    {
        Task<Maybe<UserDto>> GetAsync(string userId);
        Task<Maybe<UserDto>> GetByNameAsync(string name);
        Task<Maybe<UserSessionDto>> GetSessionAsync(Guid id);
        Task<Maybe<ApiKeyDto>> GetApiKeyAsync(string userId, string name);
        Task<Maybe<PagedResult<ApiKeyDto>>> BrowseApiKeysAsync(BrowseApiKeys query);
        Task<Maybe<AvailableResourceDto>> IsAvailableAsync(string name); 
    }
}