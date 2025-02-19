class Product
{
    public int ProductId { get; private set; }
    public string Name { get; set; }
    public int QuantityInStock { get; set; }
    public double Price { get; set; }

    public Product(int productId, string name, int quantity, double price)
    {
        if (quantity < 0) throw new ArgumentException("Invalid quantity");
        if (price < 0) throw new ArgumentException("Invalid price");

        ProductId = productId;
        Name = name;
        QuantityInStock = quantity;
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
        Product product = new Product(productId, name, quantity, price);
        products.Add(product);
        Console.WriteLine($"{product.Name} with ID {product.ProductId} added to inventory.");
        productId++;
    }

    public void RemoveProduct(int productId)
    {
        Product? product = FindProduct(productId);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }
        products.Remove(product);
        Console.WriteLine($"{product.Name} removed.");
    }

    public void UpdateProduct(int productId, int newQuantity)
    {
        Product? productToUpdate = FindProduct(productId);
        if (productToUpdate == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }
        productToUpdate.QuantityInStock = newQuantity;
        Console.WriteLine($"Updated {productToUpdate.Name}, quantity: {newQuantity}.");
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
            Console.WriteLine("3. Update Product");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option");
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.Write("Enter Product Name: ");
                    string name = Console.ReadLine() ?? "Unknown product";
                    Console.Write("Enter Quantity: ");
                    if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                        continue;
                    }
                    Console.Write("Enter Price: ");
                    if (!double.TryParse(Console.ReadLine(), out double price) || price < 0)
                    {
                        Console.WriteLine("Invalid price.");
                        continue;
                    }
                    inventory.AddProduct(name, quantity, price);
                    break;

                case 2:
                    Console.Write("Enter the Product ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid Product ID. Please enter a number.");
                        continue; 
                    }
                    inventory.RemoveProduct(id);
                    break;

                case 3:
                    Console.Write("Enter the Product ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int idToUpdate))
                    {
                        Console.WriteLine("Invalid Product ID. Please enter a number.");
                        continue;
                    }
                    Console.Write("Enter new Quantity: ");
                    if (!int.TryParse(Console.ReadLine(), out int newQuantity) || newQuantity < 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                        continue;
                    }
                    inventory.UpdateProduct(idToUpdate, newQuantity);
                    break;
            }
        }
    }
}
