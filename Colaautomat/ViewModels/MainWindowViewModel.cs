using Colaautomat.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Colaautomat.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Constructor
        private IProductStorage _productStorage;
        private IOrderService _orderService;

        public MainWindowViewModel(
            IGeldspeicherModel geldspeicher, 
            IProductStorage productstorage, 
            IMaschinenLog maschinenLog, 
            IGeldausgabeModel geldausgabe, 
            IWarenausgabeModel warenausgabe,
            IOrderService orderService)
        {
            _geldspeicher = geldspeicher;
            _productStorage = productstorage;
            _maschinenLog = maschinenLog;
            _geldausgabe = geldausgabe;
            _warenausgabe = warenausgabe;
            _orderService = orderService;
            _uiIsEnabled = true;

            _progressMessage = "Bitte warten";

            MuenzCommand = new DelegateCommand<string>(async (m) => await OnMuenzeinwurf(m));
            OrderProductCommand = new DelegateCommand<string>(async (n) => await OnProductSelected(n));
            RueckgabeCommand = new DelegateCommand(OnGeldrueckgabe);
            ClearOutputCommand = new DelegateCommand(async () => await OnClearOutputAsync());
        }

        async Task OnClearOutputAsync()
        {
            OnClearOutput();
        }

        private void OnClearOutput()
        {
            _geldausgabe.GeldausgabeInfo.Clear();
            _warenausgabe.WarenausgabeFach.Clear();
            _maschinenLog.LogEntries.Clear();
        }
        #endregion

        #region EventHandler
        async Task OnProductSelected(string productName)
        {
            this.MachineIsBuisy = true;
            var produkt = _productStorage.getProductByName(productName);

            await _orderService.OrderProductAsync(
                produkt, 
                _geldspeicher, 
                _geldausgabe, 
                _warenausgabe, 
                _maschinenLog);

            this.MachineIsBuisy = false;
        }

        async Task OnMuenzeinwurf(string Muenze)
        {
            this.MachineIsBuisy = true;
            double wert = Double.Parse(Muenze, CultureInfo.InvariantCulture);
            await _geldspeicher.AddCoinAsync(wert, _maschinenLog);
            this.MachineIsBuisy = false;
        }

        private void OnGeldrueckgabe()
        {
            _geldausgabe.GeldRueckgabe(_geldspeicher, _maschinenLog);
        }
        #endregion

        #region Properties
        private IGeldspeicherModel _geldspeicher;
        

        public IGeldspeicherModel Geldspeicher
        {
            get { return _geldspeicher; }
            set { SetProperty(ref _geldspeicher, value); }
        }

        private IMaschinenLog _maschinenLog;

        public IMaschinenLog MaschinenLog
        {
            get { return _maschinenLog; }
            set { SetProperty(ref _maschinenLog, value); }
        }

        private IWarenausgabeModel _warenausgabe;

        public IWarenausgabeModel Warenausgabe
        {
            get { return _warenausgabe; }
            set { SetProperty(ref _warenausgabe, value); }
        }

        private bool _machineIsBuisy;

        public bool MachineIsBuisy
        {
            get { return _machineIsBuisy; }
            set {
                SetProperty(ref _machineIsBuisy, value);
                UiIsEnabled = !value;
            }
        }

        private IGeldausgabeModel _geldausgabe;

        public IGeldausgabeModel Geldausgabe
        {
            get { return _geldausgabe; }
            set { SetProperty(ref _geldausgabe, value); }
        }

        private bool _uiIsEnabled;

        public bool UiIsEnabled
        {
            get { return _uiIsEnabled; }
            set { SetProperty(ref _uiIsEnabled, value); }
        }

        private string _progressMessage;

        public string ProgressMessage
        {
            get { return _progressMessage; }
            set { SetProperty(ref _progressMessage, value); }
        }


        #endregion

        #region Commands
        public DelegateCommand<string> MuenzCommand { get; set; }
        public DelegateCommand<string> OrderProductCommand { get; set; }
        public DelegateCommand RueckgabeCommand { get; set; }
        public DelegateCommand ClearOutputCommand { get; set; }
        #endregion
    }
}
