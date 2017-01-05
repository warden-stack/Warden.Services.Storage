﻿using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Services.Storage.Services;
using Warden.Services.WardenChecks.Shared.Events;

namespace Warden.Services.Storage.Handlers
{
    public class WardenCheckResultProcessedHandler : IEventHandler<WardenCheckResultProcessed>
    {
        private readonly ICheckResultService _checkResultService;

        public WardenCheckResultProcessedHandler(ICheckResultService checkResultService)
        {
            _checkResultService = checkResultService;
        }

        public async Task HandleAsync(WardenCheckResultProcessed @event)
        {
            await _checkResultService.ValidateAndAddAsync(@event.UserId,
                @event.OrganizationId, @event.WardenId, @event.Result);
        }
    }
}