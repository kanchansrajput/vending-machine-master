namespace Capstone
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class VendingMachine
    {
        private Logger Log = new Logger();
        public Dictionary<string, VendingItem> VendingMachineItems = new Dictionary<string, VendingItem>();
        FileHandler HandleFiles = new FileHandler();
        public Money Money { get; }
        public string NotEnoughMoneyError = "Not enough money in the machine to complete the transaction.";
        public string MessageToUser;

        public VendingMachine()
        {
            this.VendingMachineItems = this.HandleFiles.GetVendingItems();
            this.Money = new Money(this.Log);

        }

        public decimal MoneyInMachine
        {
            get
            {
                return this.Money.MoneyInMachine;
            }
        }

        public void DisplayAllItems()
        {
            Console.WriteLine($"\n\n{"#".PadRight(5)} { "Product".PadRight(20) } { "Quantity" } { "Price"}");

            foreach (KeyValuePair<string, VendingItem> kvp in this.VendingMachineItems)
            {
                if (kvp.Value.ItemsRemaining > 0)
                {
                    string itemNumber = kvp.Key.PadRight(5);
                    string productName = kvp.Value.ProductName.PadRight(20);
                    string quantity = "(" + kvp.Value.Quantity.ToString() + ")";
                    string price = kvp.Value.Price.ToString("C");
                    Console.WriteLine($"{itemNumber} {productName} {quantity} : {price}");
                }
                else
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value.ProductName} IS SOLD OUT.");
                }
            }
        }

        public bool ItemExists(string itemNumber)
        {
            return this.VendingMachineItems.ContainsKey(itemNumber);
        }

        public bool RetreiveItem(string itemNumber)
        {
            // If the item exists (not Q1 or something like that)
            // and we can remove the item
            // and we have more money in the machine than the price
            if (this.ItemExists(itemNumber)
                && this.Money.MoneyInMachine >= this.VendingMachineItems[itemNumber].Price
                && this.VendingMachineItems[itemNumber].ItemsRemaining > 0
                && this.VendingMachineItems[itemNumber].RemoveItem())
            {
                // Logging message "CANDYBARNAME A1"
                string message = $"{this.VendingMachineItems[itemNumber].ProductName.ToUpper()} {itemNumber}";

                // Logging before: current money in machine
                decimal before = this.Money.MoneyInMachine;

                // Remove the money
                this.Money.RemoveMoney(this.VendingMachineItems[itemNumber].Price);

                // Logging after: current money in machine
                decimal after = this.Money.MoneyInMachine;

                // Log the log
                this.Log.Log(message, before, after);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void OrderItem(string[] command)
        {
            var itemNumber = command[2].ToString();
            if (!decimal.TryParse(command[1].ToString(), out decimal price))
            {
                Console.WriteLine($"ITEM PRICE {command[1].ToString()} IS NOT VALID.");
            }

            if (!int.TryParse(command[3].ToString(), out int quantity))
            {
                Console.WriteLine($"ITEM QUANTITY {command[3].ToString()} IS NOT VALID.");
            }

            if (this.ItemExists(itemNumber.ToString())
                && this.VendingMachineItems[itemNumber].Quantity > quantity
                && this.VendingMachineItems[itemNumber].Price == price)
            {
                Console.WriteLine($"PRODUCT :{ this.VendingMachineItems[itemNumber].ProductName.ToUpper()} ORDER IS SUCCESSFUL.");
            }
            else if (this.ItemExists(itemNumber.ToString()))
            {
                if (this.VendingMachineItems[itemNumber].Price != price)
                {
                    Console.WriteLine($"PRODUCT :{ this.VendingMachineItems[itemNumber].ProductName.ToUpper()}  PRICE DOES NOT MATCH. PRICE SHOULD BE {this.VendingMachineItems[itemNumber].Price.ToString()}");
                }

                if (this.VendingMachineItems[itemNumber].Quantity < quantity)
                {
                    Console.WriteLine($"PRODUCT :{ this.VendingMachineItems[itemNumber].ProductName.ToUpper()}  QUANTITY IS HIGH. QUANTITY SHOULD BE <= { this.VendingMachineItems[itemNumber].Quantity.ToString()}");
                }
            }
            else
            {
                Console.WriteLine($"NO PRODUCT EXIST WITH GIVEN NUMBER.{itemNumber.ToString()}");
            }
        }
    }
}
