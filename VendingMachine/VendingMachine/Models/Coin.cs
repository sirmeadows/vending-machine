using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Models.Enum;

namespace VendingMachine.Models
{
    public class Coin
    {
        /// <summary>
        /// Coin takes a weight and a size to determine the CoinType and the Value.
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="size"></param>
        public Coin(decimal weight, decimal size)
        {
            Weight = weight;
            Size = size;

            CalculateCoinTypeAndValue();
        }

        public decimal Weight { get; set; }
        public decimal Size { get; set; }
        public decimal Value { get; set; }
        public CoinType CoinType { get; set; }

        /// <summary>
        /// All weight and diameter measurements are taken from https://www.usmint.gov/learn/coin-and-medal-programs/coin-specifications
        /// Nickels are 5.000 grams and 21.21mm, value is 0.05
        /// Dimes are 2.268 grams and 17.91mm, value is 0.10
        /// Quarters are 5.670 grams and 24.26mm, value is 0.25
        /// Invalid do not match these, value is 0.00
        /// </summary>
        /// <returns>CoinType</returns>
        private void CalculateCoinTypeAndValue()
        {
            if ((Weight > 4.900m && Weight < 5.100m ) && (Size > 21.10m && Size < 21.30m))
            {
                CoinType = CoinType.Nickel;
                Value = (decimal)CoinType / 100;
                return;
            }

            if ((Weight > 2.200m && Weight < 2.300m) && (Size > 17.80m && Size < 18.00m))
            {
                CoinType = CoinType.Dime;
                Value = (decimal)CoinType / 100;
                return;
            }

            if ((Weight > 5.600m && Weight < 5.800m) && (Size > 24.20m && Size < 24.40m))
            {
                CoinType = CoinType.Quarter;
                Value = (decimal)CoinType / 100;
                return;
            }

            CoinType = CoinType.Invalid;
            Value = (decimal)CoinType / 100;
        }
    }
}
