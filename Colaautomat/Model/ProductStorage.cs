using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Model
{
    public class Product
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsInStock()
        {
            return (Count > 0);
        }
    }

    public class ProductStorage : IProductStorage
    {
        const int anzCola = 3;
        const int anzSprite = 4;
        const int anzColaZero = 2;

        private Dictionary<string, Product> storage;

        public ProductStorage()
        {
            FillStorage(anzCola, anzSprite, anzColaZero);
        }
        public void FillStorage(int cola, int fanta, int colazero)
        {
            // Lager auffüllen ist hier nur zu demozwecken implementier.
            // Der Lagerbestand durch den Bediener oder Sensor eingestellt werden.
            storage = new Dictionary<string, Product>();
            storage.Add("cola", new Product() { ProductName = "River Cola", Price = 0.2, Count = cola });
            storage.Add("fanta", new Product() { ProductName = "Fanta", Price = 0.6, Count = fanta });
            storage.Add("colazero", new Product() { ProductName = "Cola Zero", Price = 1, Count = colazero });
        }

        public Product getProductByName(string productname)
        {
            return storage[productname];
        }
    }
}
