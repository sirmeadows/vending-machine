using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Logic;
using VendingMachine.Models;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Initial View
            List<Coin> coins = new List<Coin>();
            decimal totalValue = 0.00m;

            Console.WriteLine("INSERT COIN");

            // TODO: Inserting Coins
            coins.Add(new Coin(5.000m, 21.21m));
            totalValue = coins.Sum(coin => coin.Value);

            coins.Add(new Coin(2.268m, 17.91m));
            totalValue = coins.Sum(coin => coin.Value);

            coins.Add(new Coin(5.670m, 24.26m));
            totalValue = coins.Sum(coin => coin.Value);


            // TODO: Check for exact change required

            // TODO: Selecting Product

            // TODO: Sold Out Logic

            // TODO: Calculate Change

            // TODO: Return Change


        }
    }
}
