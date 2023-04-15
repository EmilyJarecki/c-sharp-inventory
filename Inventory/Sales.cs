using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory
{
    public class Sales
    {

        public string itemId;
        public string itemName;
        public decimal salesUnitPrice;
        public int qtySold;
        public DateTime salesDate;

        public Sales(string id, string name, decimal price, int qty, DateTime entryDate)
        {
            this.itemId = id;
            this.itemName = name;
            this.salesUnitPrice = price;
            this.qtySold = qty;
            this.salesDate = entryDate;
        }
    }
}
