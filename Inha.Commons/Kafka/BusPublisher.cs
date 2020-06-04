using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;
using Inha.Commons.Messages;

namespace Inha.Commons.Kafka
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IProducer<Null, string> _producer;

        public BusPublisher(ProducerConfig config)
        {
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        #region IBusPublisher Members

        public async Task SendAsync(ICommand command,
                                    string topicName)
        {
            await _producer.ProduceAsync(topicName, new Message<Null, string>
            {
                //Key = "",
                Value = JsonConvert.SerializeObject(command)
            })
                           .ContinueWith(c =>
                                         {
                                         });
            _producer.Flush(TimeSpan.FromSeconds(5));
            //this._producer.Flush(TimeSpan.FromSeconds(5));
            //Console.WriteLine($"KAFKA => Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            await Task.CompletedTask;
        }

        public async Task SendEventAsync<T>(T @event)
                where T : IEvent
        {
            await _producer.ProduceAsync(typeof(T).FullName, new Message<Null, string>
            {
                Value = JsonConvert.SerializeObject(@event)
            })
                           .ContinueWith(c =>
                                         {
                                         });
            _producer.Flush(TimeSpan.FromSeconds(5));
            await Task.CompletedTask;
        }

        #endregion

        #region Dispose

        // Flag: Has Dispose already been called?
        bool disposed;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        /// <summary>
        ///     Distroy
        /// </summary>
        ~BusPublisher()
        {
            Dispose(false);
        }

        #endregion
    }
}
