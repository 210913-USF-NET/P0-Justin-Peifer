using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBL;
using Models;

namespace UI
{
    public class InventoryMenu : IMenu
    {

        private BL _bl;
        public InventoryMenu(BL bl)
        {
            _bl = bl;
        }
        public void Start(){
            System.Console.WriteLine("Welcome to the inventory menu!");
            List<StoreFront> allStores = _bl.GetAllStoreFronts();
            StoreFront chosenStore= new StoreFrontService().SelectAStoreFront("Please select your store location.", allStores);
            chosenStore = _bl.StoreById(chosenStore.Id);
            EditInventory(chosenStore);
        }
        public void EditInventory(StoreFront storeToEdit){
            chooseProduct:
            System.Console.WriteLine($"Here's what the store in {storeToEdit.State} has in stock:");
            
            for (int i = 0; i < storeToEdit.Inventory.Count; i++)
            {
                Product productInfo = _bl.ProductByID(storeToEdit.Inventory[i].ProductId.GetValueOrDefault());
                Console.WriteLine($"[{i}] {productInfo.Name} ({storeToEdit.Inventory[i].Quantity} in stock).");
            }
            
            string input = Console.ReadLine();
            int parsedInput;
            bool parseSuccess = Int32.TryParse(input, out parsedInput);
            if(parseSuccess && parsedInput >= 0 && parsedInput < storeToEdit.Inventory.Count){
                inventoryedit:
                System.Console.WriteLine("How many items would you like to add to the stock? \n(Put negative numbers to remove stock)");
                input = System.Console.ReadLine();
                int quantityInput;
                parseSuccess = Int32.TryParse(input, out quantityInput);
                if(parseSuccess && parsedInput >= 0){
                    if (-quantityInput>storeToEdit.Inventory[parsedInput].Quantity){
                        System.Console.WriteLine("Invalid input, inventory cannot be negative!");
                        goto inventoryedit;
                    }
                    else {_bl.UpdateStock(storeToEdit.Inventory[parsedInput], quantityInput, storeToEdit.Id);}
                }
                else {
                    System.Console.WriteLine("Invalid input, please try again.");
                    goto inventoryedit;
                    }

            }
            else {
                Console.WriteLine("Invalid input, please try again.");
                goto chooseProduct;
            }

        }
    }
}