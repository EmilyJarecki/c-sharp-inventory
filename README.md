# Inventory Management Application (C# and .NET Core)

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
## Result
<h3 align='center'>📽<a href="https://lnkd.in/gbTxEmmv">Video Walkthrough</a></br></h3>
Example image shown is of user logged in viewing a Stock Report. It shows the Item Id, Item Name, Unit Price, Quantity in Stock, and Stock Entry Date. 

</br>I generated this formatted stock data report by using the String.Format method:



```
data += String.Format("{0,-20} {1,-20} {2, -20} {3, -20} {4, -20} \n", item.itemId, item.itemName, item.unitPrice, item.stockQty, item.creationDate.ToString("dd/MM/yyyy"))
```
<div align='center'><img align='center' width="750" alt="Screen Shot 2023-04-15 at 9 24 55 PM" src="https://user-images.githubusercontent.com/107048020/232262827-1903ff0a-486b-4b3d-9576-3cbc444f66e2.png"></div>

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
I created user classes, purchase classes, sales classes, and stock  classes to implement best object-oriented programming concepts and to promote code reusability, modularity, and maintainability. By creating these classes, I was able to encapsulate the data and functionality related to each concept into separate classes, making it easier to manage and maintain the codebase. 
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

## Content Includes: 
<ol>
<li>Iteration Statements
<li>Single Dimensional and Multidimensional Arrays
<li>Validating Input Data
<li>Methods with and without parameters
<li>OOP: Class and Objects, Access Modifiers, Constructors with and without Parameters, Array of Objects, Static Classes
<li>String Manipulation: StringBuilder, String Interpolation
<li>Collections: Lists
<li>File Handling: Text and Binary Files
<li>Date Manipulation
<li>Error and Exception Handling
</ol>


## Next Steps:
- [ ] Connect the application to a database
- [ ] Search Sales
- [ ] Search Stock




