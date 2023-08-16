namespace ProductsApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
