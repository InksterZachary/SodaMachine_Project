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
            Console.WriteLine("What soda would you like to purchase?\n" +
                "Please enter the number that corresponds to your soda choice:\n" +
                "(1) Rootbeer \n" +
                "(2) Orange Soda\n" +
                "(3) Cola\n" +
                "(4) To Exit selection");
            string sodaChoice = Console.ReadLine();
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
                UserInterface.EndMessage("day",0);
                Console.Clear();
                Customer cus = new Customer();
                BeginTransaction(cus);
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
                string newChoice = Console.ReadLine();
                GetSodaFromInventory(newChoice);
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
            double difference = DetermineChange(TotalCoinValue(payment), chosenSoda.Price);
            if (TotalCoinValue(payment) < chosenSoda.Price)
            {
                Console.WriteLine("Insufficient funds. We are replacing the chosen soda. Please retrieve your change below.");
                _inventory.Add(chosenSoda);
                customer.AddCoinsIntoWallet(payment);
            }
            else if (TotalCoinValue(payment) > chosenSoda.Price)
            {
                if (GatherChange(difference) == null)
                {
                    Console.WriteLine("Machine does not have proper change. Please retrieve your payment, below.");
                    _inventory.Add(chosenSoda);
                    customer.AddCoinsIntoWallet(payment);
                }
                else
                {
                    customer.AddCanToBackpack(chosenSoda);
                    DepositCoinsIntoRegister(payment);
                    UserInterface.EndMessage(chosenSoda.Name, difference);
                    
                }
            }
            else if (TotalCoinValue(payment) == chosenSoda.Price)
            {
                foreach (Can can in _inventory)
                {
                    if (can.Name == chosenSoda.Name)
                    {
                        Console.WriteLine("Please retrieve your can of " + chosenSoda.Name + " after it is dispensed and have a wonderful day!");
                        DepositCoinsIntoRegister(payment);
                        customer.AddCanToBackpack(chosenSoda);
                        UserInterface.DisplayWelcomeInstructions(_inventory);
                    }
                }
            }
            else if (TotalCoinValue(payment) == chosenSoda.Price && _inventory.Contains(chosenSoda) == false)
            {
                Console.WriteLine("Sorry about that, looks like we're all out of " + chosenSoda.Name + "\n" +
                    "Please exit the screen and make another selection, thank you!");
                customer.AddCoinsIntoWallet(payment);
            }
        }

        private List<Coin> GatherChange(double changeValue)
        {
            List<Coin> pocketChange = new List<Coin>(); //If this doesn't work I should try = null;
            double n = 0;
            while(n <= changeValue)
            {
                double difference = changeValue - n;
                Quarter quarter = new Quarter();
                Dime dime = new Dime();
                Nickel nickel = new Nickel();
                Penny penny = new Penny();
                System.Threading.Thread.Sleep(500);
                if (difference > 0.25)
                { 
                    RegisterHasCoin(quarter.Name);
                    GetCoinFromRegister(quarter.Name);
                    n += quarter.Value;
                    Console.WriteLine("Dispensing:"+quarter.Name);
                    pocketChange.Add(quarter);
                }
                else if (difference > 0.10)
                {
                    RegisterHasCoin(dime.Name);
                    GetCoinFromRegister(dime.Name);
                    n += dime.Value;
                    Console.WriteLine("Dispensing:" + dime.Name);
                    pocketChange.Add(dime);
                }
                else if (difference > 0.05)
                {
                    RegisterHasCoin(nickel.Name);
                    GetCoinFromRegister(nickel.Name);
                    n += nickel.Value;
                    Console.WriteLine("Dispensing:" + nickel.Name);
                    pocketChange.Add(nickel);
                }
                else if (difference < 0.00)
                {
                    RegisterHasCoin(penny.Name);
                    GetCoinFromRegister(penny.Name);
                    n += difference;
                }
                else if(difference == 0)
                {
                    Console.WriteLine("Please collect your change from below.");
                    return pocketChange;
                }
                else if (n < changeValue + 0.01)
                {
                    Console.WriteLine("Your change will be dispensed below.");
                    return pocketChange;
                }
            }
            Console.WriteLine("There is not enough change in the machine: transaction cancelled.");
            return null;   
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string Name)
        {
            foreach (Coin coin in _register)
            {
                if (Name == coin.Name)
                {
                    return true;
                }
            }
            return false;
        }
        private Coin GetCoinFromRegister(string name)
        {
            Quarter quarter = new Quarter();
            Dime dime = new Dime();
            Nickel nickel = new Nickel();
            Penny penny = new Penny();
            if (name == quarter.Name)
            {
                _register.Remove(quarter);
                return quarter;
            }
            else if (name == dime.Name)
            {
                _register.Remove(dime);
                return dime;
            }
            else if (name == nickel.Name)
            {
                _register.Remove(nickel);
                return nickel;
            }
            else if (name == penny.Name)
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
            double totalValue = 0;
            foreach (Coin coin in payment)
            {
                totalValue += coin.Value;
            }
            return Math.Round(totalValue,3);
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
            foreach(Coin coin in coins)
            {
                _register.Add(coin);
            }
        }
    }
}
