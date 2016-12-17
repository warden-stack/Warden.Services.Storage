using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string userId);
        Task<Maybe<UserDto>> GetByIdAsync(string userId);
        Task<Maybe<UserDto>> GetByNameAsync(string name);
        Task UpdateAsync(UserDto user);
        Task AddAsync(UserDto user);
    }
}