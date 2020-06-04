using Inha.Commons.Types;
using System.Threading.Tasks;

namespace Inha.Commons.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
