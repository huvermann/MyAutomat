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
        private IMaschinenLog _logger;

        public AutomatInputManager(IOrderService orderService, IMaschinenLog log)
        {
            _orderService = orderService;
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
                    await _orderService.OrderProductAsync(product);
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
                    IsOrdering = true;
                    await _orderService.ReturnAllMoneyAsync();
                    IsOrdering = false;
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
                await _orderService.CoinInputAsync(amount);
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
            _logger.AddLogEntry("InputManager", string.Format("Fehler aufgetreten: {0}", exception.Message));
            IsOrdering = false;
        }

        private bool _isOrdering;

        public bool IsOrdering
        {
            get { return _isOrdering; }
            set { SetProperty(ref _isOrdering, value); }
        }

    }
}
