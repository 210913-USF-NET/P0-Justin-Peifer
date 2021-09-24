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
                Console.WriteLine("[0] Add new stores");
                Console.WriteLine("[1] View all stores");
                Console.WriteLine("[x] Go Back To Main Menu");

                switch (Console.ReadLine())
                {
                    case "0":
                        //CreateStoreFront();
                        break;
                    case "1":
                        ViewAllStoreFronts();
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

        // private void CreateStoreFront(){
        //     System.Console.WriteLine("Creating new store");
        //     System.Console.WriteLine("State: ");
        //     string state = Console.ReadLine();
        //     System.Console.WriteLine("Zipcode:");
        //     int zipcode = int.Parse(Console.ReadLine());

        //     StoreFront newStore = new StoreFront(state, zipcode);
        //     _bl.CreateStoreFront(newStore);
        //     Console.WriteLine($"You created {newStore.ToString()}");
        // }
        

        
        private void ViewAllStoreFronts()
        {
            List<StoreFront> allStores = _bl.GetAllStoreFronts();
            if(allStores.Count == 0)
            {
                Console.WriteLine("You haven't added any stores to the database yet.");
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