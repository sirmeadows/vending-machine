using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Models;

namespace VendingMachine.Logic
{
    public class VendingSystem
    {
        public VendingSystem(List<Product> products, bool noChange = false)
        {
            Products = products;
            NoChange = noChange;
            Display = "INSERT COIN";
        }

        public string Display { get; set; }
        public List<Product> Products { get; set; }
        public List<Coin> Coins { get; set; } = new List<Coin>();
        public decimal TotalValue { get; set; } = 0.00m;
        public bool NoChange { get; set; }

        public void InsertCoin(decimal weight, decimal size)
        {
            var coin = new Coin(weight, size);

            if (coin.CoinType == Models.Enum.CoinType.Invalid)
            {
                return;
            }

            Coins.Add(coin);
            TotalValue += coin.Value;
            Display = TotalValue.ToString();
        }

        public void ReturnCoins()
        {
            foreach (var coin in Coins)
            {
                // return to customer
            }

            Coins = new List<Coin>();
            Display = "INSERT COIN";
        }

        public void PurchaseProduct(int productId)
        {

        }
    }
}
