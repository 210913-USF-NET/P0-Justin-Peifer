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
            reopenCart:
            while(shopping){
                
                LineItem itemToAdd = SelectLineItem(chosenStore.Inventory, currentOrder.Id);
                bool hasItem= false;
                foreach (LineItem item in shoppingCart){
                    if (item.ProductId == itemToAdd.ProductId){
                        item.Quantity= itemToAdd.Quantity;
                        hasItem=true;
                    }
                }
                if (hasItem ==false)
                {
                    shoppingCart.Add(itemToAdd);
                }
                shoppingDecision:
                Console.WriteLine("[1] Keep Shopping");
                Console.WriteLine("[2] Checkout");
                Console.WriteLine("[3] Exit");
                string keepShopping = Console.ReadLine().ToLower();
                switch(keepShopping){

                case "1": break;
                case "2":
                    currentOrder.LineItems = shoppingCart;
                    shopping=false;
                    break;
                case "3": 
                    System.Console.WriteLine("Thank you, we hope to see you soon!");
                    // _bl.ClearBadOrder(currentOrder.Id);
                    return;
                default:
                    System.Console.WriteLine("Invalid input, please select a number between 1 and 3.");
                    goto shoppingDecision;
                }
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
                
                _bl.UpdateStock(chosenStore, currentOrder.LineItems);
                
                System.Console.WriteLine($"Thank you for your order! We will be waiting for you at our Beelicious location in {chosenStore.State}!");
                
                Log.Information($"An order was successfully placed by {currentUser.Name}!");
                
            }
            else if (input.ToLower() =="n"){
                goto reopenCart;
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
            System.Console.WriteLine("Please select the number of the item you want.");
            
            string input = Console.ReadLine();
            
            int parsedInput;

            //pass by reference in, out, ref
            bool parseSuccess = Int32.TryParse(input, out parsedInput);
            if(parseSuccess && parsedInput >= 0 && parsedInput < storeInventory.Count)
            {   
                Product chosenProduct = _bl.ProductByID(storeInventory[parsedInput].ProductId.GetValueOrDefault());
                bool alcoholChecker = chosenProduct.Alcohol.HasValue ? chosenProduct.Alcohol.Value : false;//if an employee didn't put an alcohol label on an item, we will assume it is nonalcoholic.
                if(alcoholChecker && MenuFactory.currentUser.Age<21){
                    System.Console.WriteLine("Sorry, this product contains alcohol. Try some of our other honey products instead!");
                    goto itemselect;
                }
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
                    if(secondParse && parsedQuantity > 0 && parsedQuantity <= storeInventory[parsedInput].Quantity)
                    {
                        LineItem addedItem = new LineItem(orderId, storeID, chosenProduct.Id, parsedQuantity);
                        return addedItem;
                    }
                    else if (parsedQuantity<1){
                        System.Console.WriteLine("Quantity must be higher than 0. Redirecting to the main ordering section...");
                        goto itemselect;
                    }
                    else{
                        System.Console.WriteLine($"Invalid input. We have {storeInventory[parsedInput].Quantity} in stock.");
                        goto quantitychoice;
                    }
                
                }
                else {
                    System.Console.WriteLine("Invalid input, redirecting to the product selection page...");
                    goto itemselect;
                    }
            }
            else {
                Console.WriteLine("Invalid input.");
                goto itemselect;
            }
        }
    }
}