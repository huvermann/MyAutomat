using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public interface IOrderService
    {
        Task OrderProductAsync(IProduct product);
        Task ReturnAllMoneyAsync();
        Task CoinInputAsync(double amount);
    }
}