using System;
using System.Collections.Generic;
using System.Text;
namespace Inventory
{
    public class Purchase
    {
        public string itemId;
        public string itemName;
        public decimal purchaseUnitPrice;
        public int qtyPurchased;
        public DateTime purchaseDate;
        public Purchase(string id, string name, decimal price, int qty, DateTime purchDate)
        {
            this.itemId = id;
            this.itemName = name;
            this.purchaseUnitPrice = price;
            this.qtyPurchased = qty;
            this.purchaseDate = purchDate;
        }
    }
}
