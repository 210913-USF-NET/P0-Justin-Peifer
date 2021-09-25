using System;
using Models;
using SBL;
using DL;
using System.Collections.Generic;

namespace UI
{
    public class OrderMenu : IMenu

    {
        public User currentUser = MenuFactory.currentUser;
        private BL _bl;

        public OrderMenu(BL bl)
        {
            _bl = bl;
        }
        public void Start(){
            System.Console.WriteLine("Welcome to the ordering menu!");
            List<Models.StoreFront> allStores = _bl.GetAllStoreFronts();
            Models.StoreFront chosenStore = new StoreFrontService().SelectAStoreFront("Please select your store location.", allStores);
            Console.WriteLine($"You've selected our Beelicious location in {chosenStore.State}");
            chosenStore = _bl.StoreById(chosenStore.Id);
            Console.WriteLine($"You've selected{chosenStore.State}");

            bool shopping = true;
            while(shopping){
                SelectLineItem(chosenStore.Inventory);
                Console.WriteLine("Would you like to keep shopping? [Y/N]");
                string input = Console.ReadLine().ToLower();

            }

            // Models.Product chosenProduct =new StoreFrontService().SelectAProduct("Select a product to add to your cart.", allProducts);
            
        }
        public LineItem SelectLineItem(List<Inventory> storeInventory){
            System.Console.WriteLine("Here's what we have in stock:");
            foreach (Inventory item in storeInventory){
                Product productInfo = _bl.ProductByID(item.ProductId.GetValueOrDefault());
                System.Console.WriteLine(productInfo.ToString());
            }
            return null;

        }
    }
}