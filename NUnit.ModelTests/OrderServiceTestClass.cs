using Colaautomat.Core.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.ModelTests
{
    [TestFixture]
    public class OrderServiceTestClass
    {
        private static OrderService _orderService;
        private static Mock<IGeldspeicherService> _geldspeicherMock;
        private static Mock<IGeldausgabeService> _geldausgabeMock;
        private static Mock<IWarenausgabeService> _WarenausgabeMock;
        private static Mock<IMaschinenLog> _logMock;

        [SetUp]
        public void Setup()
        {
            var repository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            _geldspeicherMock = repository.Create<IGeldspeicherService>();
            _geldausgabeMock = repository.Create<IGeldausgabeService>();
            _WarenausgabeMock = repository.Create<IWarenausgabeService>();
            _logMock = repository.Create<IMaschinenLog>();
            _orderService = new OrderService(_logMock.Object, _geldspeicherMock.Object, _geldausgabeMock.Object, _WarenausgabeMock.Object);

        }
        [Test]
        public async Task OrderProductNotInStock()
        {
            Product product = new Product() { Count = 0, Price = 1, ProductName = "Kimonade" };
            string expected = string.Format("Keine {0} mehr da!", product.ProductName);
            string expectedModuleName = "OrderService";
            product.Count = 0;
            product.Price = 1;
            _geldspeicherMock.Setup(g => g.CanBuyProduct(product)).Returns(true);
            _logMock.Setup(log => log.AddLogEntry(expectedModuleName, expected));
            await _orderService.OrderProductAsync(product);

        }

        [Test]
        public async Task OrderProductNotEnoughMoney()
        {
            Product product = new Product() { Count = 1, Price = 1, ProductName = "Kimonade" };
            string expected = "Nicht genug Geld im Automaten um das Produkt zu kaufen.";
            string expectedModuleName = "OrderService";
            _geldspeicherMock.Setup(g => g.CanBuyProduct(product)).Returns(false);
            _logMock.Setup(log => log.AddLogEntry(expectedModuleName, expected));
            await _orderService.OrderProductAsync(product);
        }

        [Test]
        public async Task OrderProductProduktAusgabeReturnsFalse()
        {
            Product product = new Product() { Count = 1, Price = 1, ProductName = "Kimonade" };
            _geldspeicherMock.Setup(g => g.CanBuyProduct(product)).Returns(true);
            _WarenausgabeMock.Setup(w => w.ProduktAusgabe(product)).Returns(false);
            _geldausgabeMock.Setup(ga => ga.GeldRueckgabe(_geldspeicherMock.Object)).Returns(Task.FromResult(true));
            await _orderService.OrderProductAsync(product);
        }
    }
}
