using System;
using System.Collections.Generic;
using System.IO;

namespace Inventory
{
    public class Inventory
    {
        static string dirPath = "/Users/emilyjarecki/SEI/C#/Inventory/";
        static string filePathStock = dirPath + "stock.dat";
        static string filePathSales = dirPath + "sales.dat";
        static string filePathPurchase = dirPath + "purchase.dat";
        static List<Stock> stockList = new List<Stock>();
        static List<Sales> salesList = new List<Sales>();
        static List<Purchase> purchaseList = new List<Purchase>();
        static Stock objStock;
        static Sales objSales;
        static Purchase objPurchase;
        public void CreateStock()
        {
            string id, itemName;
            decimal price;
            int qty;
            DateTime entryDate;


            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.Write("Item Id:");
                id = Console.ReadLine().Trim().ToUpper();
                bool duplicateStatus = CheckDuplicateId(id);
                if (duplicateStatus == true)
                {
                    Console.WriteLine("Duplicate Item Id...Press any key to re-enter");
                    Console.ReadKey();
                }
                else
                {
                    break;
                }


            }

            Console.Write("Item Name:");
            itemName = Console.ReadLine();
            Console.Write("Unit Price:");
            price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Quantity:");
            qty = Convert.ToInt32(Console.ReadLine());
            entryDate = DateTime.Now;
            objStock = new Stock(id, itemName, price, qty, entryDate);
            WriteStock(objStock);

        }

        static bool CheckDuplicateId(string id)
        {

            FileStream fs = null;
            BinaryReader br = null;
            bool status = false;

            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePathStock))
                {
                    fs = new FileStream(filePathStock, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    while (br.PeekChar() != -1)
                    {
                        objStock = new Stock(br.ReadString(), br.ReadString(), br.ReadDecimal(), br.ReadInt32(), Convert.ToDateTime(br.ReadString()));
                        stockList.Add(objStock);
                    }

                    if (stockList.Count != 0)
                    {
                        foreach (var stockItem in stockList)
                        {
                            if (id == stockItem.itemId)
                            {
                                status = true;
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
                    fs = new FileStream(filePathStock, FileMode.Append, FileAccess.Write);
                    bw = new BinaryWriter(fs);
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
                String data = String.Format("{0,-20} {1,-20} {2,-20} {3, -20} {4, -20} \n", "Item Id", "Item Name", "Unit Price", "Qty in Stock", "Stock Entry Date");
                Console.WriteLine();
                Console.WriteLine("*********Stock Report*******");
                foreach (var item in stockList)
                {
                    data += String.Format("{0,-20} {1,-20} {2, -20} {3, -20} {4, -20} \n", item.itemId, item.itemName, item.unitPrice, item.stockQty, item.creationDate.ToString("dd/MM/yyyy"));
                }
                Console.WriteLine($"\n{data}");
            }

        }


        static List<Stock> ReadStock()
        {
            FileStream fs = null;
            BinaryReader br = null;
            stockList = new List<Stock>();

            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePathStock))
                {
                    fs = new FileStream(filePathStock, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
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
            List<Stock> availableItemList = new List<Stock>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.Write("Item Id:");
                id = Console.ReadLine().Trim().ToUpper();
                availableItemList = CheckItemAvailable(id);
                if (availableItemList.Count == 0)
                {
                    Console.WriteLine("Item not available...Press any key to re-enter");
                    Console.ReadKey();
                }
                else
                {
                    break;
                }


            }

            foreach (var item in availableItemList)
            {
                itemName = item.itemName;
                unitPrice = item.unitPrice;
            }

            Console.Write("Enter profit percentage:");
            profitPercent = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter sales quantity:");
            saleQty = Convert.ToInt32(Console.ReadLine());
            unitSalesPrice = unitPrice + unitPrice * profitPercent / 100;
            saleDate = DateTime.Now;
            objSales = new Sales(id, itemName, unitSalesPrice, saleQty, saleDate);
            WriteSales(objSales);

        }

        static List<Stock> CheckItemAvailable(string id)
        {
            FileStream fs = null;
            BinaryReader br = null;
            List<Stock> itemInStock = new List<Stock>();


            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePathStock))
                {
                    fs = new FileStream(filePathStock, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    while (br.PeekChar() != -1)
                    {
                        objStock = new Stock(br.ReadString(), br.ReadString(), br.ReadDecimal(), br.ReadInt32(), Convert.ToDateTime(br.ReadString()));
                        stockList.Add(objStock);
                    }

                    if (stockList.Count != 0)
                    {
                        foreach (var stockItem in stockList)
                        {
                            if (id == stockItem.itemId)
                            {
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
                    fs = new FileStream(filePathSales, FileMode.Append, FileAccess.Write);
                    bw = new BinaryWriter(fs);
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
            salesList = ReadSales();

            if (salesList.Count == 0)
                Console.WriteLine("Sales is Empty");
            else
            {
                String data = String.Format("{0,-20} {1,-20} {2,-20} {3, -20} {4, -20} \n", "Item Id", "Item Name", "Unit Price", "Qty Sold", "Sales Date");
                Console.WriteLine();
                Console.WriteLine("*********Sales Report*******");
                foreach (var item in salesList)
                {
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
            List<Stock> availableItemList = new List<Stock>();

            // while (true)
            // {
            Console.Clear();
            Console.WriteLine();
            Console.Write("Item Id:");
            id = Console.ReadLine().Trim().ToUpper();
            availableItemList = CheckItemAvailable(id);
            if (availableItemList.Count == 0)
            {
                Console.WriteLine("Item not found in Stock...Please create a stock entry..");
                Console.ReadKey();
                return;
            }
            //  else
            // {
            //   break;
            //}


            // }

            foreach (var item in availableItemList)
            {
                itemName = item.itemName;
                unitPrice = item.unitPrice;
            }


            Console.Write("Enter purchase quantity:");
            purchaseQty = Convert.ToInt32(Console.ReadLine());
            purchaseDate = DateTime.Now;

            objPurchase = new Purchase(id, itemName, unitPrice, purchaseQty, purchaseDate);
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
                    fs = new FileStream(filePathPurchase, FileMode.Append, FileAccess.Write);
                    bw = new BinaryWriter(fs);
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
                String data = String.Format("{0,-20} {1,-20} {2,-20} {3, -20} {4, -20} \n", "Item Id", "Item Name", "Unit Price", "Purchased Qty", "Purchase Date");
                Console.WriteLine();
                Console.WriteLine("*********Purchase Report*******");
                foreach (var item in purchaseList)
                {
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



