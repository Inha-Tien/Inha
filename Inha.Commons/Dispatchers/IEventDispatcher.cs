using Inha.Commons.Messages;
using System.Threading.Tasks;

namespace Inha.Commons.Dispatchers
{
    public interface IEventDispatcher
    {
        Task SendEventAsync<T>(T @event) where T : IEvent;
    }
}
