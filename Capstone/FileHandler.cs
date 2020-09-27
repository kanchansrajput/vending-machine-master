namespace Capstone
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class FileHandler
    {
        private const int Pos_itemNumber = 0;
        private const int Pos_ItemName = 1;
        private const int Pos_ItemPrice = 2;
        private const int Pos_ItemQuantity = 3;
        private const int Pos_itemType = 4;

        public Dictionary<string, VendingItem> GetVendingItems()
        {
            Dictionary<string, VendingItem> items = new Dictionary<string, VendingItem>();

            string file = string.Empty;
            if (File.Exists("vendingmachine.csv"))
            {
                file = "vendingmachine.csv";

                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while (!sr.EndOfStream)
                        {
                            // Read the line
                            string line = sr.ReadLine();

                            string[] itemDetails = line.Split("|");

                            string itemName = itemDetails[Pos_ItemName];

                            if (!decimal.TryParse(itemDetails[Pos_ItemPrice], out decimal itemPrice))
                            {
                                itemPrice = 0M;
                            }

                            if (!int.TryParse(itemDetails[Pos_ItemQuantity], out int itemQuantity))
                            {
                                itemQuantity = 0;
                            }

                            int itemsRemaining = 5;

                            VendingItem item;

                            switch (itemDetails[Pos_itemType])
                            {
                                case "Chip":
                                    item = new Chip(itemName, itemPrice, itemQuantity, itemsRemaining);
                                    break;
                                case "Drink":
                                    item = new Drink(itemName, itemPrice, itemQuantity, itemsRemaining);
                                    break;
                                case "Gum":
                                    item = new Gum(itemName, itemPrice, itemQuantity, itemsRemaining);
                                    break;
                                default:
                                    item = new Chip(itemName, itemPrice, itemQuantity, itemsRemaining);
                                    break;
                            }

                            items.Add(itemDetails[Pos_itemNumber], item);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Error trying to open the input file.");
                }
            }
            else
            {
                Console.WriteLine("Input file is missing!! The vending machine will now self destruct.");
                items.Add("A1", new Drink("YOU BROKE IT!", 10000M,5, 5));
            }

            return items;
        }
    }
}
