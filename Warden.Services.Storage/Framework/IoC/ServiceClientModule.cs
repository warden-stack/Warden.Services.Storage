using System.Linq;
using Autofac;
using Warden.Common.Security;
using Warden.Common.ServiceClients;

namespace Warden.Services.Storage.Framework.IoC
{
    public class ServiceClientModule : Module
    {
        private readonly static string OperationsSettingsKey = "operations-settings";
        private readonly static string OrganizationsSettingsKey = "organizations-settings";
        private readonly static string UsersSettingsKey = "users-settings";

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => x.Resolve<ServicesSettings>()
                    .Single(s => s.Title == "operations"))
                .Named<ServiceSettings>(OperationsSettingsKey)
                .SingleInstance();

            builder.Register(x => x.Resolve<ServicesSettings>()
                    .Single(s => s.Title == "organizations"))
                .Named<ServiceSettings>(OrganizationsSettingsKey)
                .SingleInstance();

            builder.Register(x => x.Resolve<ServicesSettings>()
                    .Single(s => s.Title == "users"))
                .Named<ServiceSettings>(UsersSettingsKey)
                .SingleInstance();

            builder.RegisterType<ServiceClient>()
                .As<IServiceClient>();

            builder.Register(x => new OperationServiceClient(x.Resolve<IServiceClient>(), 
                x.ResolveNamed<ServiceSettings>(OperationsSettingsKey)))
                .As<IOperationServiceClient>()
                .SingleInstance();

            builder.Register(x => new OrganizationServiceClient(x.Resolve<IServiceClient>(), 
                x.ResolveNamed<ServiceSettings>(OrganizationsSettingsKey)))
                .As<IOrganizationServiceClient>()
                .SingleInstance();

            builder.Register(x => new UserServiceClient(x.Resolve<IServiceClient>(), 
                x.ResolveNamed<ServiceSettings>(UsersSettingsKey)))
                .As<IUserServiceClient>()
                .SingleInstance();
        }
    }
}