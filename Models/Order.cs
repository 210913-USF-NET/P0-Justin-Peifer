using System;
using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public Order() {}

        public Order (int Id, List<LineItem> LineItems){
            this.UserId = Id;
            this.DateOrdered =DateTime.Now;
            this.LineItems = LineItems;
        }
        public int Id { get; set; }
        //LineItem will use the OrderID , and then you will get OrderedItems by looking up all LineItems with the OrderId

        public int? UserId { get; set; }

        public DateTime? DateOrdered { get; set; }

        public List<LineItem> LineItems {get; set; }
    }
}