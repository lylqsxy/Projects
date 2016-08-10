using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcApplication4.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext()
            : base("name = StoreContext")
        {
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}