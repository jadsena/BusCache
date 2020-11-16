using BusCache.Comandos.Models;
using BusCache.Comum.Models;

namespace BusCache.Comandos.Services
{
    public interface IComandosEntrada
    {
        void Executar(ComandoModel comando, ServiceClient sender);
    }
}
