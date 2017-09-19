using Colaautomat.Core.Utils;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class AutomatInputManager : BindableBase, IAutomatInputManager
    {
        private IOrderService _orderService;
        private IGeldspeicherService _geldspeicher;
        private IGeldausgabeService _geldausgabe;
        private IWarenausgabeService _warenausgabe;
        private IMaschinenLog _logger;

        public AutomatInputManager(IOrderService orderService, IGeldspeicherService geldspeicher, IGeldausgabeService geldausgabe, IWarenausgabeService warenausgabe, IMaschinenLog log)
        {
            _orderService = orderService;
            _geldspeicher = geldspeicher;
            _geldausgabe = geldausgabe;
            _warenausgabe = warenausgabe;
            _logger = log;
        }

        /// <summary>
        /// User Selected a product
        /// </summary>
        /// <param name="product">The product</param>
        public async Task<bool> SelectProduct(IProduct product)
        {
            try
            {
                if (!IsOrdering)
                {

                    IsOrdering = true;
                    await _orderService.OrderProductAsync(product, _geldspeicher, _geldausgabe, _warenausgabe);
                    IsOrdering = false;
                }
            }
            catch (AutomatException e)
            {
                HandleError(e);
                return false;
            }
            return true;            
        }

        public async Task<bool> ReturnMoneyButton()
        {
            try
            {
                if (!IsOrdering)
                {
                    _geldausgabe.GeldRueckgabe(_geldspeicher);
                }
                
            }
            catch (AutomatException e)
            {
                HandleError(e);
            }
            return true;
        }

        public async Task<bool> CoinInput(double amount)
        {
            try
            {
                if (!IsOrdering)
                {
                    IsOrdering = true;
                    await _geldspeicher.AddCoinAsync(amount);
                    IsOrdering = false;
                }

                return true;
            }
            catch (AutomatException e)
            {
                HandleError(e);
                return false;
            }
        }

        private void HandleError(AutomatException exception)
        {
            _logger.AddLogEntry(string.Format("Fehler aufgetreten: {0}", exception.Message));
        }

        private bool _isOrdering;

        public bool IsOrdering
        {
            get { return _isOrdering; }
            set { SetProperty(ref _isOrdering, value); }
        }

    }
}
