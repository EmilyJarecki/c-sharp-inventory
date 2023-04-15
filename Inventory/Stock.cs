using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory
{
    public class Stock
    {

        public string itemId;
        public string itemName;
        public decimal unitPrice;
        public int stockQty;
        public DateTime creationDate;

        public Stock(string id, string name, decimal price, int qty, DateTime entryDate)
        {
            this.itemId = id;
            this.itemName = name;
            this.unitPrice = price;
            this.stockQty = qty;
            this.creationDate = entryDate;
        }


    }
}
