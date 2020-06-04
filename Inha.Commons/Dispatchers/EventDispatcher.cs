using System.Threading.Tasks;
using Autofac;
using Inha.Commons.Handlers;
using Inha.Commons.Kafka;
using Inha.Commons.Messages;

namespace Inha.Commons.Dispatchers
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;

        public EventDispatcher(IComponentContext context)
        {
            _context = context;
        }

        #region IEventDispatcher Members

        public async Task SendEventAsync<T>(T @event)
                where T : IEvent =>
                await _context.Resolve<IEventHandler<T>>()
                              .HandleAsync(@event, CorrelationContext.Empty);

        #endregion
    }
}
