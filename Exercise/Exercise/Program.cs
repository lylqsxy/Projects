using System;
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
        static void Main(string[] args)
        {
            var productService = new ProductService();
            var products = productService.InitialiseProduct();
            Console.WriteLine("List of all products in system");
            foreach (var p in products)
            {
                Console.WriteLine("Product number: {0}", p.IdGuid);
                Console.WriteLine("Product name: {0}", p.Name);
                Console.WriteLine("Product price: {0}", p.Price);
                Console.WriteLine("----------------------");
            }
            Console.WriteLine("Please enter product number you want to purchase");
            //Convert product id to integer
            var productId = Convert.ToInt32(Console.ReadLine());
            //Check if product id valid 
            //Use foreach to go through all product list
            foreach (var pr in products)
            {
                if (pr.IdGuid == productId)
                {
                    var productType = pr.GetType();
                    Console.WriteLine("We found 1 product in our system");
                    switch (productType.Name)
                    {
                        case "Lawnmower":
                            {
                                var lawMower = (Lawmower)pr;
                                Console.WriteLine("Product number: {0}", lawMower.IdGuid);
                                Console.WriteLine("Product name: {0}", lawMower.Name);
                                Console.WriteLine("Product price: {0}", lawMower.Price);
                                Console.WriteLine("Product brand: {0}", lawMower.Brand);
                                Console.WriteLine("Product fuel efficiency: {0}", lawMower.FuelEfficiency);
                                Console.WriteLine("----------------------");
                                break;
                            }
                        case "Computer":
                            {
                                var lawMower = (Computer)pr;
                                Console.WriteLine("Product number: {0}", lawMower.IdGuid);
                                Console.WriteLine("Product name: {0}", lawMower.Name);
                                Console.WriteLine("Product price: {0}", lawMower.Price);
                                Console.WriteLine("Product memory: {0}", lawMower.Memory);
                                Console.WriteLine("Product hard drive: {0}", lawMower.HardDrive);
                                Console.WriteLine("----------------------");
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    Console.WriteLine("Would you like to pay for this product. Press Y to continue");
                    string option = Console.ReadLine();
                    if (option == "Y" || option == "y")
                    {
                        Console.WriteLine("Total to pay: ${0}", pr.Price);
                        Console.WriteLine("Thank you for shopping with us. Bye bye");
                    }
                    else
                    {
                        Console.WriteLine("Thank you for shopping with us. Bye bye");
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
