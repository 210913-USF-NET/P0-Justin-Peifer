using System;
using Models;
using SBL;
using DL;
using System.Collections.Generic;

namespace UI
{
    public class FranchiseMenu : IMenu
    {
        private IBL _bl;
        //private StoreFrontService _storeFrontService;

        public FranchiseMenu(IBL bl)
        {
            _bl = bl;
            // _storeFrontService = storeFrontService;
        }

        public void Start(){
            bool exit = false;
            do
            {
                Console.WriteLine("This is Franchise menu");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[0] Add a new store.");
                Console.WriteLine("[1] View/edit store inventories");
                Console.WriteLine("[x] Go Back To Main Menu");

                switch (Console.ReadLine())
                {
                    case "0":
                        //_bl.CreateStoreFront();
                        break;
                    case "1":
                        MenuFactory.GetMenu("inventory").Start();
                        break;
                    case "x":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            } while (!exit);
        }
    }
}