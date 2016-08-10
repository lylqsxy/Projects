using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication7.Models.Common
{
    public class DatabaseContext
    {
        private List<Customer> customers = new List<Customer>
        {
            new Customer{
                ID = 1,
                CustomerName = "John",
                CustomerAddr = "Auckland"
            },
            new Customer{
                ID = 2,
                CustomerName = "Harris",
                CustomerAddr = "Wellington"
            },
            new Customer{
                ID = 3,
                CustomerName = "Peter",
                CustomerAddr = "Christchurch"
            },
            new Customer{
                ID = 4,
                CustomerName = "May",
                CustomerAddr = "Dunedin"
            },
            new Customer{
                ID = 5,
                CustomerName = "Joe",
                CustomerAddr = "Hamilton"
            },
            new Customer{
                ID = 6,
                CustomerName = "Sam",
                CustomerAddr = "Nelson"
            },
            new Customer{
                ID = 7,
                CustomerName = "Jack",
                CustomerAddr = "Napier"
            },
            new Customer{
                ID = 8,
                CustomerName = "Terry",
                CustomerAddr = "New Plymonth"
            },
            new Customer{
                ID = 9,
                CustomerName = "Sandy",
                CustomerAddr = "Queenstown"
            },
            new Customer{
                ID = 10,
                CustomerName = "Cindy",
                CustomerAddr = "Taranga"
            }

        };

        private List<Order> orders = new List<Order>{
            new Order{
                ID = 101,
                ProductID = 504,
                CustomerID = 100,

            },
            new Order{
                ID = 102,  
                ProductID = 502,
                CustomerID = 3,

            },
            new Order{
                ID = 103,
                ProductID = 507,
                CustomerID = 7,
                
            },
            new Order{
                ID = 104,
                ProductID = 507,
                CustomerID = 9,
                
            },
            new Order{
                ID = 105,
                ProductID = 505,
                CustomerID = 2,
                
            },
            new Order{
                ID = 106,
                ProductID = 504,
                CustomerID = 1,
                
            },
            new Order{
                ID = 107,
                ProductID = 509,
                CustomerID = 6,
                
            },
            new Order{
                ID = 108,
                ProductID = 506,
                CustomerID = 5,
                
            },
            new Order{
                ID = 109,
                ProductID = 501,
                CustomerID = 10,
                
            },
            new Order{
                ID = 110,
                ProductID = 502,
                CustomerID = 1,
                
            },
            new Order{
                ID = 111,
                ProductID = 503,
                CustomerID = 7,
                
            },
            new Order{
                ID = 112,
                ProductID = 504,
                CustomerID = 6,
                
            },
            new Order{
                ID = 113,
                ProductID = 502,
                CustomerID = 3,
                
            },
            new Order{
                ID = 114,
                ProductID = 503,
                CustomerID = 8,
                
            },
            new Order{
                ID = 115,
                ProductID = 510,
                CustomerID = 9,
                
            },
            new Order{
                ID = 116,
                ProductID = 509,
                CustomerID = 10,
                
            },
            new Order{
                ID = 117,
                ProductID = 503,
                CustomerID = 1,
                
            },
            new Order{
                ID = 118,
                ProductID = 502,
                CustomerID = 2,
                
            },
            new Order{
                ID = 119,
                ProductID = 506,
                CustomerID = 5,
                
            },
            new Order{
                ID = 120,
                ProductID = 507,
                CustomerID = 3,
                
            }
        };

        private List<Product> products = new List<Product>{
            new Product{
                ID = 501,
                ProductName = "glass",
                ProductPrice = 12.22,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 502,
                ProductName = "cup",
                ProductPrice = 14.19,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 503,
                ProductName = "bookshelf",
                ProductPrice = 19.65,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 504,
                ProductName = "sofa",
                ProductPrice = 16.12,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 505,
                ProductName = "chair",
                ProductPrice = 32.99,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 506,
                ProductName = "bag",
                ProductPrice = 18.77,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 507,
                ProductName = "phone",
                ProductPrice = 13.09,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 508,
                ProductName = "PC",
                ProductPrice = 12.76,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 509,
                ProductName = "table",
                ProductPrice = 15.41,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            },
            new Product{
                ID = 510,
                ProductName = "desk",
                ProductPrice = 11.55,
                ProductDes = "asdfg",
                ProductCat = "zxcvb",
            }
        };

        public List<Customer> Customers 
        { 
            get { return customers;}
            set {}
        }
        public List<Order> Orders
        { 
            get { return orders;}
            set {}
        }
        public List<Product> Products
        { 
            get { return products;}
            set {}
        }
    }
}
