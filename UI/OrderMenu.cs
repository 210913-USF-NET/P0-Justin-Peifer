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

            //just for testing
            Order currentOrder = _bl.NewOrder(currentUser.Id);
            System.Console.WriteLine($"Your order number is {currentOrder.Id}");

            List<Models.StoreFront> allStores = _bl.GetAllStoreFronts();
            Models.StoreFront chosenStore = new StoreFrontService().SelectAStoreFront("Please select your store location.", allStores);
            Console.WriteLine($"You've selected our Beelicious location in {chosenStore.State}");
            chosenStore = _bl.StoreById(chosenStore.Id);
            Console.WriteLine($"You've selected {chosenStore.State}");
            List<LineItem> shoppingCart= new List<LineItem>();
            bool shopping = true;
            while(shopping){
                shoppingCart.Add(SelectLineItem(chosenStore.Inventory));
                Console.WriteLine("Would you like to keep shopping? [Y/N]");
                string input = Console.ReadLine().ToLower();

            }
            // Models.Product chosenProduct =new StoreFrontService().SelectAProduct("Select a product to add to your cart.", allProducts);
            
        }
        public LineItem SelectLineItem(List<Inventory> storeInventory){
            itemselect:
            System.Console.WriteLine("Here's what we have in stock:");
            
            for (int i = 0; i < storeInventory.Count; i++)
            {
                Product productInfo = _bl.ProductByID(storeInventory[i].ProductId.GetValueOrDefault());
                Console.WriteLine($"[{i}] {productInfo.Name}");
            }
            
            string input = Console.ReadLine();
            int parsedInput;

            //pass by reference in, out, ref
            bool parseSuccess = Int32.TryParse(input, out parsedInput);
            if(parseSuccess && parsedInput >= 0 && parsedInput < storeInventory.Count)
            {   
                Product chosenProduct = _bl.ProductByID(storeInventory[parsedInput].ProductId.GetValueOrDefault());
                System.Console.WriteLine(chosenProduct.Description);
            
                System.Console.WriteLine($"Would you like to buy this item? We have {storeInventory[parsedInput].Quantity} in stock. (Y/N)");

                input = System.Console.ReadLine();


                if (input.ToLower() == "y"){
                    quantitychoice:
                    System.Console.WriteLine("How many would you like to buy?");
                    string quantityChosen = Console.ReadLine();
                    int parsedQuantity;
                    bool secondParse = Int32.TryParse(input, out parsedQuantity);
                    if(secondParse && parsedQuantity >= 0 && parsedQuantity <= storeInventory[parsedInput].Quantity)
                    {
                        LineItem addedItem = new LineItem(storeInventory[parsedInput].StoreId.Value, chosenProduct.Id, parsedQuantity);
                        return addedItem;
                    }
                    else{
                        System.Console.WriteLine($"Invalid input. We have {storeInventory[parsedInput].Quantity} in stock.");
                        goto quantitychoice;
                    }
                
                }
                else {
                    goto itemselect;
                    }
            }
            else {
                Console.WriteLine("invalid input");
                goto itemselect;
            }
        }
    }
}