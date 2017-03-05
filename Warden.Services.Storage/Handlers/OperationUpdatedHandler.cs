using System.Threading.Tasks;
using Warden.Messages.Events;
using Warden.Messages.Events.Operations;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class OperationUpdatedHandler : IEventHandler<OperationUpdated>
    {
        private readonly IOperationRepository _operationRepository;

        public OperationUpdatedHandler(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task HandleAsync(OperationUpdated @event)
        {
            var operation = await _operationRepository.GetAsync(@event.RequestId);
            if (operation.HasNoValue)
                return;

            operation.Value.Code = @event.Code;
            operation.Value.Message = @event.Message;
            operation.Value.State = @event.State;
            operation.Value.UpdatedAt = @event.UpdatedAt;
            await _operationRepository.UpdateAsync(operation.Value);
        }
    }
}