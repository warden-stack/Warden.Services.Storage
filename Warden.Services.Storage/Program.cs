using Warden.Messages.Events.Features;
using Warden.Messages.Events.Organizations;
using Warden.Messages.Events.Users;
using Warden.Messages.Events.WardenChecks;
using Warden.Common.Host;
using Warden.Messages.Events.Operations;
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
