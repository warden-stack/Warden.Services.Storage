using Warden.Services.Features.Shared.Events;
using Warden.Services.Organizations.Shared.Events;
using Warden.Services.Users.Shared.Events;
using Warden.Services.WardenChecks.Shared.Events;
using Warden.Common.Host;
using Warden.Services.Operations.Shared.Events;
using Warden.Services.Storage.Framework;

namespace Warden.Services.Storage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 5050)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToEvent<ApiKeyCreated>()
                .SubscribeToEvent<SignedUp>()
                .SubscribeToEvent<SignedIn>()
                .SubscribeToEvent<UsernameChanged>()
                .SubscribeToEvent<UserPaymentPlanCreated>()
                .SubscribeToEvent<OrganizationCreated>()
                .SubscribeToEvent<WardenCheckResultProcessed>()
                .SubscribeToEvent<WardenCreated>()
                .SubscribeToEvent<OperationCreated>()
                .SubscribeToEvent<OperationUpdated>()
                .Build()
                .Run();
        }
    }
}
