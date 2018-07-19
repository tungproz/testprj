using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_BL;
using MPC_DAL;
using System.Text;

namespace PL_Console
{
    class PayoutConsole
    {
        public static bool Payout()
        {
            Order o = new Order();
            OrderBL obl = new OrderBL();
            int tableid;
            while (true)
            {
                Console.Write("Input Id Table for pay: ");
                tableid = Convert.ToInt32(Console.ReadLine());
                
                if (obl.CheckOrderTable(tableid) == true)
                {
                    List<Order> listorder = obl.GetAllListOrder();
                    o = obl.GetAllOrder();
                    Console.WriteLine("||===============================================||");
                    Console.WriteLine("||================= Bill List Infor =============||");
                    Console.WriteLine("||===============================================||");
                    Console.WriteLine("||Order ID||Item ID||Item Price||Quantity||Total ||");
                    Console.WriteLine("||===============================================||");
                    foreach (var order in listorder)
                    {
                        Console.WriteLine("||{0,-8}||{1,-7}||{2,-10}||{3,-8}||{4,-5}||", order.OrderId, order.OrderItem.ItemId, order.OrderItem.ItemPrice + "0", order.OrderItem.Amount, order.total + "0");
                    }
                    decimal totalmoney = obl.Total(listorder);
                    decimal fax = totalmoney / 10;
                    decimal grandtotal = totalmoney + fax;
                    Console.WriteLine("||Total Money: " + totalmoney + "0" + "VND" + "   Fax: " + fax + "0" + "VND");
                    Console.WriteLine("||Grand total(VND): " + grandtotal + "0" + "VND");
                    Console.Write("||Input Money for pay: ");
                    decimal Money = Convert.ToDecimal(Console.ReadLine());
                    while (true)
                    {
                        if (Money == grandtotal)
                        {
                            Console.WriteLine("Pay Order: " + (obl.PayOrder(o) ? "successfully!" : "not successfully!"));
                            break;
                        }
                        else if (Money < grandtotal)
                        {
                            decimal moneyshortage = grandtotal - Money;
                            Console.WriteLine("moneyshortage : " + moneyshortage + "0" + "VND");
                            Console.WriteLine("Input money shortage to complete order! : ");
                            decimal a = Convert.ToDecimal(Console.ReadLine());
                            if (a == moneyshortage)
                            {
                                Console.WriteLine("Pay Order: " + (obl.PayOrder(o) ? "successfully!" : "not successfully!"));
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (Money > grandtotal)
                        {
                            decimal excesscash = Money - grandtotal;
                            Console.WriteLine("Excess Cash: " + excesscash + "0" + "VND");
                            Console.WriteLine("Pay Order: " + (obl.PayOrder(o) ? "successfully!" : "not successfully!"));
                            break;

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Not Found this order to pay!!!");
                    continue;
                }
            }










            Console.WriteLine("||===============================================||");
            Console.WriteLine("Press any key to back the menu...");
            Console.ReadKey();
            return true;
        }
    }
}