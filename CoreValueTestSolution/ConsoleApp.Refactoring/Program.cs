using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopProducts shopProducts = new ShopProducts("John Doe");
            shopProducts.AddProduct(new ProductPerPound { ProductName = "Pulled Pork", Price = 6.99m, Weight = 0.5m });
            shopProducts.AddProduct(new ProductPerItem { ProductName = "Coke", Price = 3m, Quantity = 2 });
            shopProducts.CountPrice();

            ShopProducts shopProductsStiven = new ShopProducts("Stiven Sigal");
            shopProductsStiven.AddProduct(new List<Product>() {
                new ProductPerPound { ProductName = "Pulled Pork", Price = 7.99m, Weight = 1.5m },
                new ProductPerItem { ProductName = "Coke", Price = 4m, Quantity = 5 }
            });
            shopProductsStiven.CountPrice();

            ShopProducts shopProductsMerlin = new ShopProducts("Merlin Monro", new List<Product>() {
                new ProductPerPound { ProductName = "Pulled Pork", Price = 9.99m, Weight = 2.5m },
                new ProductPerItem { ProductName = "Coke", Price = 1m, Quantity = 8 }
            });
            shopProductsMerlin.CountPrice();
            Console.ReadKey();
        }
    }
    public class ShopProducts
    {
        public decimal Price = 0m;
        public string Сustomer;
        List<Product> Products = new List<Product>();

        public ShopProducts(string customer)
        {
            this.Сustomer = customer;
            this.Price = 0m;
            this.Products = new List<Product>();
        }
        public ShopProducts(string customer, List<Product> products)
        {
            this.Сustomer = customer;
            this.Price = 0m;
            this.Products = products;
        }
        public void AddProduct(Product product)
        {
            this.Products.Add(product);
        }
        public void AddProduct(List<Product> products)
        {
            this.Products.AddRange(products);
        }
        public void CountPrice()
        {
            Console.WriteLine($"ORDER SUMMARY FOR {Сustomer} :");
            foreach (Product iProduct in this.Products)
            {
                this.Price += iProduct.GetProductPrice();
                iProduct.ShowInfo();
            }
            Console.WriteLine($"Total Price: ${Price}");
        }
    }

    public class ProductPerItem : Product
    {
        public decimal Quantity { get; set; }
        public override decimal GetProductPrice()
        {
            return this.Price * this.Quantity;
        }

        public override void ShowInfo()
        {
            Console.WriteLine(String.Format("{0} ${1} ({2} items at ${3} each)", ProductName, GetProductPrice(), Quantity, Price));
        }
    }
    public class ProductPerPound : Product
    {
        public decimal Weight { get; set; }
        public override decimal GetProductPrice()
        {
            return this.Price * this.Weight;
        }
        public override void ShowInfo()
        {
            Console.WriteLine(String.Format("{0} ${1} ({2} pounds at ${3} per pound)", ProductName, GetProductPrice(), Weight, Price));
        }
    }
    public abstract class Product : IProduct
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public abstract void ShowInfo();
        public abstract decimal GetProductPrice();
    }
    public interface IProduct
    {
        string ProductName { get; set; }
        decimal Price { get; set; }
        void ShowInfo();
        decimal GetProductPrice();
    }
}
