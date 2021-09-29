using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using SBL;

namespace UI
{
    public class CustomerManagementMenu : IMenu
    {
        private BL _bl;
        public CustomerManagementMenu(BL bl){
            _bl = bl;
        }
        public void Start(){
            System.Console.WriteLine("Welcome to the customer management menu.");
            searchstart:
            System.Console.WriteLine("Please enter the name of the customer you want to look at.");
            string customerSearch = System.Console.ReadLine();
            List<User> searchResults= _bl.SearchUser(customerSearch);
            if (searchResults.Count==0){
                System.Console.WriteLine("No results found, please try again");
                goto searchstart;
            }

            System.Console.WriteLine("Here are the results we found.");
            for (int x=0; x<searchResults.Count; x++){
                System.Console.WriteLine($"[{x}] Name: {searchResults[x].Name}\n     Email:{searchResults[x].Email} ");
            }
            System.Console.WriteLine("Please select a user.");
            string input = Console.ReadLine();
            int parsedInput;
            bool parseSuccess = Int32.TryParse(input, out parsedInput);
            if(!parseSuccess || parsedInput !>= 0 || parsedInput !<= searchResults.Count){}
            else {
                System.Console.WriteLine("Invalid input, please try again.");//gets an ArgumentOutOfRangeException if you put in a big number
                goto searchstart;}

            User chosenUser = searchResults[parsedInput];
            editingchoice:
            System.Console.WriteLine($"Press [0] to view a {chosenUser.Name}'s order history.");
            System.Console.WriteLine($"Press [1] to convert {chosenUser.Name}'s account into a management account.");
            input = System.Console.ReadLine();
            switch (input){
            case "0": 
                ManagementViewOrders(chosenUser);
                break;
                
            case "1": 
                System.Console.WriteLine(_bl.MakeUserManager(chosenUser));
                break;
            default: System.Console.WriteLine("Invalid input."); goto editingchoice;

            }


        }
        public void ManagementViewOrders(User user){
            List <Order> pastOrders= _bl.OrderByUserId(user.Id);
            if (pastOrders.Count()==0)
            {
                System.Console.WriteLine("This user hasn't ordered with us yet!");
            }
            for(int x=0; x<pastOrders.Count; x++){
            
                pastOrders[x] = _bl.OrderInfoById(pastOrders[x].Id);
                int? totalQuantity=0;
                foreach(LineItem item in pastOrders[x].LineItems){
                    totalQuantity+=item.Quantity;
                }
                if (totalQuantity>0){

                System.Console.WriteLine($"ID: {pastOrders[x].Id}, {Convert.ToDateTime(pastOrders[x].DateOrdered).ToShortDateString()}, {user.Name} bought {totalQuantity} items.");
                }
            }
        }
    }
}