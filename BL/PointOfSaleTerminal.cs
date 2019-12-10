using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace BL
{
    public class PointOfSaleTerminal
    {
        public PointOfSaleTerminal()
        {
            _bag = new List<Tuple<string, int, double>>();
        }
        private Shop Shop { get; set; }
        private List<Tuple<string, int, double>> _bag;

        public void SetPricing(Shop shop) => Shop = shop;

        public double Checkout()
        {
            double total = 0;
            _bag.ToList().ForEach(b => total += b.Item3);
            _bag = new List<Tuple<string, int, double>>();
            return total;
        }

        public void Scan(string productCode)
        {
            var selectedProduct = Shop.Products.Find(p => p.ProductCode == productCode);
            if (selectedProduct != null)
            {
                var bagItem = _bag.ToList().FirstOrDefault(b => b.Item1 == productCode);
                if (bagItem == null)
                    _bag.Add(new Tuple<string, int, double>(productCode, 1, selectedProduct.UnitPrice));
                else
                {
                    var productCount = bagItem.Item2 + 1;
                    var productPrice = selectedProduct.Pack != null ? Convert.ToInt32(productCount / selectedProduct.Pack.Count) * selectedProduct.Pack.PackPrice +
                                       (productCount % selectedProduct.Pack.Count) * selectedProduct.UnitPrice : productCount * selectedProduct.UnitPrice;
                    var newTuple = new Tuple<string, int, double>(bagItem.Item1, productCount, productPrice);
                    _bag.Remove(bagItem);
                    _bag.Add(newTuple);
                }
            }
        }
    }
}
