using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5.Common
{
    public class DatabaseContext
    {
        private List<Customer> customers = new List<Customer>
        {
            new Customer{
                customerId = 1,
                customerName = "John",
                customerAddr = "Auckland"
            },
            new Customer{
                customerId = 2,
                customerName = "Harris",
                customerAddr = "Wellington"
            },
            new Customer{
                customerId = 3,
                customerName = "Peter",
                customerAddr = "Christchurch"
            },
            new Customer{
                customerId = 4,
                customerName = "May",
                customerAddr = "Dunedin"
            },
            new Customer{
                customerId = 5,
                customerName = "Joe",
                customerAddr = "Hamilton"
            },
            new Customer{
                customerId = 6,
                customerName = "Sam",
                customerAddr = "Nelson"
            },
            new Customer{
                customerId = 7,
                customerName = "Jack",
                customerAddr = "Napier"
            },
            new Customer{
                customerId = 8,
                customerName = "Terry",
                customerAddr = "New Plymonth"
            },
            new Customer{
                customerId = 9,
                customerName = "Sandy",
                customerAddr = "Queenstown"
            },
            new Customer{
                customerId = 10,
                customerName = "Cindy",
                customerAddr = "Taranga"
            }

        };

        private List<Order> orders = new List<Order>{
            new Order{
                orderId = 101,
                productId = 504,
                customerId = 100,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 102,  
                productId = 502,
                customerId = 3,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 103,
                productId = 507,
                customerId = 7,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 104,
                productId = 507,
                customerId = 9,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 105,
                productId = 505,
                customerId = 2,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 106,
                productId = 504,
                customerId = 1,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 107,
                productId = 509,
                customerId = 6,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 108,
                productId = 506,
                customerId = 5,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 109,
                productId = 501,
                customerId = 10,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 110,
                productId = 502,
                customerId = 1,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 111,
                productId = 503,
                customerId = 7,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 112,
                productId = 504,
                customerId = 6,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 113,
                productId = 502,
                customerId = 3,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 114,
                productId = 503,
                customerId = 8,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 115,
                productId = 510,
                customerId = 9,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 116,
                productId = 509,
                customerId = 10,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 117,
                productId = 503,
                customerId = 1,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 118,
                productId = 502,
                customerId = 2,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 119,
                productId = 506,
                customerId = 5,
                orderDate = DateTime.Now
            },
            new Order{
                orderId = 120,
                productId = 507,
                customerId = 3,
                orderDate = DateTime.Now
            }
        };

        private List<Product> products = new List<Product>{
            new Product{
                productId = 501,
                productName = "glass",
                productPrice = 12.22,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 502,
                productName = "cup",
                productPrice = 14.19,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 503,
                productName = "bookshelf",
                productPrice = 19.65,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 504,
                productName = "sofa",
                productPrice = 16.12,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 505,
                productName = "chair",
                productPrice = 32.99,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 506,
                productName = "bag",
                productPrice = 18.77,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 507,
                productName = "phone",
                productPrice = 13.09,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 508,
                productName = "PC",
                productPrice = 12.76,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 509,
                productName = "table",
                productPrice = 15.41,
                productDes = "asdfg",
                productCat = "zxcvb",
            },
            new Product{
                productId = 510,
                productName = "desk",
                productPrice = 11.55,
                productDes = "asdfg",
                productCat = "zxcvb",
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
