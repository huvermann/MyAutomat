using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public interface IOrderService
    {
        Task OrderProductAsync(IProduct product, IGeldspeicherService geldspeicher, IGeldausgabeService geldausgabe, IWarenausgabeService warenausgabe);
    }
}