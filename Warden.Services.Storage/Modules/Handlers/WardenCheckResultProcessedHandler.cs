using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Services.Storage.Repositories;
using Warden.Services.WardenChecks.Shared.Events;

namespace Warden.Services.Storage.Handlers
{
    public class WardenCheckResultProcessedHandler : IEventHandler<WardenCheckResultProcessed>
    {
        private readonly ICheckResultRepository _checkResultRepository;

        public WardenCheckResultProcessedHandler(ICheckResultRepository checkResultRepository)
        {
            _checkResultRepository = checkResultRepository;
        }

        public async Task HandleAsync(WardenCheckResultProcessed @event)
        {
            await _checkResultRepository.AddAsync(@event.CheckResult);
        }
    }
}