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
class InventoryManager
{
    private List<Product> products = new List<Product>();
    private int productId = 1;

    public Product? FindProduct(int productId)
    {
        return products.Find(p => p.ProductId == productId);
    }

    public void AddProduct(string name, int quantity, double price)
    {
        Product product = new Product(productId++, name, quantity, price);
        products.Add(product);
    }

    public void RemoveProduct(int productId)
    {
        Product? product = FindProduct(productId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }
        products.Remove(product);
    }

}

class Program
{ 
    static void Main()
    {
        InventoryManager inventory = new InventoryManager();
        
        while (true)
        {
            Console.WriteLine("\nInventory Management System in C#");
            Console.WriteLine("1. Add product");
            Console.WriteLine("2. Remove product");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option");
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter Product Name: ");
                    string name = Console.ReadLine() ?? "Unknown product";
                    Console.WriteLine("Enter Quantity: ");
                    int quantity = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("Enter Price: ");
                    double price = double.Parse(Console.ReadLine() ?? "0");
                    inventory.AddProduct(name, quantity, price);
                    break;

                case 2:
                    Console.WriteLine("Enter the Product ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid Product ID. Please enter a number.");
                        continue; 
                    }
                    inventory.RemoveProduct(id);
                    break;
                    
            }

        }


    }
}
