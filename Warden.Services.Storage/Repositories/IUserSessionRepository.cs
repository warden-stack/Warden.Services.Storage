using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Repositories
{
    public interface IUserSessionRepository
    {
        Task<Maybe<UserSession>> GetAsync(Guid id);
        Task AddAsync(UserSession session);
    }
}