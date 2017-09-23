using Colaautomat.Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.ModelTests
{
    [TestFixture]
    public class ProductStorageTestClass
    {
        private ProductStorageSimulator _storage;

        [SetUp]
        protected void Setup()
        {
            _storage = new ProductStorageSimulator();
        }
        [Test]
        public void TestCanCreateStorage()
        {
            // TODO: Add your test code here
            Assert.IsNotNull(_storage);
        }

        [Test]
        public void GetProductReturnsColaProductTest()
        {
            ProductStorageSimulator storage = new ProductStorageSimulator();
            var product = storage.getProductByName("cola");
            //Assert.IsInstanceOfType(product, typeof(Product));
            Assert.IsInstanceOf<Product>(product);
        }

        [Test]
        public void FillStorageTest()
        {
            int expectedCola = 10;
            int expectedFanta = 20;
            int expectedColaZero = 30;

            _storage.FillStorage(expectedCola, expectedFanta, expectedColaZero);
            var cola = _storage.getProductByName("cola");
            var fanta = _storage.getProductByName("fanta");
            var colazero = _storage.getProductByName("colazero");
            Assert.AreEqual(cola.Count, expectedCola);
            Assert.AreEqual(fanta.Count, expectedFanta);
            Assert.AreEqual(colazero.Count, expectedColaZero);
        }
    }
}
