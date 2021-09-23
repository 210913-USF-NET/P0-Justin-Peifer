using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Order
    {
        public Order()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateOrdered { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
