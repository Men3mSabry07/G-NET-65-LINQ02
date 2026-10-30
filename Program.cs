using System;
using System.Collections.Generic;
using System.Linq;

namespace G_NET_65_LINQ02
{
    class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalOrderValue { get; set; }
    }

    class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; private set; }

        public Product(int productId, string productName, string category, decimal price, int unitsInStock)
        {
            ProductID = productId;
            ProductName = productName;
            Category = category;
            Price = price;
            UnitsInStock = unitsInStock;
        }
    }

    class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
    }

    internal class Program
    {
        static void Print<T>(IEnumerable<T> list, Func<T, string> selector, string title = "")
        {
            Console.WriteLine($"{title}[{string.Join(", ", list.Select(x => selector(x)?.ToString()))}]");
        }

        static List<Product> ProductList = new List<Product>
        {
            new Product(1, "Chai", "Beverages", 18m, 39),
            new Product(2, "Chang", "Beverages", 19m, 17),
            new Product(3, "Coffee", "Beverages", 30m, 25),
            new Product(4, "Green Tea", "Beverages", 12m, 0),
            new Product(5, "Salmon", "Seafood", 250m, 25),
            new Product(6, "Shrimp", "Seafood", 320m, 18),
            new Product(7, "Tuna", "Seafood", 180m, 30),
            new Product(8, "Sardines", "Seafood", 90m, 50),
            new Product(9, "Squid", "Seafood", 210m, 0),
            new Product(10, "Crab", "Seafood", 28m, 12),
            new Product(11, "Chicken Breast", "Meat", 160m, 40),
            new Product(12, "Beef", "Meat", 350m, 15),
            new Product(13, "Cheddar Cheese", "Dairy", 120m, 22),
            new Product(14, "Milk", "Dairy", 45m, 60),
            new Product(15, "Yogurt", "Dairy", 15m, 0),
            new Product(16, "Mustard", "Condiments", 12m, 20),
            new Product(17, "Hot Sauce", "Condiments", 8m, 0),
            new Product(18, "Ketchup", "Condiments", 25m, 15),
            new Product(19, "Mayonnaise", "Condiments", 32m, 7),
            new Product(20, "Rice", "Grains", 70m, 100),
            new Product(21, "Wheat Flour", "Grains", 22m, 45),
            new Product(22, "Corn", "Grains", 9m, 0)
        };

        static List<Order> OrderList = new List<Order>
        {
            new Order { OrderID = 1, CustomerID = "ALFKI", OrderDate = new DateTime(1996, 5, 10), TotalOrderValue = 500 },
            new Order { OrderID = 2, CustomerID = "ANATR", OrderDate = new DateTime(1996, 11, 20), TotalOrderValue = 750 },
            new Order { OrderID = 3, CustomerID = "ANTON", OrderDate = new DateTime(1997, 1, 15), TotalOrderValue = 300 },
            new Order { OrderID = 4, CustomerID = "AROUT", OrderDate = new DateTime(1997, 3, 22), TotalOrderValue = 1200 },
            new Order { OrderID = 5, CustomerID = "BERGS", OrderDate = new DateTime(1997, 7, 8), TotalOrderValue = 950 },
            new Order { OrderID = 6, CustomerID = "BLAUS", OrderDate = new DateTime(1997, 12, 30), TotalOrderValue = 600 },
            new Order { OrderID = 7, CustomerID = "BONAP", OrderDate = new DateTime(1998, 2, 14), TotalOrderValue = 850 },
            new Order { OrderID = 8, CustomerID = "BOTTM", OrderDate = new DateTime(1998, 6, 5), TotalOrderValue = 400 },
            new Order { OrderID = 9, CustomerID = "BSBEV", OrderDate = new DateTime(1998, 9, 18), TotalOrderValue = 1100 },
            new Order { OrderID = 10, CustomerID = "CACTU", OrderDate = new DateTime(1999, 4, 25), TotalOrderValue = 700 },
            new Order { OrderID = 11, CustomerID = "CENTC", OrderDate = new DateTime(1996, 8, 12), TotalOrderValue = 550 },
            new Order { OrderID = 12, CustomerID = "CHOPS", OrderDate = new DateTime(2000, 1, 3), TotalOrderValue = 900 }
        };

        static List<Customer> CustomerList = new List<Customer>
        {
            new Customer { CustomerID = "ALFKI", CompanyName = "Alfreds Futterkiste", Country = "Germany" },
            new Customer { CustomerID = "ANATR", CompanyName = "Ana Trujillo", Country = "Mexico" },
            new Customer { CustomerID = "ANTON", CompanyName = "Antonio Moreno", Country = "Mexico" },
            new Customer { CustomerID = "AROUT", CompanyName = "Around the Horn", Country = "UK" },
            new Customer { CustomerID = "BERGS", CompanyName = "Berglunds", Country = "Sweden" },
            new Customer { CustomerID = "BLAUS", CompanyName = "Blauer See", Country = "Germany" },
            new Customer { CustomerID = "BONAP", CompanyName = "Bon app", Country = "France" },
            new Customer { CustomerID = "BOTTM", CompanyName = "Bottom-Dollar", Country = "Canada" },
            new Customer { CustomerID = "BSBEV", CompanyName = "B's Beverages", Country = "UK" },
            new Customer { CustomerID = "CACTU", CompanyName = "Cactus Comidas", Country = "Mexico" },
            new Customer { CustomerID = "CENTC", CompanyName = "Centro comercial", Country = "Spain" },
            new Customer { CustomerID = "CHOPS", CompanyName = "Chop-suey", Country = "Switzerland" }
        };

        static void Main(string[] args)
        {
            #region Q1 - Get Top 3 Most Expensive Products

            var res1 = ProductList.OrderByDescending(p => p.Price).Take(3);
            Print(res1, x => $"{x.ProductName} ({x.Price})", "Top 3 most expensive products: ");

            #endregion

            #region Q2 - Show Page 2 of Products (Page Size = 5)

            var res2 = ProductList.Skip((2 - 1) * 5).Take(5);
            Print(res2, x => x.ProductName, "Page 2 of products: ");

            #endregion

            #region Q3 - Get Products with Price Less Than 25

            var res3 = ProductList.OrderBy(x => x.Price).TakeWhile(x => x.Price < 25);
            Print(res3, x => $"{x.ProductName} ({x.Price})", "Cheap products: ");

            #endregion

            #region Q4 - Check if All Seafood Products Are in Stock
            bool res4 = ProductList
                .Where(x => x.Category == "Seafood")
                .All(x => x.UnitsInStock > 0);

            if (res4)
                Console.WriteLine("All seafood products are in stock.");
            else
                Console.WriteLine("Not all seafood products are in stock.");

            #endregion

            #region Q5 - Check if ID List Contains 9

            int[] ids = { 3, 9, 13, 18 };
            bool res5 = ids.Contains(9);

            if (res5)
                Console.WriteLine("The ID list contains 9.");
            else
                Console.WriteLine("The ID list does not contain 9.");

            #endregion

            #region Q6 - Group Products by Category and Count Products

            var res6 = ProductList.GroupBy(x => x.Category);
            Print(res6, x => $"{x.Key} ({x.Count()})", "Products grouped by category: ");

            #endregion

            #region Q7 - Display Products in Each Category

            var res7 = ProductList.GroupBy(x => x.Category);
            foreach (var group in res7)
            {
                Console.WriteLine($"Category: {group.Key}");
                Print(group, x => x.ProductName, "  Products: ");
            }

            #endregion

            #region Q8 - Find Categories That Have More Than 3 Products

            var res8 = ProductList.GroupBy(x => x.Category).Where(x => x.Count() > 3);
            Print(res8, group => $"Category: {group.Key} | Count: {group.Count()}", "Categories with more than 3 products: ");

            #endregion

            #region Q9 - Group Customers by Country and Calculate Total Order Value

            var res9 =
                from c in CustomerList
                join o in OrderList
                    on c.CustomerID equals o.CustomerID
                group o by c.Country into g
                select new
                {
                    Country = g.Key,
                    Count = g.Count(),
                    TotalOrderValue = g.Sum(o => o.TotalOrderValue)
                };
            Print(res9, x => $"Country: {x.Country} | Count: {x.Count} | Total Order Value: {x.TotalOrderValue}", "Customer orders grouped by country: ");

            #endregion

            #region Q10 - Calculate Total Units in Stock

            long res10 = ProductList.Sum(x => (long)x.UnitsInStock);
            Console.WriteLine($"Total units in stock: {res10}");

            #endregion

            #region Q11 - Find Cheapest and Most Expensive Product Prices

            decimal cheapest = ProductList.Min(x => x.Price);
            decimal mostExpensive = ProductList.Max(x => x.Price);

            Console.WriteLine($"Cheapest product price: {cheapest} | Most expensive product price: {mostExpensive}");

            #endregion

            #region Q12 - Get Distinct Product Categories

            var res12 = (from p in ProductList select p.Category).Distinct();
            Print(res12, x => x, "Categories: ");

            #endregion

            #region Q13 - Find IDs in SetA but Not in SetB

            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };

            var res13 = setA.Except(setB);
            Print(res13, x => x.ToString(), "SetA - SetB: ");

            #endregion

            #region Q14 - Find Countries in List1 but Not in List2 (Case-Insensitive)

            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };
            var res14 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);

            Print(res14, x => x, "List1 - List2: ");

            #endregion

            #region Q15 - Build Dictionary and Get Product with ID 18

            var res15 = ProductList.ToDictionary(x => x.ProductID, x => x);
            var product = res15[18];

            Console.WriteLine($"Product ID: {product.ProductID} | Name: {product.ProductName} | Price: {product.Price}");

            #endregion

            #region Q16 - Get First Product with Price Greater Than 50

            var res16 = ProductList.First(x => x.Price > 50);
            Console.WriteLine($"First product with price > 50: {res16.ProductName} ({res16.Price})");

            #endregion

            #region Q17 - Get First Product with Price Greater Than 500

            var res17 = ProductList.FirstOrDefault(x => x.Price > 500);

            if (res17 != null)
                Console.WriteLine(res17.ProductName);
            else
                Console.WriteLine("No product found.");

            #endregion

            #region Q18 - Generate Multiplication Table of 7

            var res18 = Enumerable.Range(1, 10).Select(x => $"7 x {x} = {7 * x}");

            Print(res18, x => x, "Multiplication table of 7: ");

            #endregion

            #region Q19 - Get Even Numbers from 1 to 30

            var res19 = Enumerable.Range(1, 30).Where(x => x % 2 == 0);
            Print(res19, x => x.ToString(), "Even numbers from 1 to 30: ");

            #endregion

            #region Q20 - Concatenate First 3 Product Names and Customer Names

            var res20 = ProductList.Select(x => x.ProductName).Take(3)
                .Concat(CustomerList.Select(x => x.CompanyName).Take(3));
            Print(res20, x => x, "First 3 product names and first 3 customer company names: ");

            #endregion

            #region Q21 - Pair Products with Customers Using Zip

            var products = ProductList.Select(x => x.ProductName);
            var companies = CustomerList.Select(x => x.CompanyName);
            var res21 = products.Zip(companies, (p, c) => $"{p} sold to {c}");

            Print(res21, x => x, "Products sold to companies: ");

            #endregion
        }
    }
}
