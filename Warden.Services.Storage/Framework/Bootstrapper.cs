using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit.Configuration;
using Warden.Messages.Commands;
using Warden.Common.Extensions;
using Warden.Messages.Events;
using Warden.Common.Mongo;
using Warden.Common.Nancy;
using Warden.Common.Nancy.Serialization;
using Warden.Common.RabbitMq;
using Warden.Common.Security;
using Warden.Common.ServiceClients;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.Services;
using Newtonsoft.Json;
using Warden.Common.Handlers;
using Warden.Common.Exceptionless;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using System.Reflection;
using Warden.Services.Storage.Framework.IoC;

namespace Warden.Services.Storage.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;
        private static IExceptionHandler _exceptionHandler;
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
                builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>().SingleInstance();
                builder.RegisterModule<MongoDbModule>();
                builder.RegisterModule<ServiceClientModule>();
                builder.RegisterModule<ServiceClientsModule>();
                builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
                builder.RegisterType<ApiKeyRepository>().As<IApiKeyRepository>();
                builder.RegisterType<UserRepository>().As<IUserRepository>();
                builder.RegisterType<UserSessionRepository>().As<IUserSessionRepository>();
                builder.RegisterType<OperationRepository>().As<IOperationRepository>();
                builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
                builder.RegisterType<CheckResultRepository>().As<ICheckResultRepository>();
                builder.RegisterType<WardenService>().As<IWardenService>();
                builder.RegisterType<ProviderClient>().As<IProviderClient>();
                builder.RegisterType<ApiKeyProvider>().As<IApiKeyProvider>();
                builder.RegisterType<OperationProvider>().As<IOperationProvider>();
                builder.RegisterType<OrganizationProvider>().As<IOrganizationProvider>();
                builder.RegisterType<UserProvider>().As<IUserProvider>();
                builder.RegisterModule<EventHandlersModule>();
                builder.RegisterType<Handler>().As<IHandler>();
                builder.RegisterInstance(_configuration.GetSettings<ExceptionlessSettings>()).SingleInstance();
                builder.RegisterType<ExceptionlessExceptionHandler>().As<IExceptionHandler>().SingleInstance();
                
                var assembly = typeof(Startup).GetTypeInfo().Assembly;
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>));
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>));

                SecurityContainer.Register(builder, _configuration);
                RabbitMqContainer.Register(builder, _configuration.GetSettings<RawRabbitConfiguration>());
            });
            LifetimeScope = container;
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                _exceptionHandler.Handle(ex, ctx.ToExceptionData(),
                    "Request details", "Warden", "Service", "Storage");

                return ctx.Response;
            });
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
            _exceptionHandler = container.Resolve<IExceptionHandler>();
            pipelines.SetupTokenAuthentication(container);
            Logger.Info("Warden.Services.Storage API has started.");
        }
    }
}