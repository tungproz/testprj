using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MPC_Persistence;
using MPC_BL;
using MPC_DAL;
using System.Text;


namespace PL_Console
{
    class OrderConsole
    {
        public static Table t = new Table();
        public static Item it = new Item();
        public static Order o = new Order();
        public static OrderBL obl = new OrderBL();
        public static TableBL tbl = new TableBL();
        public static ItemBL itbl = new ItemBL();
        public static List<Order> orl = new List<Order>();

        public static bool Add(Account a, Order o)
        {

            o.OrderTable = new Table();
            o.OrderAccount = new Account();
            int tableid;
            Console.WriteLine("|| id table  ||      table name     ||     table status     ||");
            foreach (var item in tbl.display())
            {
                if (item.Status == 0)
                {
                    string stt = "empty";
                    Console.WriteLine("||{0,10}||{1,20}||{2,20}||", item.Table_Id, item.TableName, stt);
                }
                else if (item.Status == 1)
                {
                    string stt = "has some one";
                    Console.WriteLine("||{0,10}||{1,20}||{2,20}||", item.Table_Id, item.TableName, stt);
                }
            }
            while (true)
            {
                Console.WriteLine("Input table Id: ");
                tableid = Convert.ToInt32(Console.ReadLine());
                while (tableid == 0 || tableid > 36)
                {
                    Console.Write("Pre-enter: ");
                    try
                    {
                        tableid = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        // Console.WriteLine("error"+ e);
                        // Console.Clear();
                        Console.Write("");
                    }
                }
                var result = tbl.CheckTableById(tableid);

                if (result == true)
                {
                    t = (tbl.GetTableById(tableid));

                    break;
                }

                else
                {
                    Console.WriteLine("Cant find this table or table is not empty!!!");
                    continue;
                }
            }
            o.OrderTable.Table_Id = tableid;
            o.OrderAccount.Account_Id = a.Account_Id;



            Console.WriteLine("|| item id  ||      Item name     ||     item price     ||");
            foreach (var item in itbl.display())
            {
                Console.WriteLine("||{0,10}||{1,20}||{2,20}||", item.ItemId, item.ItemName, item.ItemPrice);
            }

            while (true)
            {
                Console.WriteLine(" Input Item Id: ");
                int itemid;
                while (!int.TryParse(Console.ReadLine(), out itemid) && itemid < 100 && itemid > 0)
                {
                    Console.WriteLine("Invalid entry. Please enter a number.");
                }

                Console.WriteLine("Input quantity item: ");
                int quantity = Convert.ToInt32(Console.ReadLine());
                while (quantity <= 0 || quantity > 100)
                {
                    Console.WriteLine("Dont try to test my system :)!!");
                    Console.Write("Pre-enter: ");
                    try
                    {
                        quantity = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        // Console.WriteLine("error"+ e);
                        // Console.Clear();
                        Console.Write("");
                    }
                    //  obl.AddItemToOrder(itemid, quantity, o);
                }
                obl.AddItemToOrder(itemid, quantity, o);
                //it = itbl.GetItemById(itemid);
                Console.WriteLine("Do you want to continue Add Item? ");
                string choice2 = Console.ReadLine();
                if (choice2 == "Y" || choice2 == "y")
                {
                    continue;
                }
                else if (choice2 == "N" || choice2 == "n")
                {
                    break;
                }
            }
            Console.WriteLine("Do you want to create order: ");
            char choice3 = Convert.ToChar(Console.ReadLine());
            switch (choice3)
            {
                case 'y':
                    Console.WriteLine("Create Order: " + (obl.CreateOrder(o) ? "completed!" : "not complete!"));
                    o.ItemsList.Clear();
                    Console.WriteLine("Order ID: " + o.OrderId);

                    Console.WriteLine("Order Date: " + o.OrderDate);

                    foreach (var item in o.ItemsList)
                    {
                        Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");
                    }
                    Console.WriteLine("Order By: " + a.StaffName);
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;

                case 'Y':
                    Console.WriteLine("Create Order: " + (obl.CreateOrder(o) ? "completed!" : "not complete!"));
                    foreach (var item in o.ItemsList)
                    {
                        Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");
                    }
                    Console.WriteLine("Order By: " + a.StaffName);
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;
                case 'n':
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;
                case 'N':
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;
                default:
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;
            }
            return true;
        }
        public static void Update(Account a, Order o)
        {
            OrderBL obl = new OrderBL();
            Console.Write("input id table :");
            int table_id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input Id Order for edit: ");
            int orderid = Convert.ToInt32(Console.ReadLine());
            // obl.DeleteOrder(orderid);
            // obl.UpdateTable(table_id);

            obl.UpdateOrder(orderid, table_id);

            AddUD(table_id, a, o);

        }

