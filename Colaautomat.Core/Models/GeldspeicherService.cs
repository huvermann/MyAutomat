using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class GeldspeicherService : BindableBase, IGeldspeicherService
    {
        #region Constructor
        private IMaschinenLog _logger;

        public GeldspeicherService(IMaschinenLog logger)
        {
            _geldBetrag = 0;
            _logger = logger;
        }
        #endregion

        #region Logic
        public async Task AddCoinAsync(double wert)
        {
            _logger.AddLogEntry("GeldspeicherService", string.Format("{0}€ wurden in den Geldspeicher gelegt.", wert));
            Geldbetrag += wert;
            await Task.Delay(10);
        }

        public void CollectProductPrice(IProduct product)
        {
            Geldbetrag -= product.Price;
            _logger.AddLogEntry("GeldspeicherService", string.Format("Der Geldspeicher enthält nun: {0}€", _geldBetrag));
        }

        public bool CanBuyProduct(IProduct product)
        {
            return (_geldBetrag >= product.Price);
        }
        #endregion

        #region Properties
        private double _geldBetrag;

        public double Geldbetrag
        {
            get { return _geldBetrag; }
            set { SetProperty(ref _geldBetrag, value); }
        }
        #endregion
    }
}
