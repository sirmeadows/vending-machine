using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachine.Logic;
using VendingMachine.Models;
using VendingMachine.Models.Enum;
using Xunit;

namespace VendingMachineTests
{
    public class VendingSystemFacts
    {
        private List<Product> products;
        private List<Coin> bankedCoins;

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
                    Price = 0.65m,
                    Stock = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Chips",
                    Price = 0.60m,
                    Stock = 1
                }
            };

            bankedCoins = new List<Coin>
            {
                new Coin(5.000m, 21.21m),
                new Coin(5.000m, 21.21m),
                new Coin(5.000m, 21.21m),
                new Coin(2.268m, 17.91m)
            };
        }

        [Fact]
        public void Initiates()
        {
            var system = new VendingSystem(products, bankedCoins);
            Assert.Equal(products, system.Products);
        }

        [Fact]
        public void InitiatesWithChange()
        {
            var system = new VendingSystem(products, bankedCoins);
            Assert.False(system.NoChange);
        }

        [Fact]
        public void InitiatesWithNoChangeTwoNickelsNoDimes()
        {
            bankedCoins = new List<Coin>
            {
                new Coin(5.000m, 21.21m),
                new Coin(5.000m, 21.21m),
            };

            var system = new VendingSystem(products, bankedCoins);
            Assert.Equal(products, system.Products);
            Assert.Equal("EXACT CHANGE ONLY", system.Display);
            Assert.True(system.NoChange);
        }

        [Fact]
        public void InitiatesWithNoChangeNoNickelsOneDime()
        {
            bankedCoins = new List<Coin>
            {
                new Coin(2.268m, 17.91m)
            };

            var system = new VendingSystem(products, bankedCoins);
            Assert.Equal(products, system.Products);
            Assert.Equal("EXACT CHANGE ONLY", system.Display);
            Assert.True(system.NoChange);
        }

        [Fact]
        public void StartsWithInsertCoinDisplay()
        {
            var system = new VendingSystem(products, bankedCoins);
            Assert.Equal("INSERT COIN", system.Display);
        }

        [Fact]
        public void StartsWithNoCoins()
        {
            var system = new VendingSystem(products, bankedCoins);
            Assert.Empty(system.InsertedCoins);
        }

        [Fact]
        public void StartsWithNoTotalValue()
        {
            var system = new VendingSystem(products, bankedCoins);
            Assert.Equal(0.00m, system.TotalValue);
        }

        [Theory]
        [InlineData(0.05, 0, 1)]
        [InlineData(0.1, 1, 0)]
        [InlineData(0.15, 1, 1)]
        public void DispensesChangeNickelsAndDimes(decimal change, int expectedNumberOfDimes, int expectedNumberOfNickels)
        {
            var system = new VendingSystem(products, bankedCoins);
            system.CalculateChangeInCoins(change);
            var nickels = system.CoinsToDispense.Where(coins => coins.Key == CoinType.Nickel).FirstOrDefault();
            var dimes = system.CoinsToDispense.Where(coins => coins.Key == CoinType.Dime).FirstOrDefault();
            Assert.Equal(expectedNumberOfNickels, nickels.Value);
            Assert.Equal(expectedNumberOfDimes, dimes.Value);
        }

        [Theory]
        [InlineData(0.05, 0, 1)]
        [InlineData(0.1, 0, 2)]
        [InlineData(0.15, 0, 3)]
        public void DispensesChangeNickelsOnly(decimal change, int expectedNumberOfDimes, int expectedNumberOfNickels)
        {
            bankedCoins = new List<Coin>
            {
                new Coin(5.000m, 21.21m),
                new Coin(5.000m, 21.21m),
                new Coin(5.000m, 21.21m)
            };

            var system = new VendingSystem(products, bankedCoins);
            system.CalculateChangeInCoins(change);
            var nickels = system.CoinsToDispense.Where(coins => coins.Key == CoinType.Nickel).FirstOrDefault();
            var dimes = system.CoinsToDispense.Where(coins => coins.Key == CoinType.Dime).FirstOrDefault();
            Assert.Equal(expectedNumberOfNickels, nickels.Value);
            Assert.Equal(expectedNumberOfDimes, dimes.Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void OutOfStock(int productId)
        {
            products.Where(product => product.Id == productId).First().Stock = 0;
            var system = new VendingSystem(products, bankedCoins);

            system.PurchaseProduct(productId);
            Assert.Equal("SOLD OUT", system.Display);
        }


        [Theory]
        [InlineData(1, 0.10, "PRICE $1.00")]
        [InlineData(2, 0.10, "PRICE $0.65")]
        [InlineData(3, 0.10, "PRICE $0.60")]
        public void NotEnoughMoneyToPurchase(int productId, decimal initialMoney, string expectedDisplay)
        {
            var system = new VendingSystem(products, bankedCoins);
            system.TotalValue = initialMoney;

            system.PurchaseProduct(productId);
            Assert.Equal(expectedDisplay, system.Display);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void PurchaseProduct(int productId)
        {
            var system = new VendingSystem(products, bankedCoins);
            system.TotalValue = 1.00m;

            system.PurchaseProduct(productId);
            Assert.Equal("THANK YOU", system.Display);
        }

        [Theory]
        [InlineData(1, 0.5)]
        [InlineData(0.6, 0.9)]
        [InlineData(0.65, 0.85)]
        public void ReturnsChange(decimal purchasePrice, decimal expectedChange)
        {
            var system = new VendingSystem(products, bankedCoins);
            system.TotalValue = 1.5m;

            var change = system.MakeChange(purchasePrice);
            Assert.Equal(expectedChange, change);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(0.6, 0)]
        [InlineData(0.65, 0)]
        public void ReturnsNoChange(decimal purchasePrice, decimal expectedChange)
        {
            bankedCoins = new List<Coin>
            {
                new Coin(5.000m, 21.21m),
                new Coin(5.000m, 21.21m),
            };

            var system = new VendingSystem(products, bankedCoins);
            system.TotalValue = 1.5m;

            var change = system.MakeChange(purchasePrice);
            Assert.Equal(expectedChange, change);
        }

        [Theory]
        [InlineData(5.000, 21.21, CoinType.Nickel, 0.05)]
        [InlineData(2.268, 17.91, CoinType.Dime, 0.10)]
        [InlineData(5.670, 24.26, CoinType.Quarter, 0.25)]
        public void InsertCoinUpdatesCoinListAndValueAndDisplay(decimal coinWeight, decimal coinSize, CoinType expectedCoinType, decimal expectedValue)
        {
            var coin = new Coin(coinWeight, coinSize);

            var system = new VendingSystem(products, bankedCoins);
            system.InsertCoin(coin);

            Assert.True(system.InsertedCoins.Select(coin => coin.CoinType).First() == expectedCoinType);
            Assert.True(system.InsertedCoins.Select(coin => coin.Value).First() == expectedValue);
            Assert.Equal("$" + expectedValue.ToString("0.00"), system.Display);
        }
    }
}
