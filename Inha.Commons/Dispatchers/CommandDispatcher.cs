using Autofac;
using Inha.Commons.Handlers;
using Inha.Commons.Kafka;
using Inha.Commons.Messages;
using System.Threading.Tasks;

namespace Inha.Commons.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
            => await _context.Resolve<ICommandHandler<T>>().HandleAsync(command, CorrelationContext.Empty);
    }
}
