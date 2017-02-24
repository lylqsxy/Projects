using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Model;
using Exercise.Service;

namespace Exercise
{
    class Program
    {
        #region Exercise 1
        //static void Main(string[] args)
        //{
        //    var productService = new ProductService();
        //    var products = productService.InitialiseProduct();
        //    Console.WriteLine("List of all products in system");
        //    foreach (var p in products)
        //    {
        //        Console.WriteLine("Product number: {0}", p.IdGuid);
        //        Console.WriteLine("Product name: {0}", p.Name);
        //        Console.WriteLine("Product price: {0}", p.Price);
        //        Console.WriteLine("----------------------");
        //    }
        //    //Create empty shoppingCart - list of ShoppingCart object
        //    var shoppingCart = new List<ShoppingCart>();
        //    bool continueShopping = true;
        //    do
        //    {
        //        Console.WriteLine("Please enter product number you want to purchase");
        //        //Convert product id to integer
        //        var productId = Convert.ToInt32(Console.ReadLine());
        //        //Check if product id valid 
        //        //Use foreach to go through all product list
        //        foreach (var pr in products)
        //        {
        //            //If product in foreach same with product number user input
        //            if (pr.IdGuid == productId)
        //            {
        //                //Get specified product object by casting product to specified object
        //                var productType = pr.GetType();
        //                //Switch case to specify product that user added for display on the screen
        //                switch (productType.Name)
        //                {
        //                    case "Lawmower":
        //                    {
        //                        var lawMower = (Lawmower) pr;
        //                        Console.WriteLine("Product number: {0}", lawMower.IdGuid);
        //                        Console.WriteLine("Product name: {0}", lawMower.Name);
        //                        Console.WriteLine("Product price: {0}", lawMower.Price);
        //                        Console.WriteLine("Product brand: {0}", lawMower.Brand);
        //                        Console.WriteLine("Product fuel efficiency: {0}", lawMower.FuelEfficiency);
        //                        Console.WriteLine("----------------------");
        //                        break;
        //                    }
        //                    case "Computer":
        //                    {
        //                        var lawMower = (Computer) pr;
        //                        Console.WriteLine("Product number: {0}", lawMower.IdGuid);
        //                        Console.WriteLine("Product name: {0}", lawMower.Name);
        //                        Console.WriteLine("Product price: {0}", lawMower.Price);
        //                        Console.WriteLine("Product memory: {0}", lawMower.Memory);
        //                        Console.WriteLine("Product hard drive: {0}", lawMower.HardDrive);
        //                        Console.WriteLine("----------------------");
        //                        break;
        //                    }
        //                    default:
        //                    {
        //                        break;
        //                    }
        //                }
        //                //Get user quantity input
        //                Console.WriteLine("Please enter your quantity you want to purchase");
        //                int quantity = Convert.ToInt32(Console.ReadLine());
        //                //Create new object shopping cart and store value of chosen product - initializer 
        //                var chosenProduct = new ShoppingCart
        //                {
        //                    IdGuid = pr.IdGuid,
        //                    Name = pr.Name,
        //                    Price = pr.Price,
        //                    Quantity = quantity
        //                };
        //                //-------------------
        //                //Add product chosen to shopping cart 
        //                shoppingCart.Add(chosenProduct);
        //                // ----------------------------------
        //                Console.WriteLine("You has {0} product in your cart",shoppingCart.Count());
        //                Console.WriteLine("Would you like to add more product to your cart. Press Y to continue or N to Checkout");
        //                string option = Console.ReadLine();
        //                if (option == "Y" || option == "y")
        //                {
        //                    break;
        //                    //Console.WriteLine("Total to pay: ${0}", pr.Price);
        //                    //Console.WriteLine("Thank you for shopping with us. Bye bye");
        //                }
        //                else
        //                {
        //                    //Call display receipt method
        //                    DisplayReceipt(shoppingCart);
        //                    //Call display total to pay
        //                    DisplayTotalToPay(shoppingCart);
        //                    Console.WriteLine("Thank you for shopping with us. Bye bye");
        //                    continueShopping = false;
        //                    Console.ReadKey();
        //                }
        //            }
        //        }
        //    } while (continueShopping);
        //}
        #endregion

        /// <summary>
        /// Entry point when application start
        /// </summary>
        static void Main(string[] args)
        {
            var orderHistory = OrderHistoryService.GetOrderHistory();
            //TODO Your code will be here///
            foreach(var o in orderHistory)
            {
                Console.WriteLine("Customer: {0}, Amount:{1}.", o.Customer.Name, o.TotalAmount.ToString());
                foreach(var od in o.Order)
                {
                    Console.WriteLine("Order: {0}", od.Name);
                }
                Console.WriteLine("\r\n");
            }

            var inventory = new InventoryService().InitialiseProduct();
            var orders = orderHistory.SelectMany(x => x.Order);
            foreach(var p in inventory)
            {
                int finalStock = p.StockAvailable - orders.Count(x => x.IdGuid == p.IdGuid);
                Console.WriteLine("Name: {0}, Initial Stock: {1}, Final  Stock: {2}", p.Name, p.StockAvailable, finalStock);
            }
            var oo = orders.GroupBy(x => x.Name).Select(x => new { Name = x.Key, Number = x.Count()});
            int maxo = oo.Max(x => x.Number);
            var ooo = oo.Where(x => x.Number == maxo);
            foreach(var o in ooo)
            {
                Console.WriteLine("Name: {0}, Number: {1}", o.Name, o.Number);
            }

            Console.WriteLine("\r\n");
            foreach (var o in orderHistory.Where(x => x.Customer.DateCreated.Year.Equals(2015)))
            {
                Console.WriteLine("Name: {0}, Date: {1}", o.Customer.Name, o.Customer.DateCreated);
            }

            Console.WriteLine("\r\n");

            foreach (var o in orderHistory.Where(x => x.Order.Count >= 2))
            {
                Console.WriteLine("Name: {0}, Number: {1}", o.Customer.Name, o.Order.Count);
            }
            Console.Read();
            //
        }
        //Display total to pay from shopping cart
        public static void DisplayTotalToPay(List<ShoppingCart> customerShoppingCart)
        {
            decimal totalToPay = 0;
            foreach (var shoppingCart in customerShoppingCart)
            {
                totalToPay += (shoppingCart.Price * shoppingCart.Quantity);
            }
            Console.WriteLine("Total to pay: ${0}", totalToPay);
        }
        //Display reciept
        public static void DisplayReceipt(List<ShoppingCart> customerShoppingCart)
        {
            foreach (var shoppingCart in customerShoppingCart)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Product number : {0}", shoppingCart.IdGuid);
                Console.WriteLine("Name: {0} x {1}", shoppingCart.Name, shoppingCart.Quantity);
                Console.WriteLine("Price: ${0} x {1} = ${2}", shoppingCart.Price, shoppingCart.Quantity, shoppingCart.Price * shoppingCart.Quantity);
                Console.WriteLine("---------------------");
            }
        }
    }
}
