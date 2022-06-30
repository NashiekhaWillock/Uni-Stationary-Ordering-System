using System;
using System.Collections.Generic;

namespace Stationery_Order
{
    internal class Program
    {
        private static UserUI userUI;
       
        private static void Main(string[] args)
        {
            Console.Title = "Nash Willock's Stationery Management System";
            Console.BackgroundColor
           = ConsoleColor.Gray;
            Console.ForegroundColor
                  = ConsoleColor.Black;

            InitialiseData();
            Console.WriteLine("\n Stationery Management System V1");
            DisplayMenu();
            int choice = GetMenuChoice();

            // Each method being excuted is available for selection in the below switch case.

            while (choice != 7)
            {
                switch (choice)
                {
                    case 1:
                        AddToStock();
                        break;

                    case 2:
                        TakeFromStock();
                        break;

                    case 3:
                        ViewInventoryReport();
                        break;

                    case 4:
                        ViewFinancialReport();
                        break;

                    case 5:
                        ViewTransactionReport();
                        break;

                    case 6:
                        ViewPersonalUsage();
                        break;
                }
                DisplayMenu();
                choice = GetMenuChoice();
            }

        }
        static void InitialiseData()
        {
            string username = "w023223i";

            string password = "W023223I";

            Console.Clear();

            DatabaseManager dbMgr = new DatabaseManager(username, password);
            dbMgr.InitialiseDatabase();

            StockManager StKMngr = new StockManager(dbMgr);
            TransactionLog transLog = new TransactionLog(dbMgr);

            userUI = new UserUI(StKMngr, transLog);

        }
        //1st Menu to be displayed
        private static void DisplayMenu()
        {
            Console.WriteLine("\n Please enter a number from the options below to select what you would like to do.\n");
            Console.WriteLine(" 1. Add To Stock");
            Console.WriteLine(" 2. Take From Stock");
            Console.WriteLine(" 3. Inventory Report");
            Console.WriteLine(" 4. Financial Report");
            Console.WriteLine(" 5. Display Transaction Log");
            Console.WriteLine(" 6. Report Personal Usage");
            Console.WriteLine(" 7. Exit");
        }

        // 2nd Menu to be displayed for AddToStock() so user can either add a new item or update existing.
        private static void DisplayMenu2()
        {
            Console.WriteLine("\n Add or Amend Stock Items\n");
            Console.WriteLine("\n Please enter a number from the options below to select what you would like to do.\n");
            Console.WriteLine(" 1. Add A New Stock Item");
            Console.WriteLine(" 2. Amend An Existing Stock Item's Quantity");
            Console.WriteLine(" Or Enter 3 to go back to previous menu.");

        }

        // A method for the 1st switch case options to loop until user enters valid option.
        private static int GetMenuChoice()
        {
            int option = ReadInteger("\n Option");
            while (option < 1 || option > 7)
            {
                Console.WriteLine("\n Choice not recognised. Please try again");
                option = ReadInteger("\n Option");
            }
            return option;
        }

        // A method for the 2nd switch case options to loop until user enters valid option in AddToStock().
        private static int GetMenuChoice2()
        {
            int option = ReadInteger("\n Option");
            while (option < 1 || option > 3)
            {
                Console.WriteLine("\n Choice not recognised. Please try again");
                option = ReadInteger("\n Option");

            }
            return option;
        }

