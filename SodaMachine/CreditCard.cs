using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class CreditCard:Coin
    {
        public CreditCard()
        {
            Name = "creditCard";
            value = 100;
        }
        public double Credit(double price)
        {
            double cardLimit = value;
            cardLimit -= price;
            return cardLimit;
        }
    }
}
