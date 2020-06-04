using System.Reflection;
using Autofac;
using Inha.Commons.Handlers;
using Inha.Commons.Types;

namespace Inha.Commons.Dispatchers
{
    public static class Extensions
    {
        public static void AddDispatchers(this ContainerBuilder builder)
        {
            builder.RegisterType<CommandDispatcher>()
                   .As<ICommandDispatcher>();
            builder.RegisterType<Dispatcher>()
                   .As<IDispatcher>();
            builder.RegisterType<QueryDispatcher>()
                   .As<IQueryDispatcher>();
            builder.RegisterType<EventDispatcher>()
                   .As<IEventDispatcher>();

            //builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
            //       .AsClosedTypesOf(typeof(IQuery<>))
            //       .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
            //       .AsClosedTypesOf(typeof(IQueryHandler<,>))
            //       .AsImplementedInterfaces();
        }
    }
}
