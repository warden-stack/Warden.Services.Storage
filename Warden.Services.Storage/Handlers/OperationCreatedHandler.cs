using System;
using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Services.Operations.Shared.Dto;
using Warden.Services.Operations.Shared.Events;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class OperationCreatedHandler : IEventHandler<OperationCreated>
    {
        private readonly IOperationProvider _operationProvider;
        private readonly IOperationRepository _operationRepository;

        public OperationCreatedHandler(IOperationProvider operationProvider, IOperationRepository operationRepository)
        {
            _operationProvider = operationProvider;
            _operationRepository = operationRepository;
        }

        public async Task HandleAsync(OperationCreated @event)
        {
            var operation = await _operationProvider.GetAsync(@event.RequestId);
            if(operation.HasNoValue)
                return;

            await _operationRepository.AddAsync(new OperationDto
            {
                Id = operation.Value.Id,
                RequestId = @event.RequestId,
                UserId = @event.UserId,
                Origin = @event.Origin,
                Resource = @event.Resource,
                State = @event.State,
                Code = operation.Value.Code,
                Message = operation.Value.Message,
                CreatedAt = @event.CreatedAt,
                UpdatedAt = operation.Value.UpdatedAt
            });
        }
    }
}