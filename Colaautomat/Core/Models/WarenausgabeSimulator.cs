using Colaautomat.Core.Messages;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Colaautomat.Core.Models
{
    public class WarenausgabeSimulator : BindableBase, IWarenausgabeService, IWarenausgabeSimulator
    {
        #region Constructor
        private IEventAggregator _eventAggregator;
        private IMaschinenLog _logger;

        public WarenausgabeSimulator(IEventAggregator eventaggregator, IMaschinenLog logger)
        {
            _warenausgabeFach = new ObservableCollection<string>();
            _eventAggregator = eventaggregator;
            _logger = logger;
        }
        #endregion

        #region Programmlogic zur Warenausgabe
        public bool ProduktAusgabe(IProduct product)
        {
            bool success = false;
            try
            {
                // Hier muss das Produkt freigegeben werden
                _logger.AddLogEntry("WarenausgabeSimulator", string.Format("Mechanik für Produkt {0} wird geöffnet...", product.ProductName));

                _warenausgabeFach.Add(string.Format("{0} ist im Ausgabefach", product.ProductName));
                _eventAggregator.GetEvent<ProductDeliveredMessage>().Publish(product);
                success = true;
            }
            catch (Exception)
            {

                _logger.AddLogEntry("WarenausgabeSimulator", "Die Dose klemmt oder kann nicht augegeben werden.");
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
