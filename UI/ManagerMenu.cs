using System;
using Models;
using SBL;
using DL;
using System.Collections.Generic;

namespace UI
{
    public class ManagerMenu : IMenu
    {
        private IBL _bl;

        public ManagerMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("Welcome to the Beelicious Manager Menu!");
                Console.WriteLine("Press [1] to view and edit customers data.");
                Console.WriteLine("Press [2] to view and edit store information.");
                Console.WriteLine("Press [x] to leave.");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        MenuFactory.GetMenu("customermanagement").Start();
                        break;
                        
                    case "2":
                        MenuFactory.GetMenu("inventory").Start();
                        break;

                    case "x":
                        Console.WriteLine("Goodbye!");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid input, please try again.");
                        break;
                }
            } while (!exit);
        }
        
    }
}