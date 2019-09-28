using System.Threading;
using System.Threading.Tasks;

namespace BusCache.Services
{
    public interface ITCPServer
    {
        Task AceitarClientes(CancellationToken cancellationToken);
        void Dispose();
        void StopServer();
    }
}