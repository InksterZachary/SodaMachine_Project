using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;

        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
        }

        //Member Methods (Can Do)

        public void FillRegister() //maybe needs to be like penny = new Penny()
        {
            for (int i = 0; i < 50; i++)
            {
                Penny penny = new Penny();
                _register.Add(penny);
                
            }
            for (int i = 0; i < 20; i++)
            {
                Quarter quarter = new Quarter();
                _register.Add(quarter);
                
            }
            for (int i = 0; i < 20; i++)
            {
                Nickel nickel = new Nickel();
                _register.Add(nickel);
                
            }
            for (int i = 0; i < 10; i++)
            {
                Dime dime = new Dime();
                _register.Add(dime);
                
            }
        }
        public void FillInventory()
            {
                for (int i = 0; i < 5; i++)
                {
                    OrangeSoda orangeSoda = new OrangeSoda();
                    _inventory.Add(orangeSoda);
                }
                for (int i = 0; i < 5; i++)
                {
                    Cola cola = new Cola();
                    _inventory.Add(cola);
                }
                for (int i = 0; i < 5; i++)
                {
                    RootBeer rootBeer = new RootBeer();
                    _inventory.Add(rootBeer);
                }
            }
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda. DONE
        //grab the desired soda from the inventory. 
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
            string sodaChoice = Console.ReadLine();
            Console.WriteLine("What soda would you like to purchase?\n" +
                "Please enter the number that corresponds to your soda choice:\n" +
                "(1) Rootbeer \n" +
                "(2) Orange Soda\n" +
                "(3) Cola\n" +
                "(4) To Exit selection");
            GetSodaFromInventory(sodaChoice);
            CalculateTransaction(customer.GatherCoinsFromWallet(GetSodaFromInventory(sodaChoice)), GetSodaFromInventory(sodaChoice), customer); //need to change sodaChoice from being a string to a Can //MAYBE SOLVED
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            //access list _inventory and remove soda user chooses
            if (nameOfSoda == 1.ToString())
            {
                RootBeer rootbeer = new RootBeer();
                _inventory.Remove(rootbeer);
                return rootbeer;
            }
            else if(nameOfSoda == 2.ToString())
            {
                OrangeSoda orangeSoda = new OrangeSoda();
                _inventory.Remove(orangeSoda);
                return orangeSoda;
            }
            else if(nameOfSoda == 3.ToString())
            {
                Cola cola = new Cola();
                _inventory.Remove(cola);
                return cola;
            }
            else if(nameOfSoda == 4.ToString())
            {
                Console.WriteLine("Please leave room for the next customer to choose their soda.");
                return null;
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
                GetSodaFromInventory(nameOfSoda);
            }
            return null;
          //return choice
        }

        ///This is the main method for calculating the result of the transaction.
        ///It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        ///This is the method that will determine the following:
        ///If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Dispense soda, and change to the customer.
        ///If the payment is greater than the cost of the soda, but the machine does not have ample change: Dispense payment back to the customer.
        ///If the payment is exact to the cost of the soda:  Dispense soda.
        ///If the payment does not meet the cost of the soda: dispense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            double difference = TotalCoinValue(payment) - chosenSoda.Price;
            if (TotalCoinValue(payment) < chosenSoda.Price)
            {
                Console.WriteLine("Insufficient funds. We are replacing the chosen soda. Please retrieve your change below.");
                _inventory.Add(chosenSoda);
                customer.AddCoinsIntoWallet(payment);
            }
            else if (TotalCoinValue(payment) > chosenSoda.Price)
            {
                GatherChange(difference);
                if (GatherChange(difference) == null)
                {
                    Console.WriteLine("Machine does not have proper change. Please retrieve your payment, below.");
                    _inventory.Add(chosenSoda);
                    customer.AddCoinsIntoWallet(payment);
                }
                else
                {
                    Console.WriteLine("Have a fantastic day and don't forget your "+chosenSoda.Name);
                    customer.AddCanToBackpack(chosenSoda);
                    customer.AddCoinsIntoWallet(GatherChange(difference));
                }
            }
            else if(TotalCoinValue(payment) == chosenSoda.Price)
            {
                if (_inventory.Contains(chosenSoda))
                {
                    Console.WriteLine("Please retrieve your can of " + chosenSoda.Name + " after it is dispensed and have a wonderful day!");
                    customer.AddCanToBackpack(chosenSoda);
                }
                else
                {
                    Console.WriteLine("Sorry about that, looks like we're all out of "+chosenSoda.Name+"\n" +
                        "Please exit the screen and make another selection, thank you!");
                    customer.AddCoinsIntoWallet(payment);
                }
            }
        }

        private List<Coin> GatherChange(double changeValue)
        {
            List<Coin> pocketChange = new List<Coin>(); //If this doesn't work I should try = null;
            double n = 0;
            double difference = changeValue - n;
            while(n < changeValue)
            {
                Quarter quarter = new Quarter();
                Dime dime = new Dime();
                Nickel nickel = new Nickel();
                Penny penny = new Penny();
                if (difference > 0.25 && _register.Contains(quarter))
                { 
                    RegisterHasCoin(quarter);
                    _register.Remove(quarter);
                    n += quarter.Value;
                    pocketChange.Add(quarter);
                }
                else if (difference > 0.10 && _register.Contains(dime))
                {
                    RegisterHasCoin(dime);
                    _register.Remove(dime);
                    n += dime.Value;
                    pocketChange.Add(dime);
                }
                else if (difference > 0.05 && _register.Contains(nickel))
                {
                    RegisterHasCoin(nickel);
                    _register.Remove(nickel);
                    n += nickel.Value;
                    pocketChange.Add(nickel);
                }
                else if (difference > 0.01 && _register.Contains(penny))
                {
                    RegisterHasCoin(penny);
                    _register.Remove(penny);
                    n += penny.Value;
                    pocketChange.Add(penny);
                }
                else
                {
                    Console.WriteLine("There is not enough change in the machine: transaction cancelled.");
                    return null;
                }

            }
            return pocketChange;
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(Coin name)
        {
            if (_register.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name) //Not exactly sure what this one wants =come back to this one=//SOLVED
        {
            Quarter quarter = new Quarter();
            Dime dime = new Dime();
            Nickel nickel = new Nickel();
            Penny penny = new Penny();
            if (name == quarter.Name && _register.Contains(quarter))
            {
                _register.Remove(quarter);
                return quarter;
            }
            else if (name == dime.Name && _register.Contains(dime))
            {
                _register.Remove(dime);
                return dime;
            }
            else if (name == nickel.Name && _register.Contains(nickel))
            {
                _register.Remove(nickel);
                return nickel;
            }
            else if (name == penny.Name && _register.Contains(penny))
            {
                _register.Remove(penny);
                return penny;
            }
            else
            {
                return null;
            }
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            double change = totalPayment - canPrice;
            return change;
        }
        //Takes in a list of coins to return the total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            double totalValue;
            double n = 0;
            foreach (Coin coin in payment)
            {
                n += coin.Value;
            }
            totalValue = n;
            return totalValue;
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
            foreach(Coin coin in coins)
            {
                coins.Remove(coin);
                _register.Add(coin);
            }
        }
    }
}
