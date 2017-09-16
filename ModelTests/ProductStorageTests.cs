﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Colaautomat.Core.Models;

namespace ModelTests
{
    [TestClass]
    public class ProductStorageTests
    {
        private ProductStorage _storage;

        [TestInitialize]
        public void TestIinitializer()
        {
            _storage = new ProductStorage();
        }

        [TestMethod]
        public void GetProductReturnsColaProductTest()
        {
            ProductStorage storage = new ProductStorage();
            var product = storage.getProductByName("cola");
            Assert.IsInstanceOfType(product, typeof(Product));
        }
        [TestMethod]
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
