﻿using System;
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
            FillWallet();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillWallet()
        {
            for (int i = 0; i < 15; i++)
            {
                //Penny penny = new Penny(); <----Charle's pointed out this is the better way to do this since the Wallet won't always have a specific coin or coins at all
                Coins.Add(penny = new Penny());
            }
            for (int i = 0; i < 12; i++)
            {
                Coins.Add(quarter = new Quarter());
            }
            for (int i = 0; i < 17; i++)
            {
                Coins.Add(nickel = new Nickel());
            }
            for (int i = 0; i < 10; i++)
            {
                Coins.Add(dime = new Dime());
            }
        }
    }
}
