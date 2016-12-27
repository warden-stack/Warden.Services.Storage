using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Queries;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Modules
{
    public class ApiKeyModule : ModuleBase
    {
        public ApiKeyModule(IApiKeyProvider apiKeyProvider)
        {
            Get("api-keys", async args => await FetchCollection<BrowseApiKeys, ApiKeyDto>
                (async x => await apiKeyProvider.BrowseAsync(x)).HandleAsync());

            Get("users/{userId}/api-keys", async args => await FetchCollection<BrowseApiKeys, ApiKeyDto>
                (async x => await apiKeyProvider.BrowseAsync(x)).HandleAsync());   

            Get("users/{userId}/api-keys/{name}", async args => await Fetch<GetApiKey, ApiKeyDto>
                (async x => await apiKeyProvider.GetAsync(x.UserId, x.Name)).HandleAsync());      
        }
    }
}