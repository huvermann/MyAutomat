using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class WarenausgabeModel : BindableBase, IWarenausgabeModel
    {
        #region Constructor

        public WarenausgabeModel()
        {
            _warenausgabeFach = new ObservableCollection<string>();
        }
        #endregion

        #region Programmlogic zur Warenausgabe
        public bool ProduktAusgabe(IProduct product, IMaschinenLog log)
        {
            bool success = false;
            try
            {
                // Hier muss das Produkt freigegeben werden
                log.AddLogEntry(string.Format("Mechanik für Produkt {0} wird geöffnet...", product.ProductName));

                _warenausgabeFach.Add(string.Format("{0} ist im Ausgabefach", product.ProductName));
                success = true;
            }
            catch (Exception)
            {

                log.AddLogEntry("Die Dose klemmt oder kann nicht augegeben werden.");
                success = false;
            }
           

            return success;
        }

        #endregion

        #region properties
        private ObservableCollection<string> _warenausgabeFach;

        public ObservableCollection<string> WarenausgabeFach
        {
            get { return _warenausgabeFach; }
            set { SetProperty(ref _warenausgabeFach, value); }
        }
        #endregion
    }
}
