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
            
            await Task.Delay(2000); // Zeit vertrödeln
            _logger.AddLogEntry(string.Format("{0}€ wurden in den Geldspeicher gelegt.", wert));
            await Task.Delay(500); // Mehr Zeit vertrödeln
            Geldbetrag += wert;
        }

        public void CollectProductPrice(IProduct product)
        {
            Geldbetrag -= product.Price;
            _logger.AddLogEntry(string.Format("Der Geldspeicher enthält nun: {0}€", _geldBetrag));
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
