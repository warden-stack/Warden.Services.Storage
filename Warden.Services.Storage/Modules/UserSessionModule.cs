using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Queries;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Modules
{
    public class UserSessionModule : ModuleBase
    {
        public UserSessionModule(IUserProvider userProvider) : base("user-sessions")
        {
            Get("{id}", async args => await Fetch<GetUserSession, UserSessionDto>
                (async x => await userProvider.GetSessionAsync(x.Id)).HandleAsync());
        }
    }
}