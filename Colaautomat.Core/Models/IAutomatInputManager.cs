using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public interface IAutomatInputManager
    {
        bool IsOrdering { get; set; }

        Task<bool> CoinInput(double amount);
        Task<bool> ReturnMoneyButton();
        Task<bool> SelectProduct(IProduct product);
        void ShutDown();
    }
}