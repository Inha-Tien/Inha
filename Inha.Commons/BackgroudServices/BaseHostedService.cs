using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Inha.Commons.Dispatchers;
using Inha.Commons.Kafka;
using Inha.Commons.Messages;

namespace Inha.Commons.BackgroudServices
{
    public class BaseHostedService<T> : IHostedService, IDisposable
            where T : IEvent
    {
        private readonly IBusSubscriber _busSubscriber;

        private readonly IDispatcher _dispatcher;

        private readonly CancellationTokenSource _stoppingCts =
                new CancellationTokenSource();

        private Task _executingTask;

        public BaseHostedService(IBusSubscriber busSubscriber,
                                 IDispatcher dispatcher)
        {
            _busSubscriber = busSubscriber;
            _dispatcher = dispatcher;
        }

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHostedService Members

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => ExecuteAsync(cancellationToken));
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                                                              cancellationToken));
            }
        }

        #endregion

        private async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var receiveData = await _busSubscriber.SubscribeEvent<T>();

                if (receiveData.IsSuccess)
                {
                    await _dispatcher.SendEventAsync(receiveData.Data);
                }
            }

            await Task.CompletedTask;
        }
    }
}
