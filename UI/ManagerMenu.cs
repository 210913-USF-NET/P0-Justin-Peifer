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
        //private StoreFrontService _storeFrontService;

        public ManagerMenu(IBL bl)
        {
            _bl = bl;
            // _storeFrontService = storeFrontService;
        }
        public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("Welcome to the Beelicious Manager Menu!");
                Console.WriteLine("Press [1] to view and edit customers data.");
                Console.WriteLine("Press [2] to view order details and place new orders.");
                Console.WriteLine("Press [3] to view and edit individual store information.");
                Console.WriteLine("Press [4] to view and edit inventory data.");
                System.Console.WriteLine("Press [5] to view a list of all stores.");
                Console.WriteLine("Press[x] to leave.");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Edit this to redirect to customers menu");
                        break;

                    case "2":
                        Console.WriteLine("Edit this to redirect to orders menu");
                        break;
                    case "3":
                        //new FranchiseMenu(new BL(new StoreFrontRepo())).Start();
                        break;
                    case "4":
                        Console.WriteLine("Edit this to redirect to inventory menu");
                        break;
                    case "5":
                        ViewAllStoreFronts();
                        break;

                    case "x":
                        Console.WriteLine("Goodbye!");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("I don't know what you're talking about! :(");
                        break;
                }
            } while (!exit);
        }
        private void ViewAllStoreFronts()
        {
            List<StoreFront> allStores = _bl.GetAllStoreFronts();
            if(allStores.Count == 0)
            {
                Console.WriteLine("There is no restaurant :/");
            }
            else
            {
                foreach (StoreFront store in allStores)
                {
                    Console.WriteLine(store.ToString());
                }
            }
        }
    }
}