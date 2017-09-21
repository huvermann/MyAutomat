using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace Colaautomat.Core.Models
{


    public class ProductStorageSimulator : BindableBase, IProductStorageService, IProductStorageSimulator
    {
        const int anzCola = 3;
        const int anzSprite = 4;
        const int anzColaZero = 1;

        public ProductStorageSimulator()
        {
            FillStorage(anzCola, anzSprite, anzColaZero);
        }
        public void FillStorage(int cola, int fanta, int colazero)
        {
            string resources = @"/UglySodaMachineSimulator;component/Images/";
            // Lager auffüllen ist hier nur zu demozwecken implementier.
            // Der Lagerbestand durch den Bediener oder Sensor eingestellt werden.
            _productCatalog = new ObservableCollection<IProduct>();
            _productCatalog.Add(new Product() { Identifier = "cola", Count = cola, Price = 0.2, ProductName = "River Cola", ImageSource = resources + "cola.jpg"});
            _productCatalog.Add(new Product() { Identifier = "fanta", Count = fanta, Price = 0.6, ProductName = "Fanta", ImageSource = resources +  "fanta.jpg" });
            _productCatalog.Add(new Product() { Identifier = "colazero", Count = colazero, Price = 1, ProductName = "Cola Zero", ImageSource = resources + "colazero.jpg" });
        }

        public IProduct getProductByName(string productname)
        {
            var result = (from item in _productCatalog where item.Identifier == productname select item).FirstOrDefault();
            return result;
        }

        #region Properties
        private ObservableCollection<IProduct> _productCatalog;

        public ObservableCollection<IProduct> ProductCatalog
        {
            get { return _productCatalog; }
            set { SetProperty(ref _productCatalog, value); }
        }
        #endregion

    }
}
