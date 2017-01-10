using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit.Configuration;
using Warden.Common.Extensions;
using Warden.Common.Mongo;
using Warden.Common.Nancy;
using Warden.Common.Nancy.Serialization;
using Warden.Common.RabbitMq;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.ServiceClients;
using Warden.Services.Storage.Services;
using Warden.Services.Storage.Settings;
using Newtonsoft.Json;
using Warden.Common.Handlers;

namespace Warden.Services.Storage.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;
        public static ILifetimeScope LifetimeScope { get; private set; }

        public Bootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);
            container.Update(builder =>
            {
                builder.RegisterInstance(_configuration.GetSettings<MongoDbSettings>());
                builder.RegisterInstance(_configuration.GetSettings<ProviderSettings>());
                builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>().SingleInstance();
                builder.RegisterModule<MongoDbModule>();
                builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
                builder.RegisterType<ApiKeyRepository>().As<IApiKeyRepository>();
                builder.RegisterType<UserRepository>().As<IUserRepository>();
                builder.RegisterType<UserSessionRepository>().As<IUserSessionRepository>();
                builder.RegisterType<OperationRepository>().As<IOperationRepository>();
                builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
                builder.RegisterType<CheckResultRepository>().As<ICheckResultRepository>();
                builder.RegisterType<WardenService>().As<IWardenService>();
                builder.RegisterType<CustomHttpClient>().As<IHttpClient>();
                builder.RegisterType<ServiceClient>().As<IServiceClient>();
                builder.RegisterType<UserServiceClient>().As<IUserServiceClient>();
                builder.RegisterType<OperationServiceClient>().As<IOperationServiceClient>();
                builder.RegisterType<OrganizationServiceClient>().As<IOrganizationServiceClient>();
                builder.RegisterType<ProviderClient>().As<IProviderClient>();
                builder.RegisterType<ApiKeyProvider>().As<IApiKeyProvider>();
                builder.RegisterType<OperationProvider>().As<IOperationProvider>();
                builder.RegisterType<OrganizationProvider>().As<IOrganizationProvider>();
                builder.RegisterType<UserProvider>().As<IUserProvider>();
                builder.RegisterModule<EventHandlersModule>();
                builder.RegisterType<Handler>().As<IHandler>();
                RabbitMqContainer.Register(builder, _configuration.GetSettings<RawRabbitConfiguration>());
            });
            LifetimeScope = container;
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            var databaseSettings = container.Resolve<MongoDbSettings>();
            var databaseInitializer = container.Resolve<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync();
            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };
            Logger.Info("Warden.Services.Storage API Started");
        }
    }
}