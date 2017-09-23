using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class OrderService : IOrderService
    {
        #region Constructor
        private readonly IMaschinenLog _machineLog;
        private readonly IGeldspeicherService _geldspeicher;
        private readonly IGeldausgabeService _geldausgabe;
        private readonly IWarenausgabeService _warenausgabe;

        public OrderService(IMaschinenLog machineLog, IGeldspeicherService geldspeicher, IGeldausgabeService geldausgabe, IWarenausgabeService warenausgabe)
        {
            _machineLog = machineLog;
            _geldspeicher = geldspeicher;
            _geldausgabe = geldausgabe;
            _warenausgabe = warenausgabe;
        }
        #endregion

        public async Task OrderProductAsync(IProduct product)
        {
            await Task.Delay(1000);
            // Produkt auf Lager und genug geld im speicher

            if (product.IsInStock() && _geldspeicher.CanBuyProduct(product))
            {
                if (_warenausgabe.ProduktAusgabe(product))
                {
                    await Task.Delay(500);
                    _geldspeicher.CollectProductPrice(product);

                }
                await Task.Delay(500);
                await _geldausgabe.GeldRueckgabe(_geldspeicher);

            }
            else
            {
                await Task.Delay(500);
                _machineLog.AddLogEntry("OrderService", CheckError(product));
            }

        }

        private string CheckError(IProduct product)
        {
            if (!product.IsInStock())
            {
                return string.Format("Keine {0} mehr da!", product.ProductName);
            }
            if (!_geldspeicher.CanBuyProduct(product))
            {
                return "Nicht genug Geld im Automaten um das Produkt zu kaufen.";
            }
            else
            {
                return "Unbekannter Fehler!";
            }
        }

        public async Task ReturnAllMoneyAsync()
        {
            await _geldausgabe.GeldRueckgabe(_geldspeicher);
        }

        public async Task CoinInputAsync(double amount)
        {
            await _geldspeicher.AddCoinAsync(amount);
        }
    }
}
