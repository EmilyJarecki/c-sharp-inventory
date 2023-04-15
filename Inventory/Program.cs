using System;

namespace Inventory
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice, answer = "", user, passwd;
            UserLogon objUserLogon = new UserLogon();


            while (true)
            {

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("1. Sign In");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("3. Exit");

                Console.Write("Choose Your Option(1/2/3):");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("User Id:");
                        user = Console.ReadLine();
                        Console.Write("Password:");
                        passwd = Console.ReadLine();
                        bool userSatus = objUserLogon.CheckUserStatus(user, passwd);
                        if (userSatus == true)
                        {

                            InventoryMenu(user);



                        }
                        else
                        {
                            Console.WriteLine("Invalid User Id/Password");
                            Console.WriteLine("Press Any Key to Continue..");
                            Console.ReadKey();

                        }

                        break;

                    case "2":

                        while (true)
                        {
                            Console.Clear();
                            Console.Write("User Id:");
                            user = Console.ReadLine();
                            Console.Write("Password:");
                            passwd = Console.ReadLine();
                            if (string.IsNullOrEmpty(user.ToString()) || string.IsNullOrEmpty(passwd.ToString()))
                            {
                                Console.Write("User Id and Password cannot be empty ...Do you want to re-enter(Y/N)?:");
                                answer = Console.ReadLine();
                                if (answer.ToUpper().Equals("N") == true)
                                    break;

                            }
                            else
                            {


                                bool availStatus = objUserLogon.CheckUserAvailability(user);
                                if (availStatus == true)
                                {

                                    objUserLogon.SignupUser(user, passwd);
                                    break;
                                }
                                else
                                {
                                    Console.Write("This User Id is not available ...Do you want to re-enter(Y/N)?:");
                                    answer = Console.ReadLine();
                                    if (answer.ToUpper().Equals("N") == true)
                                        break;

                                }

                            }

                        }

                        break;
                    case "3":
                        return;
                    default:
                        Console.Write("Invalid Choice ...Do you want to continue(Y/N)?:");
                        answer = Console.ReadLine();
                        if (answer.ToUpper().Equals("N") == true)
                            return;
                        break;

                }
            }

        }

        static void InventoryMenu(string user)
        {
            string option = "";
            Inventory objInventory = new Inventory();
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"User Id:  {user}");
                Console.WriteLine();
                Console.WriteLine("Techmart Retailers");
                Console.WriteLine("******************");
                Console.WriteLine();
                Console.WriteLine("1. Create Stock Entry");
                Console.WriteLine("2. Sales");
                Console.WriteLine("3. Purchase");
                Console.WriteLine("4. Stock Report");
                Console.WriteLine("5. Purchase Report");
                Console.WriteLine("6. Sales Report");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice(1/2/3/4/5/6/7):");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        objInventory.CreateStock();
                        break;
                    case "2":
                        objInventory.CreateSales();
                        break;
                    case "3":
                        objInventory.CreatePurchase();
                        break;
                    case "4":
                        objInventory.GenerateStockReport();
                        Console.WriteLine();
                        Console.Write("Press any key to continue..");
                        Console.ReadKey();
                        break;
                    case "5":
                        objInventory.GeneratePurchaseReport();
                        Console.WriteLine();
                        Console.Write("Press any key to continue..");
                        Console.ReadKey();
                        break;
                    case "6":
                        objInventory.GenerateSalesReport();
                        Console.WriteLine();
                        Console.Write("Press any key to continue..");
                        Console.ReadKey();
                        break;
                    case "7":
                        return;
                    default:
                        break;


                }


            }

        }

    }
}
