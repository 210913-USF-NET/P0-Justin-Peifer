﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class LineItem
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? StoreId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual StoreFront Store { get; set; }
    }
}
