using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        //Member Variables (Has A)
        public Wallet Wallet;
        public Backpack Backpack;

        //Constructor (Spawner)
        public Customer()
        {
            Wallet = new Wallet();
            Backpack = new Backpack();
        }
        //Member Methods (Can Do)

        //This method will be the main logic for a customer to retrieve coins form their wallet.
        //Takes in the selected can for price reference
        //Will need to get user input for coins they would like to add.
        //When all is said and done this method will return a list of coin objects that the customer will use a payment for their soda.
        public List<Coin> GatherCoinsFromWallet(Can selectedCan)
        {
            List<Coin> payment = new List<Coin>();
            double n = 0;
            Quarter quarter = new Quarter();
            Dime dime = new Dime();
            Nickel nickel = new Nickel();
            Penny penny = new Penny();
            Console.WriteLine("Please choose your coins one at a time.");
            while (n < selectedCan.Price)
            {
                double difference = selectedCan.Price - n;
                Console.WriteLine("Total still owed is: "+ difference);
                string userChoice = Console.ReadLine();
                if(userChoice == "quarter")
                {
                    GetCoinFromWallet("quarter");
                    n += Wallet.quarter.Value;
                    payment.Add(quarter);
                }
                else if(userChoice == "dime")
                {
                    GetCoinFromWallet("dime");
                    n += Wallet.dime.Value;
                    payment.Add(dime);
                }
                else if(userChoice == "nickel")
                {
                    GetCoinFromWallet("nickel");
                    n += Wallet.nickel.Value;
                    payment.Add(nickel);
                }
                else if(userChoice == "penny")
                {
                    GetCoinFromWallet("penny");
                    n += Wallet.penny.Value;
                    payment.Add(penny);
                }
                else
                {
                    Console.WriteLine("Insufficient funds. Transaction cancelled.");
                    Console.Clear();
                }
            }
            return payment;
        }
        //Returns a coin object from the wallet based on the name passed into it.
        //Returns null if no coin can be found
        public Coin GetCoinFromWallet(string coinName)
        {
            if(coinName == Wallet.quarter.Name)
            {
                Wallet.Coins.Remove(Wallet.quarter);
                return Wallet.quarter;
            }
            else if(coinName == Wallet.dime.Name)
            {
                Wallet.Coins.Remove(Wallet.dime);
                return Wallet.dime;
            }
            else if(coinName == Wallet.nickel.Name)
            {
                Wallet.Coins.Remove(Wallet.nickel);
                return Wallet.nickel;
            }
            else if(coinName == Wallet.penny.Name)
            {
                Wallet.Coins.Remove(Wallet.penny);
                return Wallet.penny;
            }
            Console.WriteLine("You don't have that coin in your wallet\n" +
                    "Please choose a different coin.");
            return null;
        }
        //Takes in a list of coin objects to add into the customers wallet.
        public void AddCoinsIntoWallet(List<Coin> coinsToAdd)
        {
            foreach(Coin coin in coinsToAdd)
            {
                //coinsToAdd.Remove(coin);
                Wallet.Coins.Add(coin);
            }
        }
        //Takes in a can object to add to the customers backpack.
        public void AddCanToBackpack(Can purchasedCan)
        {
            Backpack.cans.Add(purchasedCan);
        }
    }
}
