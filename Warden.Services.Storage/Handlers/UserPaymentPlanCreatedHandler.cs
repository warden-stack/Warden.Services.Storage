using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Services.Features.Shared.Events;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class UserPaymentPlanCreatedHandler : IEventHandler<UserPaymentPlanCreated>
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserRepository _userRepository;

        public UserPaymentPlanCreatedHandler(IUserProvider userProvider, IUserRepository userRepository)
        {
            _userProvider = userProvider;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UserPaymentPlanCreated @event)
        {
            var user = await _userProvider.GetAsync(@event.UserId);
            if(user.HasNoValue)
                return;
            if(user.Value.PaymentPlanId == @event.PlanId)
                return;

            user.Value.PaymentPlanId = @event.PlanId;
            await _userRepository.UpdateAsync(user.Value);
        }
    }
}