using Colaautomat.Core.Models;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace UglySodaMachineSimulator.ViewModels
{
    public class AutomatenViewModel : BindableBase
    {
        private IProductStorageService _productStorage;
        private IOrderService _orderService;
        private IRegionManager _navigationService;
        public AutomatenViewModel(
             IServiceLocator serviceLocator,
            IRegionManager navigationService,
            IAutomatInputManager inputManager,
            IGeldspeicherService geldspeicher,
            IProductStorageService productstorage,
            IMaschinenLog maschinenLog,
            IGeldausgabeSimulator geldausgabe,
            IWarenausgabeSimulator warenausgabe,
            IOrderService orderService)
        {
            _automatInputManager = inputManager;
            _geldspeicher = geldspeicher;
            _productStorage = productstorage;
            _maschinenLog = maschinenLog;
            _geldausgabe = geldausgabe;
            _warenausgabe = warenausgabe;
            _orderService = orderService;
            _navigationService = navigationService;

            _progressMessage = "Bitte warten";

            MuenzCommand = new DelegateCommand<string>(async (m) => await OnMuenzeinwurf(m));
            OrderProductCommand = new DelegateCommand<string>(async (n) => await OnProductSelected(n));
            RueckgabeCommand = new DelegateCommand(async () => OnGeldrueckgabe());
            ClearOutputCommand = new DelegateCommand(async () => await OnClearOutputAsync());
            ServiceCommand = new DelegateCommand(NavigateToService);

        }

        private void NavigateToService()
        {
            _navigationService.RequestNavigate("Shell", "WartungsView");
        }

        //private Task NavigateToServiceView()
        //{
        //    _navigationService.RequestNavigate()
        //}

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

        #region EventHandler
        async Task OnProductSelected(string productName)
        {
            var product = _productStorage.getProductByName(productName);
            await AutomatInputManager.SelectProduct(product);
        }

        async Task OnMuenzeinwurf(string Muenze)
        {
            double wert = Double.Parse(Muenze, CultureInfo.InvariantCulture);
            await AutomatInputManager.CoinInput(wert);
        }

        async Task OnGeldrueckgabe()
        {
            await AutomatInputManager.ReturnMoneyButton();
        }
        #endregion

        #region Properties
        private IGeldspeicherService _geldspeicher;


        public IGeldspeicherService Geldspeicher
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

        private IWarenausgabeSimulator _warenausgabe;

        public IWarenausgabeSimulator Warenausgabe
        {
            get { return _warenausgabe; }
            set { SetProperty(ref _warenausgabe, value); }
        }

        private IGeldausgabeSimulator _geldausgabe;

        public IGeldausgabeSimulator Geldausgabe
        {
            get { return _geldausgabe; }
            set { SetProperty(ref _geldausgabe, value); }
        }

        private string _progressMessage;

        public string ProgressMessage
        {
            get { return _progressMessage; }
            set { SetProperty(ref _progressMessage, value); }
        }

        private IAutomatInputManager _automatInputManager;
        public IAutomatInputManager AutomatInputManager
        {
            get { return _automatInputManager; }
            set { SetProperty(ref _automatInputManager, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand<string> MuenzCommand { get; set; }
        public DelegateCommand<string> OrderProductCommand { get; set; }
        public DelegateCommand RueckgabeCommand { get; set; }
        public DelegateCommand ClearOutputCommand { get; set; }
        public DelegateCommand ServiceCommand { get; set; }
        #endregion
    }
}
