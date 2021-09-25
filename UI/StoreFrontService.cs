using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace UI
{
    public class StoreFrontService
    {
        public StoreFront SelectAStoreFront(string prompt, List<StoreFront> listToPick)
        {
            selectStore:
            Console.WriteLine(prompt);
            for (int i = 0; i < listToPick.Count; i++)
            {
                Console.WriteLine($"[{i}] {listToPick[i]}");
            }
            string input = Console.ReadLine();
            int parsedInput;

            //pass by reference in, out, ref
            bool parseSuccess = Int32.TryParse(input, out parsedInput);

            //I'm checking to see that parse has been successful
            //and the result stays within the boundary of the index
            if(parseSuccess && parsedInput >= 0 && parsedInput < listToPick.Count)
            {
                return listToPick[parsedInput];
            }
            else {
                Console.WriteLine("invalid input");
                goto selectStore;
            }
        }

        public Product SelectAProduct(string prompt, List<Product> listToPick)
        {
            selectProduct:
            Console.WriteLine(prompt);
            for (int i = 0; i < listToPick.Count; i++)
            {
                Console.WriteLine($"[{i}] {listToPick[i]}");
            }
            string input = Console.ReadLine();
            int parsedInput;

            //pass by reference in, out, ref
            bool parseSuccess = Int32.TryParse(input, out parsedInput);

            //I'm checking to see that parse has been successful
            //and the result stays within the boundary of the index
            if(parseSuccess && parsedInput >= 0 && parsedInput < listToPick.Count)
            {
                return listToPick[parsedInput];
            }
            else {
                Console.WriteLine("invalid input");
                goto selectProduct;
            }
        }

        public List<Inventory> StoreInventory(List<Inventory> allInventory, int storeId){
            List<Inventory> storeInventory = null;
            foreach (Inventory stock in allInventory){
                if (stock.StoreId == storeId){
                    storeInventory.Add(stock);
                }
            }
            return storeInventory;
        }

        
    }
}