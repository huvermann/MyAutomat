﻿using Colaautomat.Core.Models;
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
        private IProductStorageSimulator _productStorage;
        private IOrderService _orderService;
        private IRegionManager _navigationService;
        public AutomatenViewModel(
            IRegionManager navigationService,
            IAutomatInputManager inputManager,
            IGeldspeicherService geldspeicher,
            IProductStorageSimulator productstorage,
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

            MuenzCommand = new DelegateCommand<string>(OnMuenzeinwurf);
            OrderProductCommand = new DelegateCommand<IProduct>(OnProductSelected);
            RueckgabeCommand = new DelegateCommand(OnGeldrueckgabe);
            ClearOutputCommand = new DelegateCommand(OnClearOutput);
            ServiceCommand = new DelegateCommand(NavigateToService);

        }

        

        private void NavigateToService()
        {
            _navigationService.RequestNavigate("Shell", "WartungsView");
        }


        private void OnClearOutput()
        {
            _geldausgabe.GeldausgabeInfo.Clear();
            _warenausgabe.WarenausgabeFach.Clear();
            _maschinenLog.LogEntries.Clear();
        }

        #region EventHandler
        private void OnProductSelected(IProduct product)
        {
            Func<Task> testFunc = async () =>
            {
                var result = await AutomatInputManager.SelectProduct(product);
            };
            testFunc.Invoke();
        }

        private void OnMuenzeinwurf(string Muenze)
        {
            Func<Task> coinInputFunc = async () =>
            {
                double wert = Double.Parse(Muenze, CultureInfo.InvariantCulture);
                await AutomatInputManager.CoinInput(wert);
            };
            coinInputFunc.Invoke();
        }

        private void OnGeldrueckgabe()
        {
            Func<Task> returnMoneyButtonAsync = async () =>
            {
                await AutomatInputManager.ReturnMoneyButton();
            };
            returnMoneyButtonAsync.Invoke();
            
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

        
        public IProductStorageSimulator Products
        {
            get { return _productStorage; }
            set { SetProperty(ref _productStorage, value); }
        }


        #endregion

        #region Commands
        public DelegateCommand<string> MuenzCommand { get; set; }
        public DelegateCommand<IProduct> OrderProductCommand { get; set; }
        public DelegateCommand RueckgabeCommand { get; set; }
        public DelegateCommand ClearOutputCommand { get; set; }
        public DelegateCommand ServiceCommand { get; set; }
        #endregion
    }
}
