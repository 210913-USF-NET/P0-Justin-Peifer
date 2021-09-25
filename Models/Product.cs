using System.Collections.Generic;

namespace Models
{
    
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? Price { get; set; }

        public bool? Alcohol { get; set; }
//if Alcohol = true, check for age requirement (21 or older to buy)

        public List<Inventory> Inventory { get; set; }
        //although there probably is a quicker way, when looking at a product this will be able to see which other stores may have it in stock.

        public override string ToString()
        {
            return $"Name: {this.Name}, \nDescription: {this.Description} \nPrice: {this.Price}";
        }
    }
}