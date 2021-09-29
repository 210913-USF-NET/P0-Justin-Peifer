using System.Collections.Generic;

namespace Models
{
    public class Inventory
    {
        
        public int Id { get; set; }
        public int? StoreId { get; set; }
        
        public int? ProductId { get; set; }
        private int? _quantity;

        public int? Quantity
                {
                    get
                    {
                        return _quantity;
                    } 
                    set
                    {
                        if (value<0)
                        {
                            throw new NegativeInventoryException("Inventory for a product cannot be negative.");
                        }
                        else{
                            _quantity = value;
                        }
                    }
                }
        public List<Product> Products { get; set; }

        public virtual Product Product { get; set; }
        public virtual StoreFront Store { get; set; }


        public Inventory() {}
        public Inventory(int storeId, int productId, int quantity){
            this.StoreId = storeId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        
    }

}