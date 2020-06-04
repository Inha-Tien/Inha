using Inha.Commons.Kafka;
using Inha.Commons.Messages;
using System.Threading.Tasks;

namespace Inha.Commons.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}
