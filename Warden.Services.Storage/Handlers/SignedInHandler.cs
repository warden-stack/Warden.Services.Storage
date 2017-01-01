using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Services.Storage.Providers;
using Warden.Services.Users.Shared.Events;

namespace Warden.Services.Storage.Handlers
{
    public class SignedInHandler : IEventHandler<SignedIn>
    {
        private readonly IUserProvider _userProvider;

        public SignedInHandler(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public async Task HandleAsync(SignedIn @event)
        {
            await Task.CompletedTask;
        }
    }
}