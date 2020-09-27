namespace Capstone
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Drink : VendingItem
    {
        public const string Message = "Glug, Glug, Yum!";

        public Drink(
            string productName,
            decimal price,
            int quantity,
            int itemsRemaining)
                : base(
                productName,
                price,
                itemsRemaining,
                 Message,
                quantity)
        {
        }
    }
}
