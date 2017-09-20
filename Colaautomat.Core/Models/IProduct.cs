using System;

namespace Colaautomat.Core.Models
{
    public interface IProduct
    {
        string Identifier { get; set; }
        int Count { get; set; }
        double Price { get; set; }
        string ProductName { get; set; }
        string ImageSource { get; set; }

        bool IsInStock();
    }
}