using Warden.Services.Storage.Providers;
using Warden.Services.Storage.ServiceClients.Queries;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Modules
{
    public class UserSessionModule : ModuleBase
    {
        public UserSessionModule(IUserProvider userProvider) : base("user-sessions")
        {
            Get("{id}", async args => await Fetch<GetUserSession, UserSession>
                (async x => await userProvider.GetSessionAsync(x.Id)).HandleAsync());
        }
    }
}