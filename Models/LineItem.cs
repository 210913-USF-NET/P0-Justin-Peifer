namespace Models
{
    public class LineItem
    //pretty much the same as inventory, but used for orders instead
    {
        public Product Item {get;set;}

        public int Quantities {get; set;}
    }
}