using System;
using Models;
using SBL;
using DL;
using System.Collections.Generic;
using Serilog;

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
            System.Console.WriteLine($"Your order number is {currentOrder.Id}.");

            List<StoreFront> allStores = _bl.GetAllStoreFronts();
            StoreFront chosenStore = new StoreFrontService().SelectAStoreFront("Please select your store location.", allStores);
            Console.WriteLine($"You've selected our Beelicious location in {chosenStore.State}");
            chosenStore = _bl.StoreById(chosenStore.Id);
            
            Console.WriteLine($"You've selected {chosenStore.State}");
            List<LineItem> shoppingCart= new List<LineItem>();
            bool shopping = true;
            while(shopping){
                shoppingCart.Add(SelectLineItem(chosenStore.Inventory, currentOrder.Id));
                Console.WriteLine("Would you like to keep shopping? [Y/N]");
                string keepShopping = Console.ReadLine().ToLower();
                if (keepShopping == "n")
                {
                    currentOrder.LineItems = shoppingCart;
                    shopping=false
                    ;}
            }

            System.Console.WriteLine("Here are the Items you have:");
            int totalPrice = 0;
            foreach (LineItem item in currentOrder.LineItems)
            {
                Product productInfo = _bl.ProductByID(item.ProductId);
                Console.WriteLine($"Name: {productInfo.Name}\nPrice: {productInfo.Price}\nQuantity: {item.Quantity}\n");
                totalPrice= totalPrice + (productInfo.Price.GetValueOrDefault())*item.Quantity.GetValueOrDefault();
            }
            System.Console.WriteLine($"For a total of {totalPrice} dollars.");
            System.Console.WriteLine("Is this your final order?[Y/N]");
            string input = System.Console.ReadLine();
            if (input.ToLower() =="y"){

                _bl.PlaceOrder(chosenStore, currentOrder);
                foreach (LineItem item in currentOrder.LineItems)
                {
                _bl.UpdateStock(chosenStore, item);
                }
                Log.Information($"An order was successfully placed by {currentUser.Name}!");
            }
        }



        public LineItem SelectLineItem(List<Inventory> storeInventory, int orderId){
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
                    bool secondParse = Int32.TryParse(quantityChosen, out parsedQuantity);
                    System.Console.WriteLine($"You entered {parsedQuantity}");
                    int storeID = storeInventory[parsedInput].StoreId.GetValueOrDefault();
                    if(secondParse && parsedQuantity >= 0 && parsedQuantity <= storeInventory[parsedInput].Quantity)
                    {
                        LineItem addedItem = new LineItem(orderId, storeID, chosenProduct.Id, parsedQuantity);
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