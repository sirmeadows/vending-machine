using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Models;
using VendingMachine.Models.Enum;
using Xunit;

namespace VendingMachineTests
{
    public class CoinFacts
    {
        [Fact]
        public void CoinIsANickel()
        {
            var coin = new Coin(5.000m, 21.21m);

            Assert.Equal(CoinType.Nickel, coin.CoinType);
            Assert.Equal(0.05m, coin.Value);

        }

        [Fact]
        public void CoinIsADime()
        {
            var coin = new Coin(2.268m, 17.91m);

            Assert.Equal(CoinType.Dime, coin.CoinType);
            Assert.Equal(0.10m, coin.Value);

        }

        [Fact]
        public void CoinIsAQuarter()
        {
            var coin = new Coin(5.670m, 24.26m);

            Assert.Equal(CoinType.Quarter, coin.CoinType);
            Assert.Equal(0.25m, coin.Value);

        }

        [Fact]
        public void CoinIsANickelFuzzyHigher()
        {
            var coin = new Coin(5.050m, 21.26m);

            Assert.Equal(CoinType.Nickel, coin.CoinType);
            Assert.Equal(0.05m, coin.Value);

        }

        [Fact]
        public void CoinIsADimeFuzzyHigher()
        {
            var coin = new Coin(2.272m, 17.98m);

            Assert.Equal(CoinType.Dime, coin.CoinType);
            Assert.Equal(0.10m, coin.Value);

        }

        [Fact]
        public void CoinIsAQuarterFuzzyHigher()
        {
            var coin = new Coin(5.678m, 24.31m);

            Assert.Equal(CoinType.Quarter, coin.CoinType);
            Assert.Equal(0.25m, coin.Value);

        }

        [Fact]
        public void CoinIsANickelFuzzyLower()
        {
            var coin = new Coin(4.970m, 21.16m);

            Assert.Equal(CoinType.Nickel, coin.CoinType);
            Assert.Equal(0.05m, coin.Value);

        }

        [Fact]
        public void CoinIsADimeFuzzyLower()
        {
            var coin = new Coin(2.262m, 17.85m);

            Assert.Equal(CoinType.Dime, coin.CoinType);
            Assert.Equal(0.10m, coin.Value);

        }

        [Fact]
        public void CoinIsAQuarterFuzzyLower()
        {
            var coin = new Coin(5.662m, 24.21m);

            Assert.Equal(CoinType.Quarter, coin.CoinType);
            Assert.Equal(0.25m, coin.Value);

        }

        [Fact]
        public void CoinIsInvalid()
        {
            var coin = new Coin(3.560m, 20.30m);

            Assert.Equal(CoinType.Invalid, coin.CoinType);
            Assert.Equal(0.00m, coin.Value);
        }
    }
}
