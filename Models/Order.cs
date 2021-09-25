using System;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        //LineItem will use the OrderID , and then you will get OrderedItems by looking up all LineItems with the OrderId

        public int? UserId { get; set; }

        public DateTime? DateOrdered { get; set; }
    }
}