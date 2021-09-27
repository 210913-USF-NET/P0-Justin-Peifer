namespace Models
{
    public class LineItem
    {
        public int OrderId { get; set; }
        public int? StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual StoreFront Store { get; set; }

        public LineItem() {}

        public LineItem (int OrderId, int StoreId, int ProductId, int Quantity){
            this.OrderId = OrderId;
            this.StoreId = StoreId;
            this.ProductId = ProductId;
            this.Quantity = Quantity;
        }
    }
}