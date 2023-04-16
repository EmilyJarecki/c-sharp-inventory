using System;
using System.Collections.Generic;
using System.IO;

namespace Inventory
{
    public class Inventory
    {
        //class-level varaibles so can be accessed anywhere in the Inventory class
        //file paths
        static string dirPath = "/Users/emilyjarecki/SEI/C#/Inventory/";
        static string filePathStock = dirPath + "stock.txt";
        static string filePathSales = dirPath + "sales.txt";
        static string filePathPurchase = dirPath + "purchase.txt";

        //list Stock objects
        static List<Stock> stockList = new List<Stock>();
        static List<Sales> salesList = new List<Sales>();
        static List<Purchase> purchaseList = new List<Purchase>();

        //objStock is just a single object of class Stock
        //whereas stockList is a list of objStocks
        static Stock objStock;
        static Sales objSales;
        static Purchase objPurchase;

        public void CreateStock()
        {
            //local variables just for this method
            string id, itemName;
            decimal price;
            int qty;
            DateTime entryDate;


            while (true)
            {
                Console.Clear();
                Console.WriteLine();

                //create item where each item id is unique
                Console.Write("Item Id: ");
                id = Console.ReadLine().Trim().ToUpper();

                //id information passed into function that checks if id is a duplicate
                //method is in this file
                bool duplicateStatus = CheckDuplicateId(id);
                if (duplicateStatus == true)
                    //repeating loop because it hasn't broken out of it
                {
                    Console.WriteLine("Duplicate Item Id...Press any key to re-enter");
                    Console.ReadKey();
                }
                //if it is unique, break out of this cycle and as for name, price, etc. 
                else
                {
                    break;
                }


            }

            //need to convert to specific datatype because it's being converted to Stock object
            Console.Write("Item Name:");
            itemName = Console.ReadLine();
            Console.Write("Unit Price:");
            price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Quantity:");
            qty = Convert.ToInt32(Console.ReadLine());
            entryDate = DateTime.Now;

            //passing our gathered info into Stock constructor class to initialize object
            objStock = new Stock(id, itemName, price, qty, entryDate);

            //write it into the stock file
            WriteStock(objStock);

        }