        public static void DisplayOrder()
        {
            Console.WriteLine("================================================================================================");
            Console.WriteLine("|===========================                ORDERS                ==============================|");
            Console.WriteLine("================================================================================================");
            Console.WriteLine("| Order ID | Order Table | Account ID | Item ID | Item Price | Quantity |       Date Order      |");
            foreach (var i in obl.display())
            {

                
                Console.WriteLine("|{0,6}    |{1,8}     |  {2,6}    |{3,6}   |  {4,6}    |{5,6}    | {6,15}  |", i.OrderId, i.OrderTable.Table_Id, i.OrderAccount.Account_Id, i.OrderItem.ItemId, i.OrderItem.ItemPrice, i.OrderItem.Amount, i.OrderDate);
               
            }
             Console.WriteLine("================================================================================================");
            Console.Write("Press any key to back the menu: ");
            Console.ReadKey();
        }
        public static void Order(Account a)
        {
            short imChoose1;

            Console.Clear();

            string[] order = { "Create Order", "Edit Order", "Show list Order", "Exit" };
            do
            {
                imChoose1 = Menu("Order Management", order);
                switch (imChoose1)
                {
                    case 1:
                        Add(a, o);
                        break;
                    case 2:
                        Update(a, o);
                        break;
                    case 3:
                        DisplayOrder();
                        break;
                }
            } while (imChoose1 != order.Length);
        }

        public static short Menu(string title, string[] menuItems)
        {
            short choose = 0;
            string line = "\n========================================";
            Console.WriteLine(line);
            Console.WriteLine(" " + title);
            Console.WriteLine(line);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + ". " + menuItems[i]);
            }
            Console.WriteLine(line);
            do
            {
                Console.Write("Your choice: ");
                try
                {
                    choose = Int16.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Your Choose is wrong!");
                    continue;
                }
            } while (choose <= 0 || choose > menuItems.Length);
            return choose;
        }
        public static bool AddUD(int tableid, Account a, Order o)
        {

            o.OrderTable = new Table();
            o.OrderAccount = new Account();

            int table_id = tableid;

            o.OrderTable.Table_Id = table_id;
            o.OrderAccount.Account_Id = a.Account_Id;



            Console.WriteLine("|| item id  ||      Item name     ||     item price     ||");
            foreach (var item in itbl.display())
            {
                Console.WriteLine("||{0,10}||{1,20}||{2,20}||", item.ItemId, item.ItemName, item.ItemPrice);
            }

            while (true)
            {
                Console.WriteLine(" Input Item Id: ");
                int itemid;
                while (!int.TryParse(Console.ReadLine(), out itemid) && itemid < 100 && itemid > 0)
                {
                    Console.WriteLine("Invalid entry. Please enter a number.");
                }

                Console.WriteLine("Input quantity item: ");
                int quantity = Convert.ToInt32(Console.ReadLine());
                while (quantity <= 0 || quantity > 100)
                {
                    Console.WriteLine("Dont try to test my system :)!!");
                    Console.Write("Pre-enter: ");
                    try
                    {
                        quantity = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        // Console.WriteLine("error"+ e);
                        // Console.Clear();
                        Console.Write("");
                    }
                    //  obl.AddItemToOrder(itemid, quantity, o);
                }
                obl.AddItemToOrder(itemid, quantity, o);
                //it = itbl.GetItemById(itemid);
                Console.WriteLine("Do you want to continue Add Item? ");
                string choice2 = Console.ReadLine();
                if (choice2 == "Y" || choice2 == "y")
                {
                    continue;
                }
                else if (choice2 == "N" || choice2 == "n")
                {
                    break;
                }
            }
            Console.WriteLine("Do you want to create order: ");
            char choice3 = Convert.ToChar(Console.ReadLine());
            switch (choice3)
            {
                case 'y':
                    Console.WriteLine("Create Order: " + (obl.CreateOrder(o) ? "completed!" : "not complete!"));
                    o.ItemsList.Clear();
                    Console.WriteLine("Order ID: " + o.OrderId);

                    Console.WriteLine("Order Date: " + o.OrderDate);

                    foreach (var item in o.ItemsList)
                    {
                        Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");
                    }
                    Console.WriteLine("Order By: " + a.StaffName);
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;

                case 'Y':
                    Console.WriteLine("Create Order: " + (obl.CreateOrder(o) ? "completed!" : "not complete!"));
                    foreach (var item in o.ItemsList)
                    {
                        Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");
                    }
                    Console.WriteLine("Order By: " + a.StaffName);
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;
                case 'n':
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;
                case 'N':
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;
                default:
                    Console.Write("Press any key to back the menu: ");
                    Console.ReadKey();
                    break;
            }
            return true;


        }
    }
}