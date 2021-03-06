﻿using System.Threading.Tasks;
using Warden.Messages.Events;
using Warden.Messages.Events.Organizations;
using Warden.Services.Storage.Services;

namespace Warden.Services.Storage.Handlers
{
    public class WardenDeletedHandler : IEventHandler<WardenDeleted>
    {
        private readonly IWardenService _wardenService;

        public WardenDeletedHandler(IWardenService wardenService)
        {
            _wardenService = wardenService;
        }

        public async Task HandleAsync(WardenDeleted @event)
        {
            await _wardenService.DeleteWardenAsync(@event.WardenId, @event.OrganizationId);
        }
    }
}