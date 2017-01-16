using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Exceptions;
using Warden.Common.Handlers;
using Warden.Services.Storage.Repositories;
using Warden.Services.Users.Shared.Events;

namespace Warden.Services.Storage.Handlers
{
    public class UsernameChangedHandler : IEventHandler<UsernameChanged>
    {
        private readonly IHandler _handler;
        private readonly IUserRepository _userRepository;

        public UsernameChangedHandler(IHandler handler, 
            IUserRepository userRepository)
        {
            _handler = handler;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UsernameChanged @event)
        {
            await _handler
                .Run(async () =>
                {
                    var user = await _userRepository.GetByIdAsync(@event.UserId);
                    if (user.HasNoValue)
                    {
                        throw new ServiceException(OperationCodes.UserNotFound,
                            $"User name cannot be changed because user: {@event.UserId} does not exist");
                    }

                    user.Value.Name = @event.NewName;
                    user.Value.State = @event.State;
                    await _userRepository.UpdateAsync(user.Value);
                })
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, $"Error occured while handling {@event.GetType().Name} event");
                })
                .ExecuteAsync();
        }
    }
}