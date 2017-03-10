using System;
using System.Linq;
using Autofac;
using Warden.Common.Security;
using Warden.Common.ServiceClients;
using Warden.Services.Storage.ServiceClients;

namespace Warden.Services.Storage.Framework.IoC
{
    public class ServiceClientsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterService<OperationServiceClient, IOperationServiceClient>(builder, "operations");
            RegisterService<UserServiceClient, IUserServiceClient>(builder, "users");
            RegisterService<OrganizationServiceClient, IOrganizationServiceClient>(builder, "organizations");
        }

        private void RegisterService<TService, TInterface>(ContainerBuilder builder, string title) where TService : TInterface
        {
            builder.Register(x =>
            {
                var name = x.Resolve<ServicesSettings>()
                            .Single(s => s.Title == $"{title}-service")
                            .Name;

                return (TService)Activator.CreateInstance(typeof(TService), 
                                new object[]{x.Resolve<IServiceClient>(), name});
            }) 
            .As<TInterface>()
            .SingleInstance();
        }
    }
}