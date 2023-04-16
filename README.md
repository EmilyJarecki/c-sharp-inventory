# Inventory Management Application (C# and .NET)

Goal: Create a menu driven application to manage the inventory in an online store.
```
Menu Choices
*************
1. Create Stock Entry
2. Sales
3. Purchase
4. Stock Report
5. Purchase Report
6. Sales Report
7. Exit
```

## Schematic Diagram
<details>
<summary>View Diagrams</summary>
<div>
<img width="874" alt="Screen Shot 2023-04-15 at 8 55 36 PM" src="https://user-images.githubusercontent.com/107048020/232262457-27a34f52-5d29-41cc-b544-e2c954fbadba.png"/>
<img width="874" alt="Screen Shot 2023-04-15 at 8 55 49 PM" src="https://user-images.githubusercontent.com/107048020/232262459-fa68b4d2-89e5-40a1-91ce-442a59f80ccf.png"/>
<img width="874" alt="Screen Shot 2023-04-15 at 8 56 14 PM" src="https://user-images.githubusercontent.com/107048020/232262460-bb58cc37-f2eb-460e-a72d-695f39e71fa4.png"/>
</div>
</details>

## Classes
<details>
<summary>User Class</summary>

```
public class User
    {
        public string userId;
        public string password;

        public User(string user, string pwd)
        {
            this.userId = user;
            this.password = pwd;
        }

    }
```
</details>
<details>
<summary>Sales Class</summary>

```
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
```
</details>
<details>
<summary>Purchase Class</summary>

```
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
```
</details>
<details>
<summary>Stock Class</summary>

```
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
```
</details>

## Next Steps:
- [ ] Connect the application to a database