        private static int ReadInteger(string prompt)
        {  // A method used in the switch case options to read then convert each string as an int.
            try
            {
                Console.Write(prompt + ": > ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private static void AddToStock()
        {//AddToStock Method which allows the user to add a new stock item or add items to an existing stock item.
            DisplayMenu2();
            // Each method being excuted for AddToStock is available for selection in the below switch case.
            int choice2 = GetMenuChoice2();

            while (choice2 != 4)
            {
                switch (choice2)
                {
                    case 1:
                        AddItemToStock();
                        break;

                    case 2:
                        AddQtyToStock();     
                        break;
                    case 3:
                        return;

                }
                DisplayMenu2();

                choice2 = GetMenuChoice2();

            }
        }
        private static void AddItemToStock()
        {
            //AddItemToStock() allows the user to add a new stock item.
            ViewInventoryReport();
            Console.Write(" Your Name: ");
            string USERSNAME = Console.ReadLine();
            DateTime TRANS_DATE = DateTime.Now;
            Console.Write(" Name of New Stock Item: ");
            string ITEMNAME = Console.ReadLine();
            Console.Write(" How many do you wish to Add? ");
            int ITEMQTY = Convert.ToInt32(Console.ReadLine());
            Console.Write(" What is the Cost Per Stock Item?: ");
            double COST = Convert.ToDouble(Console.ReadLine());
            string TRANS_TYPE = "Add";
            int ITEMCODE = 0;
           
            Dictionary<string, Stock> Stock = userUI.AddToStock(USERSNAME, ITEMCODE, ITEMQTY, ITEMNAME, TRANS_DATE, COST, TRANS_TYPE);
            
            ViewInventoryReport();
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

        private static void AddQtyToStock()
        { //AddQtyToStock Method which allows the user to add items to an existing stock item.

            ViewInventoryReport();
            Console.Write(" Your Name: ");
            string USERSNAME = Console.ReadLine();
            Console.Write(" The Item Code you wish to update: ");
            int itemcode = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Name of the Item: ");
            string ITEMNAME = Console.ReadLine();
            Console.Write(" Quantity you wish to add: ");
            int itemqty = Convert.ToInt32(Console.ReadLine());
            Console.Write(" The cost: ");
            double cost = Convert.ToDouble(Console.ReadLine());
            string TRANS_TYPE = "Add";

            userUI.AddQtyToStock(USERSNAME, itemcode, itemqty, ITEMNAME, DateTime.Now, cost, TRANS_TYPE);
            ViewInventoryReport();
            try
            {

                Console.Write("\n Stock Quantity Updated.");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
        private static void TakeFromStock()
        {//TakeFromStock Method which allows the user to remove items from an existing stock item.
            ViewInventoryReport();
            Console.Write(" Your Name: ");
            string USERSNAME = Console.ReadLine();
            Console.Write(" The Item Code you wish to update: ");
            int ITEMCODE = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Name of the Item: ");
            string ITEMNAME = Console.ReadLine();
            Console.Write(" How many do you wish to Take? ");
            int ITEMQTY = Convert.ToInt32(Console.ReadLine());
            double COST = 0;
            string TRANS_TYPE = "Remove";
            ViewInventoryReport();

            try
            {
                userUI.TakeQtyFromStock(USERSNAME, ITEMCODE, ITEMQTY, ITEMNAME, DateTime.Now, COST, TRANS_TYPE);

            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

        private static void ViewInventoryReport()
        {//ViewInventoryReport() Method which allows the user to view stock inventory.
            Dictionary<int, Stock> Stock = userUI.ViewInventoryReport();

            foreach (Stock s in Stock.Values)
            {
                DisplayStock(s);
            }
        }
        static void DisplayStock(Stock s)
        {
            Console.WriteLine(
                "\t{0, -20} {1, -20} {2, -20}",
                s.ITEMCODE,
                s.ITEMNAME,
                s.ITEMQTY);
        }

        private static void ViewFinancialReport()
        {//ViewFinancialReport() Method which allows the user to view stock financial records.
            Dictionary<int, Stock> Stock = userUI.ViewFinancialReport();

            foreach (Stock s in Stock.Values)
            {
                DisplayStock(s);
            }

            void DisplayStock(Stock s)
            {
                Console.WriteLine(
                    "\t{0, -20} {1, -20} {2, -20}",
                    s.ITEMCODE,
                    s.ITEMNAME,
                    s.COST);

            }
            TotalExpenditure();
        }

        private static void TotalExpenditure()
        {//TotalExpenditure() which extracts a sum of stock cost from the database.
            Dictionary<double, Stock> Stock = userUI.ViewTotalExpenditure();

            foreach (Stock t in Stock.Values)
            {
                DisplayTotal(t);
            }

            void DisplayTotal(Stock t)
            {
                Console.WriteLine(
                    "\n The Total Expenditure on All Stationery Stock is: £{0} .",
                    t.COST);
            }
        }
        

        private static void ViewPersonalUsage()
        {//ViewPersonalUsage() method which allows the user to either search for a specific users usage or view all records.
            Console.Write("\n Personal Usage Viewer");
            Console.Write(" =======================");
            Console.Write("\n Who's transactions would you like to see? Name: ");
            Console.Write(" Or Press Enter to view all users personal usage records:  ");
            string Username = Console.ReadLine();
            Dictionary<int, TransactionLog> Transaction = userUI.PersonalUsage(Username);

            try
            {
                userUI.ViewPersonalUsage(Username);
                Console.Write("\n Personal Usage Report\n");
                Console.WriteLine("\t{0, -30} {1, -25} {2, -16} {3, -25} {4}", "Transaction Date", "Users Name", "ItemCode", "Item Name", "Quantity");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
            foreach (TransactionLog t in Transaction.Values)
            {
                
                DisplayPersonalUsage(t);
            }
            void DisplayPersonalUsage(TransactionLog t)
            {
                Console.WriteLine(
                    "\t{0, -30} {1, -25} {2, -16} {3, -25} {4}",

                    t.TRANS_DATE,
                    t.USERSNAME,
                    t.ITEMCODE,
                    t.ITEMNAME,
                    t.ITEMQTY);
            }
        }

       
        private static void ViewTransactionReport()
        {//ViewTransactionReport() method which allows the user to view all transactions.

            Add();
            Remove();
           
        }
        private static void Add()
        {//Add() method which allows the user to view all Remove transactions.

            Dictionary<DateTime, TransactionLog> Transactions = userUI.AddTrans();

            foreach (TransactionLog t in Transactions.Values)
            {
                AddTransactions(t);
            }

            void AddTransactions(TransactionLog t)
            {

                Console.WriteLine(
                     "\t{0, -25} {1, -8} {2, -10} {3, -20} {4, -10} {5}",

                    t.TRANS_DATE,
                    t.TRANS_TYPE,
                    t.ITEMCODE,
                    t.ITEMNAME,
                    t.ITEMQTY,
                    t.COST);
            }
        }
        private static void Remove()
        {//Remove() method which allows the user to view all Remove transactions.

            Dictionary<string, TransactionLog> Transactions = userUI.RemTrans();

            foreach (TransactionLog t in Transactions.Values)
            {
                RemTransactions(t);
            }

            void RemTransactions(TransactionLog t)
            {

                Console.WriteLine(
                    "\t{0, -25} {1, -10} {2, -18} {3, -10} {4, -18} {5}",

                    t.TRANS_DATE,
                    t.TRANS_TYPE,
                    t.USERSNAME,
                    t.ITEMCODE,
                    t.ITEMNAME,
                    t.ITEMQTY);
            }
        }
    }
}