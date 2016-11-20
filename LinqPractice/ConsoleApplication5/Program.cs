using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication5.Common;

namespace ConsoleApplication5
{

    class Program
    {
        static void Main(string[] args)
        {
            DatabaseContext db = new DatabaseContext();

            var q = db.Orders.Join(db.Customers, 
                                    o => o.customerId, 
                                    c => c.customerId, 
                                    (o, c) => new { o.orderId, c.customerName, c.customerId });

            foreach (var item in q)
            {
                //Console.WriteLine("{0}, {1}, {2}",item.orderId, item.customerName, item.customerId);
            }

            var q1 = db.Orders.Join(db.Customers,
                        o => o.customerId,
                        c => c.customerId,
                        (o, c) => new { o.orderId, c.customerName, o.productId })
                        .GroupBy(x => x.customerName)
                        .Select(x => new { x.Key, Avg = x.Average(y => y.orderId), Sum = x.Sum(y => y.productId) });

            foreach (var item in q1)
            {
                //Console.WriteLine("{0}, {1}, {2}", item.Key, item.Avg, item.Sum);
            }

            var q2 = from o in db.Orders
                     join c in db.Customers on o.customerId equals c.customerId into box
                     from b in box.DefaultIfEmpty(new Customer())
                     select new
                     {
                         o.orderId,
                         b.customerId


                     };

            foreach (var item in q2)
            {
                //Console.WriteLine("{0}, {1}, {2}", item.orderId, item.customerId, item.orderId);
            }

            var q3 = (from c in db.Customers
                      join o in db.Orders on c.customerId equals o.customerId into box
                      from b in box
                      select new
                      {
                          c,
                          b,
                      }).GroupBy(x => x.c)
                     .Select(x => new
                     {
                         x.Key.customerId,
                         x.Key.customerName,
                         Sum = x.Sum(y => y.b.customerId),
                         num = x.Where(z => z.b.orderId == 102).Select(z => z.b.customerId).FirstOrDefault()
                     });

            foreach (var item in q3)
            {
                //Console.WriteLine("{0}, {1}, {2}, {3}", item.customerId, item.customerName, item.Sum, item.num);
            }

            var q4 = (from c in db.Customers
                      join o in db.Orders on c.customerId equals o.customerId into box
                      from b in box.DefaultIfEmpty(new Order())
                      join p in db.Products on b.productId equals p.productId into otherBox
                      from ob in otherBox.DefaultIfEmpty(new Product())
                      select new
                      {
                          Customers = c,
                          Products = ob,
                      }).GroupBy(x => x.Customers)
                     .Select(y => new
                     {
                         CustomId = y.Key.customerId,
                         CustomName = y.Key.customerName,
                         Count = y.Where(z => z.Products.productId != 0).Count(),
                         Sum = y.Sum(z => z.Products.productPrice),
                     });

            foreach (var item in q4)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", item.CustomId, item.CustomName, item.Count, item.Sum);
            }
                      

            Console.Read();

        }
    }
}
