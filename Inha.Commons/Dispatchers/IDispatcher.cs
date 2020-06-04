using Inha.Commons.Messages;
using Inha.Commons.Types;
using System.Threading.Tasks;

namespace Inha.Commons.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task SendEventAsync<TEvent>(TEvent @event) where TEvent : IEvent;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
