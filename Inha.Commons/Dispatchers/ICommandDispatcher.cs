using Inha.Commons.Messages;
using System.Threading.Tasks;

namespace Inha.Commons.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}
