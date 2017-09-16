﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Models
{
    public class OrderService : IOrderService
    {
        public async Task OrderProductAsync(Product product, IGeldspeicherModel geldspeicher, IGeldausgabeModel geldausgabe, IWarenausgabeModel warenausgabe, IMaschinenLog log)
        {
            await Task.Delay(1000);
            // Produkt auf Lager und genug geld im speicher

            if (product.IsInStock() && geldspeicher.CanBuyProduct(product))
            {
                if (warenausgabe.ProduktAusgabe(product, log))
                {
                    await Task.Delay(500);
                    geldspeicher.CollectProductPrice(product, log);

                }
                await Task.Delay(500);
                geldausgabe.GeldRueckgabe(geldspeicher, log);

            }
            else
            {
                await Task.Delay(500);
                log.AddLogEntry(CheckError(product, geldspeicher));
            }

        }

        private string CheckError(Product product, IGeldspeicherModel geldspeicher)
        {
            if (!product.IsInStock())
            {
                return string.Format("Keine {0} mehr da!", product.ProductName);
            }
            if (!geldspeicher.CanBuyProduct(product))
            {
                return "Nicht genug Geld im Automaten um das Produkt zu kaufen.";
            }
            else
            {
                return "Unbekannter Fehler!";
            }
        }
    }
}
