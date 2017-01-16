using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Repositories;
using Warden.Services.Users.Shared.Dto;
using Warden.Services.Users.Shared.Events;

namespace Warden.Services.Storage.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserRepository _userRepository;

        public SignedUpHandler(IUserProvider userProvider, IUserRepository userRepository)
        {
            _userProvider = userProvider;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(SignedUp @event)
        {
            var user = await _userProvider.GetAsync(@event.UserId);
            if(user.HasNoValue)
                return;

            await _userRepository.AddAsync(new UserDto
            {
                Id = user.Value.Id,
                UserId = @event.UserId,
                Email = @event.Email,
                Role = @event.Role,
                State = @event.State,
                Name = @event.Name,
                CreatedAt = @event.CreatedAt,
                ExternalUserId = @event.ExternalUserId,
                Provider = user.Value.Provider
            });
        }
    }
}