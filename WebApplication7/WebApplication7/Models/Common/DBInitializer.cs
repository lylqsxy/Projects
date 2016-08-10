using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication7.Models.Common;

namespace WebApplication7.Models.Common
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseAlways<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    CustomerName = "John",
                    CustomerAddr = "Auckland"
                },
                new Customer
                {
                    CustomerName = "Harris",
                    CustomerAddr = "Wellington"
                },
                new Customer
                {
                    CustomerName = "Peter",
                    CustomerAddr = "Christchurch"
                },
                new Customer
                {
                    CustomerName = "May",
                    CustomerAddr = "Dunedin"
                },
                new Customer
                {
                    CustomerName = "Joe",
                    CustomerAddr = "Hamilton"
                },
                new Customer
                {
                    CustomerName = "Sam",
                    CustomerAddr = "Nelson"
                },
                new Customer
                {
                    CustomerName = "Jack",
                    CustomerAddr = "Napier"
                },
                new Customer
                {
                    CustomerName = "Terry",
                    CustomerAddr = "New Plymonth"
                },
                new Customer
                {
                    CustomerName = "Sandy",
                    CustomerAddr = "Queenstown"
                },
                new Customer
                {
                    CustomerName = "Cindy",
                    CustomerAddr = "Taranga"
                }
            };
            customers.ForEach(x => context.Customers.Add(x));
            context.SaveChanges();

            List<Product> products = new List<Product>
            {
                new Product
                {
                    ProductName = "glass",
                    ProductPrice = 12.22,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "cup",
                    ProductPrice = 14.19,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "bookshelf",
                    ProductPrice = 19.65,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "sofa",
                    ProductPrice = 16.12,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "chair",
                    ProductPrice = 32.99,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "bag",
                    ProductPrice = 18.77,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "phone",
                    ProductPrice = 13.09,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "PC",
                    ProductPrice = 12.76,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "table",
                    ProductPrice = 15.41,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                },
                new Product
                {
                    ProductName = "desk",
                    ProductPrice = 11.55,
                    ProductDes = "asdfg",
                    ProductCat = "zxcvb",
                }
            };
            products.ForEach(x => context.Products.Add(x));
            context.SaveChanges();


            List<Order> orders = new List<Order>
            {
                new Order
                {
                    ProductID = 5,
                    CustomerID = 10,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 1,
                    CustomerID = 3,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 7,
                    CustomerID = 7,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 2,
                    CustomerID = 9,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 8,
                    CustomerID = 2,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 9,
                    CustomerID = 1,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 1,
                    CustomerID = 6,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 3,
                    CustomerID = 5,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 1,
                    CustomerID = 10,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 4,
                    CustomerID = 1,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 6,
                    CustomerID = 7,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 9,
                    CustomerID = 6,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 2,
                    CustomerID = 3,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 7,
                    CustomerID = 8,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 9,
                    CustomerID = 9,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 2,
                    CustomerID = 10,
                    OrderDate = DateTime.Now
                },
                new Order{
                    ProductID = 6,
                    CustomerID = 1,
                    OrderDate = DateTime.Now
                },
                new Order{
                    ProductID = 4,
                    CustomerID = 2,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 1,
                    CustomerID = 5,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    ProductID = 7,
                    CustomerID = 3,
                    OrderDate = DateTime.Now
                }
            };
            orders.ForEach(x => context.Orders.Add(x));
            context.SaveChanges();

        }
    }
}

