using System;
using BL;
using Domain;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace Drawboard
{
    static class Program
    {
        private static void Main()
        {
            var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var shop = new Shop();
            configuration.GetSection("Shop").Bind(shop);
            var terminal = new PointOfSaleTerminal();
            terminal.SetPricing(shop);
            var products = Console.ReadLine();
            products?.ToCharArray().ToList().ForEach(p => terminal.Scan(p.ToString()));
            Console.WriteLine($"Total price is: {terminal.Checkout()}");
            Console.Read();
        }
    }
}
