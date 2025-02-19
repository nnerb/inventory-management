class Product
{
    public int ProductId { get; private set; }
    public string Name { get; set; }
    public int QuantityStock { get; set; }
    public double Price { get; set; }

    public Product(int productId, string name, int quantity, double price)
    {
        if (quantity < 0) throw new ArgumentException("Invalid quantity");
        if (price < 0) throw new ArgumentException("Invalid price");

        ProductId = productId;
        Name = name;
        QuantityStock = quantity;
        Price = price;
    }

}

class Program
{ 
    static void Main()
    {
       
    }
}
