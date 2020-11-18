using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> Coins;
        public Penny penny;
        public Nickel nickel;
        public Dime dime;
        public Quarter quarter;
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            FillRegister();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillRegister()
        {
            for (int i = 0; i < 15; i++)
            {
                Coins.Add(penny = new Penny());
                i++;
            }
            for (int i = 0; i < 12; i++)
            {
                Coins.Add(quarter = new Quarter());
                i++;
            }
            for (int i = 0; i < 17; i++)
            {
                Coins.Add(nickel = new Nickel());
                i++;
            }
            for (int i = 0; i < 10; i++)
            {
                Coins.Add(dime = new Dime());
                i++;
            }
        }
    }
}
