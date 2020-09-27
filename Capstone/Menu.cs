namespace Capstone
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Menu
    {
        public void Display()
        {
            VendingMachine vm = new VendingMachine();

            PrintHeader();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1] Display the current inventory, Type Command-->> env");
                Console.WriteLine("2] Order, Type Command-->> order <amount><item_number><Quantity> ");
                Console.WriteLine("Q] Quit");

                Console.Write("What option do you want to select? ");
                string input = Console.ReadLine().Trim();
                string[] commandSplit = input.Split(" ");
                if (input == "env")
                {
                    Console.WriteLine("Displaying Items");
                    vm.DisplayAllItems();
                }
                else if (commandSplit.Length == 4 && commandSplit[0].ToLower().ToString() == "order")
                {
                    vm.OrderItem(commandSplit);
                }
                else if (input.ToUpper() == "Q")
                {
                    Console.WriteLine("Quitting");
                    break;
                }
                else
                {
                    Console.WriteLine("Please try again");
                }

                Console.ReadLine();
            }
        }

        private static void PrintHeader()
        {
            Console.WriteLine("WELCOME TO THE BEST VENDING MACHINE EVER!!!! (Distant crowd roar)");
        }

    }
}
