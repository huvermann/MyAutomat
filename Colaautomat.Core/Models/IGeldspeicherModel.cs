﻿using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public interface IGeldspeicherModel
    {
        double Geldbetrag { get; set; }

        Task AddCoinAsync(double wert, IMaschinenLog log);
        void CollectProductPrice(IProduct product, IMaschinenLog log);
        bool CanBuyProduct(IProduct product);
    }
}