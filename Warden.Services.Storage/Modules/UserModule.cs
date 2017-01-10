using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Queries;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Modules
{
    public class UserModule : ModuleBase
    {
        public UserModule(IUserProvider userProvider)
        {
            Get("users", async args => await FetchCollection<BrowseUsers, UserDto>
                (async x => await userProvider.BrowseAsync(x)).HandleAsync());

            Get("users/{id}", async args => await Fetch<GetUser, UserDto>
                (async x => await userProvider.GetAsync(x.Id)).HandleAsync());

            Get("users/{name}/account", async args => await Fetch<GetUserByName, UserDto>
                (async x => await userProvider.GetByNameAsync(x.Name)).HandleAsync());

            Get("usernames/{name}/available", async args => await Fetch<GetNameAvailability, AvailableResourceDto>
                (async x => await userProvider.IsAvailableAsync(x.Name)).HandleAsync());
        }
    }
}