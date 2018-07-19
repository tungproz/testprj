﻿using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_BL;
using MPC_DAL;
using System.Text;

namespace PL_Console
{
	class Program
	{
		PayoutConsole pc = new PayoutConsole();
		OrderConsole oc = new OrderConsole();
		LoginConsole lc = new LoginConsole();
		static void Main(string[] args)
		{
			Console.Clear();
			
			MENU();
		}
		public static void MENU()
		{
			
			short mainChoose = 0;
			string[] login = { "Login", "Exit." };
			do
			{
				mainChoose = OrderConsole.Menu("Welcom to management cafe restaurent system !", login);
				switch (mainChoose)
				{

					case 1:
						LoginConsole.Login();
						// CafeManagementSystem();
						break;
				}
			} while (mainChoose != login.Length);

		}
		public static void CafeManagementSystem(Account a)
		{
			Console.Clear();
			
			short imChoose;
			string[] mainMenu = { "Order Management", "Payout ", "Exit" };
			do
			{
				imChoose = OrderConsole.Menu(" Cafe Management System ", mainMenu);
				switch (imChoose)
				{
					case 1:
						Console.Clear();
						OrderConsole.Order(a); 
						break;

					case 2:
					Console.Clear();
					
						PayoutConsole.Payout();
						break;
				}
			} while (imChoose != mainMenu.Length);
		}

	}
}
