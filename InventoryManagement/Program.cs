class Product
{
    public int ProductId { get; private set; }
    public string Name { get; set; }
    public int QuantityInStock { get; private set; }
    public double Price { get; set; }

    public Product(int productId, string name, int quantity, double price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name cannot be empty.");
        if (quantity < 0) throw new ArgumentException("Invalid quantity");
        if (price < 0) throw new ArgumentException("Invalid price");

        ProductId = productId;
        Name = name;
        QuantityInStock = quantity;
        Price = price;
    }
    public void UpdateStock(int newQuantity)
    {
        if (newQuantity < 0) throw new ArgumentException("Quantity cannot be negative.");
        QuantityInStock = newQuantity;
    }
}
class InventoryManager
{
    private List<Product> products = new List<Product>();
    private int nextProductId = 1;

    public Product? FindProduct(int productId)
    {
        return products.FirstOrDefault(p => p.ProductId == productId);
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"Added {product.Name} with ID {product.ProductId} to inventory.");
    }

    public int FindNextProductId()
    {
        return nextProductId++;
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
            Console.WriteLine($"Product with {productId} not found.");
            return;
        }
        productToUpdate.UpdateStock(newQuantity);
        Console.WriteLine($"Updated {productToUpdate.Name}, quantity: {newQuantity}.");
    }

    public void ListProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("Inventory List:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.QuantityInStock}, Price: {product.Price:C}");
        }
    }

    public double GetTotalValue()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Inventory is empty");
            return 0.0;
        }
       return products.Sum(product => product.Price * product.QuantityInStock); 
    }
}

class Program
{ 
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        InventoryManager inventory = new InventoryManager();

        while (true)
        {
            Console.WriteLine("\nInventory Management System in C#");
            Console.WriteLine("1. Add product");
            Console.WriteLine("2. Remove product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Product List");
            Console.WriteLine("5. Get Total Inventory Value");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option");
                continue;
            }

            switch (option)
            {
                case 1:
                    string name;
                    while (true)
                    {
                        Console.Write("Enter Product Name: ");
                        name = (Console.ReadLine() ?? "Unknown Product").Trim();
                        if (!string.IsNullOrWhiteSpace(name))
                            break;
                        Console.WriteLine("Product name cannot be empty.");
                    }
                    int quantity = ReadPositiveInt("Enter Quantity: ");
                    double price = ReadPositiveDouble("Enter Price: "); 
                    Product newProduct = new Product(inventory.FindNextProductId(), name, quantity, price);
                    inventory.AddProduct(newProduct);
                    break;

                case 2:
                    int id = ReadPositiveInt("Enter the Product ID: ");
                    inventory.RemoveProduct(id);
                    break;
                        
                case 3:
                    int idToUpdate = ReadPositiveInt("Enter the Product ID: ");
                    int newQuantity = ReadPositiveInt("Enter the new quantity: ");
                    inventory.UpdateProduct(idToUpdate, newQuantity);
                    break;

                case 4:
                    inventory.ListProducts();
                    break;

                case 5:
                    Console.WriteLine($"Total Inventory Value: {inventory.GetTotalValue():C}");
                    break;

                case 6:
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
        int ReadPositiveInt (string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                    return value;

                Console.WriteLine($"Invalid input. Please try again.");
            }
        }
        double ReadPositiveDouble (string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && value >= 0)
                    return value == -0.0 ? 0.0 : value;

                Console.WriteLine($"Invalid input. Please try again.");
            }
        }
    }
}
