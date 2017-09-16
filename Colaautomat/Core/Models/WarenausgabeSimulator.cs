using Colaautomat.Core.Messages;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class WarenausgabeSimulator : BindableBase, IWarenausgabeModel
    {
        #region Constructor
        private IEventAggregator _eventAggregator;

        public WarenausgabeSimulator(IEventAggregator eventaggregator)
        {
            _warenausgabeFach = new ObservableCollection<string>();
            _eventAggregator = eventaggregator;
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
                _eventAggregator.GetEvent<ProductDeliveredMessage>().Publish(product);
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
