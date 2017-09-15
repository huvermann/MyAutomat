using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Model
{
    public class GeldspeicherModel : BindableBase, IGeldspeicherModel
    {
        public GeldspeicherModel()
        {
            _geldBetrag = 0;
        }
        public async Task AddCoinAsync(double wert, IMaschinenLog log)
        {
            
            await Task.Delay(2000); // Zeit vertrödeln
            log.AddLogEntry(string.Format("{0} Wurden in den Geldspeicher gelegt.", wert));
            await Task.Delay(500); // Mehr Zeit vertrödeln
            Geldbetrag += wert;
        }

        private double _geldBetrag;

        public double Geldbetrag
        {
            get { return _geldBetrag; }
            set { SetProperty(ref _geldBetrag, value); }
        }

        public void CollectProductPrice(Product product, IMaschinenLog log)
        {
            Geldbetrag -= product.Price;
            log.AddLogEntry(string.Format("Der Geldspeicher enthält nun: {0}", _geldBetrag));
        }

        public bool CanBuyProduct(Product product)
        {
            return (_geldBetrag >= product.Price);
        }
    }
}
