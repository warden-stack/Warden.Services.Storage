using Warden.Services.Storage.Providers;
using Warden.Services.Storage.ServiceClients.Queries;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Modules
{
    public class ApiKeyModule : ModuleBase
    {
        public ApiKeyModule(IApiKeyProvider apiKeyProvider)
        {
            Get("api-keys", async args => await FetchCollection<BrowseApiKeys, ApiKey>
                (async x => await apiKeyProvider.BrowseAsync(x)).HandleAsync());

            Get("users/{userId}/api-keys", async args => await FetchCollection<BrowseApiKeys, ApiKey>
                (async x => await apiKeyProvider.BrowseAsync(x)).HandleAsync());   

            Get("users/{userId}/api-keys/{name}", async args => await Fetch<GetApiKey, ApiKey>
                (async x => await apiKeyProvider.GetAsync(x.UserId, x.Name)).HandleAsync());      
        }
    }
}