using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public interface IProductStorageService
    {
        void FillStorage(int cola, int fanta, int colazero);
        IProduct getProductByName(string productname);
    }
}