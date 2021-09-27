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

            System.Console.WriteLine("Here are your past orders.");
            OrderByUserId(MenuFactory.currentUser.Id);

        }
        public void OrderByUserId(int Id){
            List <Order> pastOrders= _bl.OrderByUserId(Id);
            Order orderInfo;
            for(int x=0; x<pastOrders.Count; x++){
            
                pastOrders[x] = _bl.OrderInfoById(pastOrders[x].Id);
                int? totalQuantity=0;
                foreach(LineItem item in pastOrders[x].LineItems){
                    totalQuantity+=item.Quantity;
                }
                if (totalQuantity>0){

                System.Console.WriteLine($"On {Convert.ToDateTime(pastOrders[x].DateOrdered).ToShortDateString()}, you bought {totalQuantity} items.");
                }
            }

        }
    }
}