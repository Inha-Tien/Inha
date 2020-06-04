using Inha.Commons.Kafka;
using Inha.Commons.Messages;
using System.Threading.Tasks;

namespace Inha.Commons.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}
