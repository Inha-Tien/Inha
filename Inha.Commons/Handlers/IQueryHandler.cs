using Inha.Commons.Types;
using System.Threading.Tasks;

namespace Inha.Commons.Handlers
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
