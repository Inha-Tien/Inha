using Inha.Commons.Messages;
using System;
using System.Threading.Tasks;

namespace Inha.Commons.Kafka
{
    public interface IBusPublisher : IDisposable
    {
        Task SendAsync(ICommand command, string topicName);

        Task SendEventAsync<T>(T @event)
                where T : IEvent;
    }
}
