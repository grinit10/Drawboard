using System.Collections.Generic;
using System.Linq;
using BL;
using Domain;
using NUnit.Framework;

namespace UT
{
    public class PointOfSaleTerminalTests
    {
        private Shop _shop;
        private PointOfSaleTerminal _terminal;
        [SetUp]
        public void Setup()
        {
            _terminal = new PointOfSaleTerminal();
            _shop = new Shop
            {
                Products = new List<Product>
                {
                    new Product
                    {
                        ProductCode = "A",
                        UnitPrice = 1.25,
                        Pack = new Pack
                        {
                            Count = 3,
                            PackPrice = 3
                        }
                    },
                    new Product
                    {
                        ProductCode = "B",
                        UnitPrice = 4.25
                    },
                    new Product
                    {
                        ProductCode = "C",
                        UnitPrice = 1,
                        Pack = new Pack
                        {
                            Count = 6,
                            PackPrice = 5
                        }
                    },
                    new Product
                    {
                        ProductCode = "D",
                        UnitPrice = 0.75
                    }
                }
            };

            _terminal.SetPricing(_shop);
        }

        [Test]
        public void GetCorrectPriceWhenSameProductIsScanned()
        {
            "CCCCCCC".ToCharArray().ToList().ForEach(p => _terminal.Scan(p.ToString()));
            Assert.AreEqual(6, _terminal.Checkout());
        }
        
        [Test]
        public void GetCorrectPriceWhenAllDifferentIsScanned()
        {
            "ABCD".ToCharArray().ToList().ForEach(p => _terminal.Scan(p.ToString()));
            Assert.AreEqual(7.25, _terminal.Checkout());
        }
        
        [Test]
        public void GetCorrectPriceWhenMixedItemsAreScanned()
        {
            "ABCDABA".ToCharArray().ToList().ForEach(p => _terminal.Scan(p.ToString()));
            Assert.AreEqual(13.25, _terminal.Checkout());
        }

        [Test]
        public void GetCorrectPriceWhenNotMatchingItemsAreScanned()
        {
            "ABEABA".ToCharArray().ToList().ForEach(p => _terminal.Scan(p.ToString()));
            Assert.AreEqual(11.5, _terminal.Checkout());
        }
    }
}