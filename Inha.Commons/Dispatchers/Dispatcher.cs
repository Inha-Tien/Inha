using System.Threading.Tasks;
using Inha.Commons.Messages;
using Inha.Commons.Types;

namespace Inha.Commons.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;

        private readonly IQueryDispatcher _queryDispatcher;

        private readonly IEventDispatcher _eventDispatcher;

        public Dispatcher(ICommandDispatcher commandDispatcher,
                          IQueryDispatcher queryDispatcher,
                          IEventDispatcher eventDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _eventDispatcher = eventDispatcher;
        }

        #region IDispatcher Members

        public async Task SendAsync<TCommand>(TCommand command)
                where TCommand : ICommand =>
                await _commandDispatcher.SendAsync(command);

        public async Task SendEventAsync<TEvent>(TEvent @event)
                where TEvent : IEvent =>
                await _eventDispatcher.SendEventAsync(@event);

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query) => await _queryDispatcher.QueryAsync(query);

        #endregion
    }
}
