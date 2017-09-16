using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class GeldausgabeModel : BindableBase, IGeldausgabeModel
    {
        #region Constructor

        public GeldausgabeModel()
        {
            _geldausgabeInfo = new ObservableCollection<string>();
        }
        #endregion

        public void GeldRueckgabe(IGeldspeicherModel geldspeicher, IMaschinenLog log)
        {
            log.AddLogEntry(string.Format("Der Geldspeicher wird geleert."));
            double summe = geldspeicher.Geldbetrag;
            GeldausgabeInfo.Add(string.Format("Pling! {0}€ werden ausgeworfen!", summe));
            geldspeicher.Geldbetrag = 0;
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
