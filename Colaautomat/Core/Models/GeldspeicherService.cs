using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class GeldspeicherService : BindableBase, IGeldspeicherModel
    {
        #region Constructor
        public GeldspeicherService()
        {
            _geldBetrag = 0;
        }
        #endregion

        #region Logic
        public async Task AddCoinAsync(double wert, IMaschinenLog log)
        {
            
            await Task.Delay(2000); // Zeit vertrödeln
            log.AddLogEntry(string.Format("{0}€ wurden in den Geldspeicher gelegt.", wert));
            await Task.Delay(500); // Mehr Zeit vertrödeln
            Geldbetrag += wert;
        }

        public void CollectProductPrice(IProduct product, IMaschinenLog log)
        {
            Geldbetrag -= product.Price;
            log.AddLogEntry(string.Format("Der Geldspeicher enthält nun: {0}€", _geldBetrag));
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
