using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Repositories;
using Warden.Services.Users.Shared.Dto;
using Warden.Services.Users.Shared.Events;

namespace Warden.Services.Storage.Handlers
{
    public class ApiKeyCreatedHandler : IEventHandler<ApiKeyCreated>
    {
        private readonly IApiKeyProvider _apiKeyProvider;
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyCreatedHandler(IApiKeyProvider apiKeyProvider, IApiKeyRepository apiKeyRepository)
        {
            _apiKeyProvider = apiKeyProvider;
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task HandleAsync(ApiKeyCreated @event)
        {
            var apiKey = await _apiKeyProvider.GetAsync(@event.UserId, @event.Name);
            if(apiKey.HasNoValue)
                return;

            await _apiKeyRepository.AddAsync(new ApiKeyDto
            {
                Id = apiKey.Value.Id,
                UserId = @event.UserId,
                Key = apiKey.Value.Key,
                Name = apiKey.Value.Name
            });
        }
    }
}