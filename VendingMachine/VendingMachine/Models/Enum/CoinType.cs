using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Models.Enum
{
    /// <summary>
    /// Identifiers are the value of the coin in cents
    /// </summary>
    public enum CoinType
    {
        Invalid = 0,
        Nickel = 5,
        Dime = 10,
        Quarter = 25
    }
}