        static bool CheckDuplicateId(string id)
         //this method recieved information via parameter
        {

            FileStream fs = null;
            BinaryReader br = null;

            //default result which will be updated at the end of this function
            bool status = false;

            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePathStock))
                {
                    //(filename (defined as a class-level variable), opens the file for reading if it exists, enables reading from file)
                    fs = new FileStream(filePathStock, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);

                    //if PeekChar returns -1, that means it is the last file
                    //so this loop is happening if there still are ids to go through in the file
                    while (br.PeekChar() != -1)
                    {
                        //this is reading all of the variables in order of Stock item (seen in Stock.cs) and converting them to corresponding variable
                        //passed into constructor of Stock class
                        //it's initialized 
                        objStock = new Stock(br.ReadString(), br.ReadString(), br.ReadDecimal(), br.ReadInt32(), Convert.ToDateTime(br.ReadString()));

                        //defined above
                        stockList.Add(objStock);
                    }

                    if (stockList.Count != 0)
                    {
                        foreach (var stockItem in stockList)
                        {
                            //iterating theough each item in stockList
                            //comparing values of item id
                            if (id == stockItem.itemId)
                            {
                                status = true;
                                break;
                            }
                        }



                    }

                }

            }
            //managing exceptions
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePathStock} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    br.Close();
                    fs.Close();
                }
            }
            //defined a few line above as false
            //as it went through this method test, it could have updated to true depending if id is repeated
            return status;
        }

        static void WriteStock(Stock objStock)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                if (Directory.Exists(dirPath))
                {
                    //(filename (defined as a class-level variable), opens the file for adding, enables writing into file)

                    fs = new FileStream(filePathStock, FileMode.Append, FileAccess.Write);
                    bw = new BinaryWriter(fs);
                    //writing this information into file
                    bw.Write(objStock.itemId);
                    bw.Write(objStock.itemName);
                    bw.Write(objStock.unitPrice);
                    bw.Write(objStock.stockQty);
                    bw.Write(objStock.creationDate.ToString());



                }
                else
                {
                    Console.WriteLine("Directory does not exist");
                }
            }
            //handling exceptions
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePathStock} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                //once we finish adding, we close it
                if (fs != null)
                {
                    bw.Close();
                    fs.Close();
                }
            }
        }

        public void GenerateStockReport()
        {

            stockList = new List<Stock>();
            stockList = ReadStock();

            if (stockList.Count == 0)
                Console.WriteLine("Stock is Empty");
            else
            {
                //format header via String.Format
                String data = String.Format("{0,-20} {1,-20} {2,-20} {3, -20} {4, -20} \n", "Item Id", "Item Name", "Unit Price", "Qty in Stock", "Stock Entry Date");
                Console.WriteLine();
                Console.WriteLine("*********Stock Report*******");

                //traverse each record and retrieve 
                foreach (var item in stockList)
                {
                    //format items via String.Format
                    data += String.Format("{0,-20} {1,-20} {2, -20} {3, -20} {4, -20} \n", item.itemId, item.itemName, item.unitPrice, item.stockQty, item.creationDate.ToString("dd/MM/yyyy"));
                }
                Console.WriteLine($"\n{data}");
            }

        }


        static List<Stock> ReadStock()
        {
            FileStream fs = null;
            BinaryReader br = null;

            //stockList instatiated
            stockList = new List<Stock>();

            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePathStock))
                {
                    //(stock.dat, open, read)
                    fs = new FileStream(filePathStock, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);

                    //
                    while (br.PeekChar() != -1)
                    {
                        objStock = new Stock(br.ReadString(), br.ReadString(), br.ReadDecimal(), br.ReadInt32(), Convert.ToDateTime(br.ReadString()));
                        stockList.Add(objStock);
                    }
                }
                else
                {
                    Console.WriteLine("Directory or File does not exist");
                }
            }
            //handle exceptions
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePathStock} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    br.Close();
                    fs.Close();
                }
            }
            return stockList;
        }


        public void CreateSales()
        {
            string id, itemName = "";
            decimal unitPrice = 0, unitSalesPrice;
            decimal profitPercent;
            int saleQty;
            DateTime saleDate;

            //new list of avaliable items stock 
            List<Stock> availableItemList = new List<Stock>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.Write("Item Id:");

                //gathering item info
                id = Console.ReadLine().Trim().ToUpper();
                availableItemList = CheckItemAvailable(id);

                //making sure it's avaliable
                //in the CheckItemAvaliable(), we add the item with the id=id into avaliableItemList
                //so if it returns empty, it means that there is no item
                if (availableItemList.Count == 0)
                {
                    //loop continues
                    Console.WriteLine("Item not available...Press any key to re-enter");
                    Console.ReadKey();
                }
                else
                {
                    break;
                }


            }

            //getting other info about the object because we want to create a sales entry
            foreach (var item in availableItemList)
            {
                itemName = item.itemName;
                unitPrice = item.unitPrice;
            }

            //details for creating sales entry
            Console.Write("Enter profit percentage:");
            profitPercent = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter sales quantity:");
            saleQty = Convert.ToInt32(Console.ReadLine());
            unitSalesPrice = unitPrice + unitPrice * profitPercent / 100;
            saleDate = DateTime.Now;
            //write it into sales file so we create an object to store the info
            //call constructor and pass this information to create an object sold
            objSales = new Sales(id, itemName, unitSalesPrice, saleQty, saleDate);
            //enter info via new method
            WriteSales(objSales);

        }
        //if item in stock is avaliable
        static List<Stock> CheckItemAvailable(string id)
        {
            FileStream fs = null;
            BinaryReader br = null;
            //open file of stock
            List<Stock> itemInStock = new List<Stock>();


            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePathStock))
                {
                    //(stock.dat, open file, read file)
                    fs = new FileStream(filePathStock, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);

                    //if it's not the last item, keep the loop going
                    while (br.PeekChar() != -1)
                    {
                        //reading all items of stock
                        objStock = new Stock(br.ReadString(), br.ReadString(), br.ReadDecimal(), br.ReadInt32(), Convert.ToDateTime(br.ReadString()));
                        //stockList is class varaible
                        //stockList will contain all info
                        //add each one to stockList
                        stockList.Add(objStock);
                    }

                    if (stockList.Count != 0)
                        //traverse through each item in list we created
                    {
                        foreach (var stockItem in stockList)
                        {
                            //id was passed as argument into this function (the one we want to sell)
                            if (id == stockItem.itemId)
                            {
                                //add item avaliable to list we created above in Stock-format
                                itemInStock.Add(stockItem);
                                break;
                            }
                        }

                    }

                }

            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePathStock} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    br.Close();
                    fs.Close();
                }
            }
            //if the list returns not empty, then the item is avaliable
            return itemInStock;

        }

        static void WriteSales(Sales objSales)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                if (Directory.Exists(dirPath))
                {
                    //(sales.dat, add item, write into the file)
                    fs = new FileStream(filePathSales, FileMode.Append, FileAccess.Write);
                    bw = new BinaryWriter(fs);
                    //written into binary file
                    bw.Write(objSales.itemId);
                    bw.Write(objSales.itemName);
                    bw.Write(objSales.salesUnitPrice);
                    bw.Write(objSales.qtySold);
                    bw.Write(objSales.salesDate.ToString());



                }
                else
                {
                    Console.WriteLine("Directory does not exist");
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePathSales} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    bw.Close();
                    fs.Close();
                }
            }

        }

        public void GenerateSalesReport()
        {

            salesList = new List<Sales>();
            //all information from Sales List is retrieved to variable
            salesList = ReadSales();

            if (salesList.Count == 0)
                Console.WriteLine("Sales is Empty");
            else
            {
                //format header
                String data = String.Format("{0,-20} {1,-20} {2,-20} {3, -20} {4, -20} \n", "Item Id", "Item Name", "Unit Price", "Qty Sold", "Sales Date");
                Console.WriteLine();
                Console.WriteLine("*********Sales Report*******");
                foreach (var item in salesList)
                {
                    //format data
                    data += String.Format("{0,-20} {1,-20} {2, -20} {3, -20} {4, -20} \n", item.itemId, item.itemName, item.salesUnitPrice, item.qtySold, item.salesDate.ToString("dd/MM/yyyy"));
                }
                Console.WriteLine($"\n{data}");
            }
        }

        static List<Sales> ReadSales()
        {
            FileStream fs = null;
            BinaryReader br = null;
            salesList = new List<Sales>();

            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePathSales))
                {
                    fs = new FileStream(filePathSales, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    while (br.PeekChar() != -1)
                    {
                        objSales = new Sales(br.ReadString(), br.ReadString(), br.ReadDecimal(), br.ReadInt32(), Convert.ToDateTime(br.ReadString()));
                        salesList.Add(objSales);
                    }
                }
                else
                {
                    Console.WriteLine("Directory or File does not exist");
                }
            }
            //handle exceptions
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePathSales} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    br.Close();
                    fs.Close();
                }
            }
            return salesList;
        }

        public void CreatePurchase()
        {
            string id, itemName = "";
            decimal unitPrice = 0;
            int purchaseQty;
            DateTime purchaseDate;

            //create list of items aaliable to purchase
            List<Stock> availableItemList = new List<Stock>();


            Console.Clear();
            Console.WriteLine();
            Console.Write("Item Id:");
            id = Console.ReadLine().Trim().ToUpper();

            //check if item is avaliable via id
            availableItemList = CheckItemAvailable(id);

            if (availableItemList.Count == 0)
            {
                Console.WriteLine("Item not found in Stock...Please create a stock entry..");
                Console.ReadKey();
                //will jump out of CreatePurchase method and go back to previous method(inventory method/menu)
                return;
            }

            //retrieve other details from object of item in avaliableItemList (the item you want to purchase)
            foreach (var item in availableItemList)
            {
                itemName = item.itemName;
                unitPrice = item.unitPrice;
            }


            Console.Write("Enter purchase quantity:");
            purchaseQty = Convert.ToInt32(Console.ReadLine());
            purchaseDate = DateTime.Now;

            //call Purchase constructor, passing in these details to create purchase object
            objPurchase = new Purchase(id, itemName, unitPrice, purchaseQty, purchaseDate);
            //pass in the object of the purchased class to record purchase 
            WritePurchase(objPurchase);

        }


        static void WritePurchase(Purchase objPurchase)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                if (Directory.Exists(dirPath))
                {
                    //(purchase.dat, add to file, write to file)
                    fs = new FileStream(filePathPurchase, FileMode.Append, FileAccess.Write);
                    bw = new BinaryWriter(fs);
                    //writing the object (passed in) into the purchase file
                    bw.Write(objPurchase.itemId);
                    bw.Write(objPurchase.itemName);
                    bw.Write(objPurchase.purchaseUnitPrice);
                    bw.Write(objPurchase.qtyPurchased);
                    bw.Write(objPurchase.purchaseDate.ToString());



                }
                else
                {
                    Console.WriteLine("Directory does not exist");
                }
            }
            //manage exceptions
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePathPurchase} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    bw.Close();
                    fs.Close();
                }
            }

        }

        public void GeneratePurchaseReport()
        {

            purchaseList = new List<Purchase>();
            purchaseList = ReadPurchase();

            if (purchaseList.Count == 0)
                Console.WriteLine("Purchase is Empty");
            else
            {
                //format header
                String data = String.Format("{0,-20} {1,-20} {2,-20} {3, -20} {4, -20} \n", "Item Id", "Item Name", "Unit Price", "Purchased Qty", "Purchase Date");
                Console.WriteLine();
                Console.WriteLine("*********Purchase Report*******");
                foreach (var item in purchaseList)
                {
                    //format datasimilarly
                    data += String.Format("{0,-20} {1,-20} {2, -20} {3, -20} {4, -20} \n", item.itemId, item.itemName, item.purchaseUnitPrice, item.qtyPurchased, item.purchaseDate.ToString("dd/MM/yyyy"));
                }
                Console.WriteLine($"\n{data}");
            }

        }

        static List<Purchase> ReadPurchase()
        {
            FileStream fs = null;
            BinaryReader br = null;
            purchaseList = new List<Purchase>();

            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePathPurchase))
                {
                    fs = new FileStream(filePathPurchase, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    while (br.PeekChar() != -1)
                    {
                        objPurchase = new Purchase(br.ReadString(), br.ReadString(), br.ReadDecimal(), br.ReadInt32(), Convert.ToDateTime(br.ReadString()));
                        purchaseList.Add(objPurchase);
                    }
                }
                else
                {
                    Console.WriteLine("Directory or File does not exist");
                }
            }

            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePathPurchase} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    br.Close();
                    fs.Close();
                }
            }
            return purchaseList;
        }

    }
}



