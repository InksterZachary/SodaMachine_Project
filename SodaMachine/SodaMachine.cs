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
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        

        private void Transaction(Customer customer)
        {
            Console.WriteLine("What soda would you like to purchase?\n" +
                "Please enter the number that corresponds to your soda choice:\n" +
                "(1) Rootbeer \n" +
                "(2) Orange Soda\n" +
                "(3) Cola\n" +
                "(4) To Exit selection");
            string sodaChoice = Console.ReadLine();
            Console.WriteLine("Due to the high traffic, please have coins ready in case card reader is down.");
            CalculateTransaction(customer.GatherCoinsFromWallet(GetSodaFromInventory(sodaChoice)), GetSodaFromInventory(sodaChoice), customer);
        }
        
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            
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
        
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            Console.WriteLine("Would you like to use card or coin to complete this transaction?");
            string payChoice = Console.ReadLine();
            if (payChoice == "coin")
            {
                double difference = DetermineChange(TotalCoinValue(payment), chosenSoda.Price);
                if (TotalCoinValue(payment) < chosenSoda.Price)
                {
                    Console.WriteLine("Insufficient funds. We are replacing the chosen soda. Please retrieve your change below.");
                    _inventory.Add(chosenSoda);
                    customer.AddCoinsIntoWallet(payment);
                    Console.WriteLine("Please select 'Enter' to end transaction.");
                    Console.ReadLine();
                    Console.Clear();
                    UserInterface.DisplayWelcomeInstructions(_inventory);
                }
                else if (TotalCoinValue(payment) > chosenSoda.Price)
                {
                    if (GatherChange(difference) == null)
                    {
                        Console.WriteLine("Machine does not have proper change. Please retrieve your payment, below.");
                        _inventory.Add(chosenSoda);
                        customer.AddCoinsIntoWallet(payment);
                        Console.WriteLine("Please select 'Enter' to end transaction.");
                        Console.ReadLine();
                        Console.Clear();
                        UserInterface.DisplayWelcomeInstructions(_inventory);
                    }
                    else
                    {
                        customer.AddCanToBackpack(chosenSoda);
                        DepositCoinsIntoRegister(payment);
                        UserInterface.EndMessage(chosenSoda.Name, difference);
                        Console.WriteLine("Please select 'Enter' to end transaction.");
                        Console.ReadLine();
                        Console.Clear();
                        UserInterface.DisplayWelcomeInstructions(_inventory);

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
                            Console.Clear();
                            UserInterface.DisplayWelcomeInstructions(_inventory);
                        }
                    }
                }
                else if (TotalCoinValue(payment) == chosenSoda.Price && _inventory.Contains(chosenSoda) == false)
                {
                    Console.WriteLine("Sorry about that, looks like we're all out of " + chosenSoda.Name + "\n" +
                        "Please select 'enter' exit the screen and make another selection, thank you!");
                    customer.AddCoinsIntoWallet(payment);
                    Console.ReadLine();
                    Console.Clear();
                    UserInterface.DisplayWelcomeInstructions(_inventory);
                }
            }
            else if(payChoice == "card")
            {
                if (TotalCoinValue(customer.Wallet.cc) >= chosenSoda.Price)
                {
                    foreach (Can can in _inventory)
                    {
                        if (can.Name == chosenSoda.Name)
                        {
                            customer.Wallet.cc.Credit(chosenSoda.Price);
                            Console.WriteLine("Please retrieve your can of " + chosenSoda.Name + " after it is dispensed and have a wonderful day!\n" +
                                "To end transaction please press 'enter'");
                            customer.AddCanToBackpack(chosenSoda);
                            Console.ReadLine();
                            Console.Clear();
                            UserInterface.DisplayWelcomeInstructions(_inventory);
                        }
                    }
                }
            }
        }

        private List<Coin> GatherChange(double changeValue)
        {
            List<Coin> pocketChange = new List<Coin>();
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

        private double DetermineChange(double totalPayment, double canPrice)
        {
            double change = totalPayment - canPrice;
            return change;
        }

        private static double TotalCoinValue(CreditCard credicard)
        {
            return credicard.Value;
        }
        private static double TotalCoinValue(List<Coin> payment)
        {
            double totalValue = 0;
            foreach (Coin coin in payment)
            {
                totalValue += coin.Value;
            }
            return Math.Round(totalValue,3);
        }

        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
            foreach(Coin coin in coins)
            {
                _register.Add(coin);
            }
        }
    }
}
