namespace Colaautomat.Core.Models
{
    public interface IProduct
    {
        int Count { get; set; }
        double Price { get; set; }
        string ProductName { get; set; }

        bool IsInStock();
    }
}