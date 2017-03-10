using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.ServiceClients.Queries;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string id);
        Task<Maybe<AvailableResource>> IsNameAvailableAsync(string name);
        Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsers query);
        Task<Maybe<User>> GetByIdAsync(string id);
        Task<Maybe<User>> GetByNameAsync(string name);
        Task UpdateAsync(User user);
        Task AddAsync(User user);
        Task AddManyAsync(IEnumerable<User> users);
    }
}