﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulation simulation = new Simulation();
            simulation.Simulate();
            Console.ReadLine();

            //SodaMachine testRun = new SodaMachine();
            //Customer Zachary = new Customer();
            //testRun.BeginTransaction(Zachary);
            //Console.ReadLine();
            //Customer testCustomer = new Customer();
            //Coin testCoin;
            //testCoin = testCustomer.GetCoinFromWallet("quarter");

        }
    }
}
