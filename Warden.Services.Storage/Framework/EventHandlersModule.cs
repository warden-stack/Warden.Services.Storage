using Autofac;
using System.Reflection;
using Warden.Messages.Events;
using Module = Autofac.Module;

namespace Warden.Services.Storage.Framework
{
    public class EventHandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>));
        }
    }
}