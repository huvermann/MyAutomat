using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public interface IGeldspeicherService
    {
        double Geldbetrag { get; set; }

        Task AddCoinAsync(double wert);
        void CollectProductPrice(IProduct product);
        bool CanBuyProduct(IProduct product);
    }
}