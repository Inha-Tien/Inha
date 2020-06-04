using System.Threading.Tasks;

namespace Inha.Commons.Utils
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
