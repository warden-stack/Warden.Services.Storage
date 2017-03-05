using System.Threading.Tasks;
using Warden.Messages.Events;
using Warden.Services.Storage.Repositories;
using Warden.Messages.Events.WardenChecks;
using Warden.Services.Storage.Models.WardenChecks;

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
            await _checkResultRepository.AddAsync((CheckResult)@event.CheckResult);
        }
    }
}