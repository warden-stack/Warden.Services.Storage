using System;
using System.Threading.Tasks;
using Warden.Messages.Events;
using Warden.Services.Storage.Models.Operations;
using Warden.Messages.Events.Operations;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class OperationCreatedHandler : IEventHandler<OperationCreated>
    {
        private readonly IOperationRepository _operationRepository;

        public OperationCreatedHandler(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task HandleAsync(OperationCreated @event)
        {
            await _operationRepository.AddAsync(new Operation
            {
                Id = Guid.NewGuid(),
                RequestId = @event.RequestId,
                Name = @event.Name,
                UserId = @event.UserId,
                Origin = @event.Origin,
                Resource = @event.Resource,
                State = @event.State,
                CreatedAt = @event.CreatedAt,
            });
        }
    }
}