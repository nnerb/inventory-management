// Represents a product in the inventory
class Product
{
    public int ProductId { get; private set; } // Unique product identifier
    public string Name { get; set; } // Product name
    public int QuantityInStock { get; private set; } // Current stock quantity
    public double Price { get; set; } // Price per unit

    // Constructor to initialize a new product
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
    // Updates the stock quantity
    public void UpdateStock(int newQuantity)
    {
        if (newQuantity < 0) throw new ArgumentException("Quantity cannot be negative.");
        QuantityInStock = newQuantity;
    }
}
// Manages inventory operations
class InventoryManager
{
    private List<Product> products = new List<Product>(); // List to store products
    private int nextProductId = 1; // Auto-increment product ID

    // Finds a product by ID
    public Product? FindProduct(int productId)
    {
        return products.FirstOrDefault(p => p.ProductId == productId);
    }

    // Adds a new product to the inventory
    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"✅ Product '{product.Name}' added successfully with ID: {product.ProductId}.");
    }
    
    // Generates the next product ID
    public int FindNextProductId()
    {
        return nextProductId++;
    }

    // Removes a product from the inventory
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

    // Updates the stock of a product
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

    // Lists all products in the inventory
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

        // Header of the table
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

    // Calculates the total value of the inventory
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

// Main program entry point
class Program
{ 
    static void Main()
    {
        // Set console encoding to UTF-8 for Unicode support such as Philippine peso
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        InventoryManager inventory = new InventoryManager();

        while (true)
        {
            // Display Menu
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

            // Validate user input
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("❌ Invalid option");
                continue;
            }

            switch (option)
            {
                case 1:
                    ShowCancelPrompt(26);
                    string? name = PromptForProductName("Enter Product Name: ");
                    if (name == null) break;
                    int quantity = PromptForInteger("Enter Quantity: ");
                    if (quantity == -1) break;
                    double price = PromptForDouble("Enter Price: ");
                    if (price == -1) break;
                    Product newProduct = new Product(inventory.FindNextProductId(), name, quantity, price);
                    inventory.AddProduct(newProduct);
                    break;

                case 2:
                    ShowCancelPrompt(26);
                    int id = PromptForInteger("Enter the Product ID: ");
                    if (id == -1) break;
                    inventory.RemoveProduct(id);
                    break;
                        
                case 3:
                    ShowCancelPrompt(26);
                    int idToUpdate = PromptForInteger("Enter the Product ID: ");
                    if (idToUpdate == -1) break;
                    int newQuantity = PromptForInteger("Enter the new quantity: ");
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

        // Utilility methods for user input handling and validation
        string? PromptForProductName(string prompt)
        {
            while (true)
            {
                string? input = GetInputOrCancel(prompt);
                if (input == null) return null;
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("❌ Product name cannot be empty.");
            }
        }
        int PromptForInteger(string prompt) 
        {
            while (true)
            {
                string? input = GetInputOrCancel(prompt);
                if (input == null) return -1;
                if (int.TryParse(input, out int value) && value >= 0)
                    return value;
                Console.WriteLine("❌ Invalid input. Please try again.");
            }
        }
        double PromptForDouble(string prompt)
        {
            while (true)
            {
                string? input = GetInputOrCancel(prompt); 
                if (input == null) return -1;
                if (double.TryParse(input, out double value) && value >= 0)
                    return value == -0.0 ? 0.0 : value;
                Console.WriteLine("❌ Invalid input. Please try again.");
            }
        }

        // Utility method to get input or cancel operation
        string? GetInputOrCancel(string prompt)
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

        // Utility method to show cancel prompt
        void ShowCancelPrompt(int width)
        {
            string separator = new string('=', width);
            Console.WriteLine(separator);
            Console.WriteLine("Type 'cancel' to abort 🚫");
            Console.WriteLine(separator);
        }
    }
}
