using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Colaautomat.Core.Models;
using Moq;
using System.Threading.Tasks;

namespace ModelTests
{
    /// <summary>
    /// Summary description for OrderServiceTest
    /// </summary>
    [TestClass]
    public class OrderServiceTest
    {
        public OrderServiceTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
           
            var repository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            _geldspeicherMock = repository.Create<IGeldspeicherService>();
            _geldausgabeMock = repository.Create<IGeldausgabeService>();
            _WarenausgabeMock = repository.Create<IWarenausgabeService>();
            _logMock = repository.Create<IMaschinenLog>();
            _orderService = new OrderService(_logMock.Object);
        }

        private TestContext testContextInstance;
        private static OrderService _orderService;
        private static Mock<IGeldspeicherService> _geldspeicherMock;
        private static Mock<IGeldausgabeService> _geldausgabeMock;
        private static Mock<IWarenausgabeService> _WarenausgabeMock;
        private static Mock<IMaschinenLog> _logMock;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class

        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public async Task OrderProductAsyncTest()
        {
            Product product = new Product();
            product.Count = 1;
            product.Price = 1;
            _geldspeicherMock.Setup(g => g.CanBuyProduct(product)).Returns(true);
            _WarenausgabeMock.Setup(w => w.ProduktAusgabe(product)).Returns(true);
            _geldausgabeMock.Setup(ga => ga.GeldRueckgabe(_geldspeicherMock.Object));
            _geldspeicherMock.Setup(g => g.CollectProductPrice(product));

            await _orderService.OrderProductAsync(product, _geldspeicherMock.Object, _geldausgabeMock.Object, _WarenausgabeMock.Object);
        }

        [TestMethod]
        public async Task OrderProductNotInStock()
        {
            Product product = new Product() { Count = 0, Price = 1, ProductName = "Kimonade" };
            string expected = string.Format("Keine {0} mehr da!", product.ProductName);
            product.Count = 0;
            product.Price = 1;
            _geldspeicherMock.Setup(g => g.CanBuyProduct(product)).Returns(true);
            _logMock.Setup(log => log.AddLogEntry(expected));
            await _orderService.OrderProductAsync(product, _geldspeicherMock.Object, _geldausgabeMock.Object, _WarenausgabeMock.Object);

        }

        [TestMethod]
        public async Task OrderProductNotEnoughMoney()
        {
            Product product = new Product() { Count = 1, Price = 1, ProductName = "Kimonade" };
            string expected = "Nicht genug Geld im Automaten um das Produkt zu kaufen.";
            _geldspeicherMock.Setup(g => g.CanBuyProduct(product)).Returns(false);
            _logMock.Setup(log => log.AddLogEntry(expected));
            await _orderService.OrderProductAsync(product, _geldspeicherMock.Object, _geldausgabeMock.Object, _WarenausgabeMock.Object);
        }

        [TestMethod]
        public async Task OrderProductProduktAusgabeReturnsFalse()
        {
            Product product = new Product() { Count = 1, Price = 1, ProductName = "Kimonade" };
            _geldspeicherMock.Setup(g => g.CanBuyProduct(product)).Returns(true);
            _WarenausgabeMock.Setup(w => w.ProduktAusgabe(product)).Returns(false);
            _geldausgabeMock.Setup(ga => ga.GeldRueckgabe(_geldspeicherMock.Object));
            await _orderService.OrderProductAsync(product, _geldspeicherMock.Object, _geldausgabeMock.Object, _WarenausgabeMock.Object);
        }
    }
}
