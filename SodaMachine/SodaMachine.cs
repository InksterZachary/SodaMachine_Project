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
        public Penny penny;
        public Nickel nickel;
        public Dime dime;
        public Quarter quarter;
        public OrangeSoda orangeSoda;
        public Cola cola;
        public RootBeer rootbeer;

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
            for (int i = 0; i < 15; i++)
            {
                _register.Add(penny = new Penny());
                i++;
            }
            for (int i = 0; i < 12; i++)
            {
                _register.Add(quarter = new Quarter());
                i++;
            }
            for (int i = 0; i < 17; i++)
            {
                _register.Add(nickel = new Nickel());
                i++;
            }
            for (int i = 0; i < 10; i++)
            {
                _register.Add(dime = new Dime());
                i++;
            }
        }
        public void FillInventory()
            {
                for (int i = 0; i < 5; i++)
                {
                    _inventory.Add(orangeSoda = new OrangeSoda());
                }
                for (int i = 0; i < 5; i++)
                {
                    _inventory.Add(cola = new Cola());
                }
                for (int i = 0; i < 5; i++)
                {
                    _inventory.Add(rootbeer = new RootBeer());
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
                "(1) Rootbeer\n" +
                "(2) Orange Soda\n" +
                "(3) Cola");
            GetSodaFromInventory(sodaChoice);

        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            //access list _inventory and remove soda user chooses
            if (nameOfSoda == 1.ToString())
            {
                _inventory.Remove(rootbeer);
                return rootbeer;
            }
            else if(nameOfSoda == 2.ToString())
            {
                _inventory.Remove(orangeSoda);
                return orangeSoda;
            }
            else if(nameOfSoda == 3.ToString())
            {
                _inventory.Remove(cola);
                return cola;
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
                GetSodaFromInventory(nameOfSoda);
            }
            return null;
          //return choice
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Dispense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Dispense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Dispense soda.
        //If the payment does not meet the cost of the soda: dispense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            if(TotalCoinValue(payment) < chosenSoda.Price)
            {
                Console.WriteLine("Insufficient funds. We are replacing the chosen soda.");
                _inventory.Add(chosenSoda);

            }
            GatherChange(chosenSoda.Price);
            
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue)
        {
            double n = 0;
            
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
        private Coin GetCoinFromRegister(string name) //Not exactly sure what this one wants =come back to this one=
        {
            
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
