using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBL;
using Models;
using System.Globalization;
using System.Threading;


namespace UI
{
    public class OrderViewer : IMenu
    {
        private BL _bl;

        public OrderViewer(BL bl)
        {
            _bl = bl;
        }
        public void Start(){
            OrderByUserId(MenuFactory.currentUser.Id);
            input:
            System.Console.WriteLine("Press [1] to start a new order, or [2] to log out.");
            string input = System.Console.ReadLine();
            if (input=="1"){
                MenuFactory.GetMenu("order").Start();
            }
            else if (input != "2"){
                System.Console.WriteLine("Invalid input, please try again");
                goto input;
            }

        }
        public void OrderByUserId(int Id){

            List <Order> pastOrders= _bl.OrderByUserId(Id);
            if (pastOrders.Count==0){
                System.Console.WriteLine("You haven't ordered from us yet!");
                return;
            }
            searchstart:
            System.Console.WriteLine("Press [0] to sort by newest orders to oldest orders.");
            System.Console.WriteLine("Press [1] to sort by oldest orders to newest orders.");
            string input = System.Console.ReadLine();
            switch (input){
                case "0":
                    
                    for(int x=0; x<pastOrders.Count; x++){
                        pastOrders[x] = _bl.OrderInfoById(pastOrders[x].Id);
                        int? totalQuantity=0;
                        foreach(LineItem item in pastOrders[x].LineItems){
                            totalQuantity+=item.Quantity;
                        }
                        if (totalQuantity!=0){
                            System.Console.WriteLine($"On {Convert.ToDateTime(pastOrders[x].DateOrdered).ToShortDateString()}, you bought {totalQuantity} items.");
                        }
                    }
                    break;
                case "1":

                    for(int x=pastOrders.Count-1; x>=0; x--){
                        pastOrders[x] = _bl.OrderInfoById(pastOrders[x].Id);
                        int? totalQuantity=0;
                        foreach(LineItem item in pastOrders[x].LineItems){
                            totalQuantity+=item.Quantity;
                        }
                        if (totalQuantity!=0){
                            System.Console.WriteLine($"On {Convert.ToDateTime(pastOrders[x].DateOrdered).ToShortDateString()}, you bought {totalQuantity} items.");
                        }
                    }
                    break;

                default:
                    System.Console.WriteLine("Invalid input, please try again.");
                    goto searchstart;
                }

        }
                
    }
}