using System;
using System.Threading.Tasks;
using Inha.Commons.Messages;
using Inha.Commons.Types;

namespace Inha.Commons.Kafka
{
    public interface IBusSubscriber : IDisposable
    {
        Task<TResponse<TCommand>> SubscribeCommand<TCommand>(string topicName);

        Task<TResponse<T>> SubscribeEvent<T>()
                where T : IEvent;
    }
}
