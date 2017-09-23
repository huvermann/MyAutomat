using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class GeldausgabeHWSimulator : BindableBase, IGeldausgabeService, IGeldausgabeSimulator
    {
        #region Constructor
        private IMaschinenLog _logger;

        public GeldausgabeHWSimulator(IMaschinenLog logger)
        {
            _geldausgabeInfo = new ObservableCollection<string>();
            _logger = logger;
        }
        #endregion

        public async Task<bool> GeldRueckgabe(IGeldspeicherService geldspeicher)
        {
            _logger.AddLogEntry("GeldausgabeHWSimulator", string.Format("Der Geldspeicher wird geleert."));
            double summe = geldspeicher.Geldbetrag;
            geldspeicher.Geldbetrag = 0;
            GeldausgabeInfo.Add(string.Format("Pling! {0}€ werden ausgeworfen!", summe));
            await Task.Delay(10);
            return true;
        }

        #region Bildschirmausgabe
        private ObservableCollection<string> _geldausgabeInfo;

        public ObservableCollection<string> GeldausgabeInfo
        {
            get { return _geldausgabeInfo; }
            set { SetProperty(ref _geldausgabeInfo, value); }
        }
        #endregion
    }
}
