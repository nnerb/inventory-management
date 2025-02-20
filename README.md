# Inventory Management System with C#

## 📌 Overview

The **Inventory Management System** is a console-based application that allows users to manage product inventory efficiently. It provides features for adding, removing, updating, and listing products, along with calculating the total inventory value.

## 🎯 Features

- ✅ **Add Products**: Add new products with a unique ID, name, quantity, and price.
- ✅ **Remove Products**: Delete products from inventory by ID.
- ✅ **Update Stock**: Modify the stock quantity of a product.
- ✅ **View Product List**: Display all products in a formatted table.
- ✅ **Calculate Total Inventory Value**: Get the total monetary value of all inventory items.
- ✅ **User Input Validation**: Ensures proper input and allows users to cancel operations at any step.

## 🏗️ Technologies Used

- **C#** (Console Application)
- **.NET**

## 🚀 Installation & Usage

1. **Clone the repository**:
   ```sh
   git clone https://github.com/your-repo/inventory-management.git
   cd inventory-management
   ```
2. **Build and Run**:
   - Open the project in **Visual Studio** or **VS Code**
   - Run the application using:
     ```sh
     dotnet run
     ```

## 📖 How to Use

1. Start the program and select an option from the menu.
2. Follow the prompts to enter product details (Name, Quantity, Price, etc.).
3. Use the `cancel` command at any prompt to return to the main menu.
4. View the product list or check the total inventory value as needed.
5. Exit the program when done.

## 📌 Example Menu

```
=================================
🏪 Inventory Management System
=================================
1. Add Product
2. Remove Product
3. Update Product
4. Product List
5. Get Total Inventory Value
6. Exit
Choose an option:
```

## 🛠️ Code Structure

- `Product` Class: Represents an item in the inventory.
- `InventoryManager` Class: Handles inventory operations like add, remove, update, and list.
- `Program` Class: Main entry point for the console application.

## 📜 License

This project is licensed under the **MIT License**.

## 👥 Contributors

- [Brenn Santiago](https://github.com/nnerb)

## 📬 Contact

For any issues or feature requests, please open an **issue** on the repository.

---

⚡ *Built for efficient inventory management*
