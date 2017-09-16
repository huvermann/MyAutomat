﻿using System.Threading.Tasks;

namespace Colaautomat.Models
{
    public interface IOrderService
    {
        Task OrderProductAsync(Product product, IGeldspeicherModel geldspeicher, IGeldausgabeModel geldausgabe, IWarenausgabeModel warenausgabe, IMaschinenLog log);
    }
}