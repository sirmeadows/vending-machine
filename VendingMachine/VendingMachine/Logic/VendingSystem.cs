using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachine.Models;
using VendingMachine.Models.Enum;

namespace VendingMachine.Logic
{
    public class VendingSystem
    {
        public VendingSystem(List<Product> products, List<Coin> bankedCoins)
        {
            Products = products;
            BankedCoins = bankedCoins;
            HasChangeAvailable();
            Display = NoChange ? "EXACT CHANGE ONLY" : "INSERT COIN";
        }

        public string Display { get; set; }
        public List<Product> Products { get; set; }
        public List<Coin> InsertedCoins { get; set; } = new List<Coin>();
        public List<Coin> BankedCoins { get; set; }
        public Dictionary<CoinType, int> CoinsToDispense { get; set; }
        public decimal TotalValue { get; set; } = 0.00m;
        public bool NoChange { get; set; } = false;

        public void CheckDisplay()
        {
            if (TotalValue <= 0.00m)
            {
                Display = NoChange ? "EXACT CHANGE ONLY" : "INSERT COIN";
            }
            else
            {
                Display = $"${TotalValue.ToString("0.00")}";
            }
        }

        public void InsertCoin(Coin coin)
        {
            InsertedCoins.Add(coin);
            TotalValue += coin.Value;
            Display = $"${TotalValue.ToString("0.00")}";
        }

        public void ReturnCoins()
        {
            foreach (var coin in InsertedCoins)
            {
                // TODO: Return to customer
            }

            InsertedCoins = new List<Coin>();
            Display = NoChange ? "EXACT CHANGE ONLY" : "INSERT COIN";
        }

        public void PurchaseProduct(int productId)
        {
            var selectedProduct = Products.Where(product => product.Id == productId).FirstOrDefault();

            if (selectedProduct == null)
                return;

            if (selectedProduct.Stock <= 0)
            {
                Display = "SOLD OUT";
                return;
            }

            if (selectedProduct.Price > TotalValue)
            {
                Display = $"PRICE ${selectedProduct.Price.ToString("0.00")}";
                return;
            }

            var change = MakeChange(selectedProduct.Price);

            UpdateBankedCoins();

            if (change > 0.00m) 
            {
                CalculateChangeInCoins(change);

                // Dispense coins here

                CoinsToDispense = new Dictionary<CoinType, int>();
            }

            Display = "THANK YOU";
        }

        public decimal MakeChange(decimal purchasePrice)
        {
            if (NoChange)
                return 0.00m;

            return TotalValue - purchasePrice;
        }

        void UpdateBankedCoins()
        {
            foreach (var coin in InsertedCoins)
            {
                BankedCoins.Add(coin);
            }

            InsertedCoins = new List<Coin>();
        }

        public void CalculateChangeInCoins(decimal change)
        {
            var coinsToReturn = new Dictionary<CoinType, int>();
            var availableCoins = BankedCoins.GroupBy(coin => coin.CoinType);

            var availableDimes = availableCoins.Where(coins => coins.Key == CoinType.Dime).FirstOrDefault() != null ? availableCoins.Where(coins => coins.Key == CoinType.Dime).FirstOrDefault().Count() : 0;
            var availableNickels = availableCoins.Where(coins => coins.Key == CoinType.Nickel).FirstOrDefault() != null ? availableCoins.Where(coins => coins.Key == CoinType.Nickel).FirstOrDefault().Count() : 0;

            var dimes = Math.Min(Math.Floor(change / 0.10m), availableDimes);
            change -= 0.10m * dimes;

            coinsToReturn.Add(CoinType.Dime, (int)dimes);

            var nickels = Math.Min(Math.Floor(change / 0.05m), availableNickels);

            coinsToReturn.Add(CoinType.Nickel, (int)nickels);

            CoinsToDispense = coinsToReturn;
        }

        /// <summary>
        /// To buy a product, there needs to be 1 Nickel (0.05) and 1 Dime (0.10) OR 3 Nickels
        /// Most that can be expected is $1.00 for Cola, made of 4 Quarters. Customers buying other Products should enter 3 Quarters maximum.
        /// </summary>
        void HasChangeAvailable()
        {
            var nickels = BankedCoins.Where(coin => coin.CoinType == CoinType.Nickel).Count();
            var dimes = BankedCoins.Where(coin => coin.CoinType == CoinType.Dime).Count();

            if (!((dimes > 0 && nickels > 0) || nickels > 2))
            {
                NoChange = true;
            }
        }
    }
}
