using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Logic;
using VendingMachine.Models;
using Xunit;

namespace VendingMachineTests
{
    public class VendingSystemFacts
    {
        private List<Product> products;

        public VendingSystemFacts()
        {
            products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Cola",
                    Price = 1.00m,
                    Stock = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Candy",
                    Price = 1.00m,
                    Stock = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Chips",
                    Price = 1.00m,
                    Stock = 1
                }
            };
        }

        [Fact]
        public void Initiates()
        {
            var system = new VendingSystem(products);
            Assert.Equal(products, system.Products);
        }

        [Fact]
        public void InitiatesWithNoChange()
        {
            var system = new VendingSystem(products, true);
            Assert.Equal(products, system.Products);
            Assert.True(system.NoChange);
        }

        [Fact]
        public void StartsWithInsertCoinDisplay()
        {
            var system = new VendingSystem(products);
            Assert.Equal("INSERT COIN", system.Display);
        }

        [Fact]
        public void StartsWithNoCoins()
        {
            var system = new VendingSystem(products);
            Assert.Empty(system.Coins);
        }

        [Fact]
        public void StartsWithNoTotalValue()
        {
            var system = new VendingSystem(products);
            Assert.Equal(0.00m, system.TotalValue);
        }
    }
}
