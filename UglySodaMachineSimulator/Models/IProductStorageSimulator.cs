using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public interface IProductStorageSimulator
    {
        

        void FillStorage(int cola, int fanta, int colazero);
        IProduct getProductByName(string productname);
        ObservableCollection<IProduct> ProductCatalog { get; set; }
    }
}