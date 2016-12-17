using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Repositories
{
    public interface IUserSessionRepository
    {
        Task<Maybe<UserSessionDto>> GetAsync(Guid id);
        Task AddAsync(UserSessionDto session);
    }
}