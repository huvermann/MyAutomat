﻿using System.Threading.Tasks;

namespace Colaautomat.Models
{
    public interface IProductStorage
    {
        void FillStorage(int cola, int fanta, int colazero);
        Product getProductByName(string productname);
    }
}