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
        Console.WriteLine($"✅ Product '{product.Name}' added successfully with ID: {product.ProductId}.");
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
            Console.WriteLine($"❌ Product with {productId} not found.");
            return;
        }
        products.Remove(product);
        Console.WriteLine($"✅ {product.Name} removed.");
    }

    public void UpdateProduct(int productId, int newQuantity)
    {
        Product? productToUpdate = FindProduct(productId);
        if (productToUpdate == null)
        {
            Console.WriteLine($"❌ Product with {productId} not found.");
            return;
        }
        productToUpdate.UpdateStock(newQuantity);
        Console.WriteLine($"✅ Updated {productToUpdate.Name}, quantity: {newQuantity}.");
    }

    public void ListProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("❌ Inventory is empty.");
            return;
        }

        // Calculate dynamic column widths
        int idWidth = Math.Max("ID".Length, products.Max(p => p.ProductId.ToString().Length)) + 2;
        int nameWidth = Math.Max("Name".Length, products.Max(p => p.Name.Length)) + 2;
        int quantityWidth = Math.Max("Quantity".Length, products.Max(p => p.QuantityInStock.ToString().Length)) + 2;
        int priceWidth = Math.Max("Price".Length, products.Max(p => p.Price.ToString("C").Length)) + 2;

        int totalWidth = idWidth + nameWidth + quantityWidth + priceWidth + 5; // Add spacing and separators

        Console.WriteLine("\n📦 Inventory List:");
        Console.WriteLine(new string('-', totalWidth));

        // Header
        Console.WriteLine(
            $"{"ID".PadRight(idWidth)}" +
            $"{"Name".PadRight(nameWidth)}" +
            $"{"Quantity".PadRight(quantityWidth)}" +
            $"{"Price".PadLeft(priceWidth)}"
        );
        Console.WriteLine(new string('-', totalWidth));

        // Product rows
        foreach (var product in products)
        {
            Console.WriteLine(
                $"{product.ProductId.ToString().PadRight(idWidth)}" +
                $"{product.Name.PadRight(nameWidth)}" +
                $"{product.QuantityInStock.ToString().PadRight(quantityWidth)}" +
                $"{product.Price.ToString("C").PadLeft(priceWidth)}"
            );
        }

        Console.WriteLine(new string('-', totalWidth));
    }

    public double GetTotalValue()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("❌ Inventory is empty");
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
            Console.WriteLine(new string('=', 31));
            Console.WriteLine("🏪 Inventory Management System");
            Console.WriteLine(new string('=', 31));
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Product List");
            Console.WriteLine("5. Get Total Inventory Value");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("❌ Invalid option");
                continue;
            }

            switch (option)
            {
                case 1:
                    ShowCancelPrompt(26);
                    string? name = ReadProductName("Enter Product Name: ");
                    if (name == null) break;
                    int quantity = ReadPositiveInt("Enter Quantity: ");
                    if (quantity == -1) break;
                    double price = ReadPositiveDouble("Enter Price: ");
                    if (price == -1) break;
                    Product newProduct = new Product(inventory.FindNextProductId(), name, quantity, price);
                    inventory.AddProduct(newProduct);
                    break;

                case 2:
                    ShowCancelPrompt(26);
                    int id = ReadPositiveInt("Enter the Product ID: ");
                    if (id == -1) break;
                    inventory.RemoveProduct(id);
                    break;
                        
                case 3:
                    ShowCancelPrompt(26);
                    int idToUpdate = ReadPositiveInt("Enter the Product ID: ");
                    if (idToUpdate == -1) break;
                    int newQuantity = ReadPositiveInt("Enter the new quantity: ");
                    if (newQuantity == -1) break;
                    inventory.UpdateProduct(idToUpdate, newQuantity);
                    break;

                case 4:
                    inventory.ListProducts();
                    break;

                case 5:
                    Console.WriteLine($"✅ Total Inventory Value: {inventory.GetTotalValue():C}");
                    break;

                case 6:
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("❌ Invalid option. Please choose again.");
                    break;
            }
        }

        string? ReadProductName(string prompt)
        {
            while (true)
            {
                string? input = ReadInputOrCancel(prompt);
                if (input == null) return null;
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("❌ Product name cannot be empty.");
            }
        }
        int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                string? input = ReadInputOrCancel(prompt);
                if (input == null) return -1;
                if (int.TryParse(input, out int value) && value >= 0)
                    return value;
                Console.WriteLine("❌ Invalid input. Please try again.");
            }
        }
        double ReadPositiveDouble(string prompt)
        {
            while (true)
            {
                string? input = ReadInputOrCancel(prompt); 
                if (input == null) return -1;
                if (double.TryParse(input, out double value) && value >= 0)
                    return value == -0.0 ? 0.0 : value;
                Console.WriteLine("❌ Invalid input. Please try again.");
            }
        }
        string? ReadInputOrCancel(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();

            if (input?.ToLower() == "cancel")
            {
                Console.WriteLine("🚫 Operation canceled. Returning to main menu.");
                return null;
            }
            return input;
        }
        void ShowCancelPrompt(int width)
        {
            string separator = new string('=', width);
            Console.WriteLine(separator);
            Console.WriteLine("Type 'cancel' to abort 🚫");
            Console.WriteLine(separator);
        }
    }
}
