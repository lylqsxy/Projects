using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Model;

namespace Exercise.Service
{
    public class OrderHistoryService
    {
        public static List<OrderHistory> GetOrderHistory()
        {
            return new List<OrderHistory>
            {
                new OrderHistory
                {
                    Customer = new Customer
                    {
                        Name = "Justin Pham",
                        DateCreated = new DateTime(2015,12,4)
                    },
                    Order = new List<Product>
                    {
                        new Lawmower
                        {
                            IdGuid = 1,
                            Brand = "Masport",
                            FuelEfficiency = "Very Effective",
                            IsVehicle = false,
                            Name = "Lawnmower Masport",
                            Price = 120,
                        },
                        new Computer
                        {
                            IdGuid = 4,
                            Name = "HP ProBook 450",
                            Price = 450,
                            HardDrive = "1TB",
                            Memory = "16Gb",
                        },
                    },
                    TotalAmount = 570
                },
                new OrderHistory
                {
                    Customer = new Customer
                    {
                        Name = "John Doe",
                        DateCreated = new DateTime(2015,9,14)
                    },
                    Order = new List<Product>
                    {
                        new Lawmower
                        {
                            IdGuid = 3,
                            Brand = "Flymo",
                            FuelEfficiency = "Very Effective",
                            IsVehicle = false,
                            Name = "Lawnmower Flymo",
                            Price = 40,
                        },
                        new Computer
                        {
                            IdGuid = 4,
                            Name = "HP ProBook 450",
                            Price = 450,
                            HardDrive = "1TB",
                            Memory = "16Gb",
                        },
                    },
                    TotalAmount = 490
                },
                new OrderHistory
                {
                    Customer = new Customer
                    {
                        Name = "Chris Steve",
                        DateCreated = new DateTime(2016,3,14)
                    },
                    Order = new List<Product>
                    {
                        new Computer
                        {
                            IdGuid = 4,
                            Name = "HP ProBook 450",
                            Price = 450,
                            HardDrive = "1TB",
                            Memory = "16Gb",
                        },
                    },
                    TotalAmount = 450
                },
                new OrderHistory
                {
                    Customer = new Customer
                    {
                        Name = "Allen Hillary",
                        DateCreated = new DateTime(2016,1,4)
                    },
                    Order = new List<Product>
                    {
                        new Computer
                        {
                            IdGuid = 4,
                            Name = "HP ProBook 450",
                            Price = 450,
                            HardDrive = "1TB",
                            Memory = "16Gb",
                        },
                    },
                    TotalAmount = 450
                },
                new OrderHistory
                {
                    Customer = new Customer
                    {
                        Name = "Joe Henry",
                        DateCreated = new DateTime(2015,11,4)
                    },
                    Order = new List<Product>
                    {
                        new Computer
                        {
                            IdGuid = 6,
                            HardDrive = "SSD 218Mb",
                            Memory = "16Gb",
                            Name = "Asus Pro",
                            Price = 2140,
                        },
                    },
                    TotalAmount = 2140
                },

                new OrderHistory
                {
                    Customer = new Customer
                    {
                        Name = "Petunia Pacemaker",
                        DateCreated = new DateTime(2015,7,3)
                    },
                    Order = new List<Product>
                    {
                        new Computer
                        {
                            IdGuid = 6,
                            HardDrive = "SSD 218Mb",
                            Memory = "16Gb",
                            Name = "Asus Pro",
                            Price = 2140,
                        },
                        new Lawmower()
                        {
                            IdGuid = 3,
                            Brand = "Flymo",
                            FuelEfficiency = "Very Effective",
                            IsVehicle = false,
                            Name = "Lawnmower Flymo",
                            Price = 40,
                        },

                        new Computer()
                        {
                            IdGuid = 4,
                            Name = "HP ProBook 450",
                            Price = 450,
                            HardDrive = "1TB",
                            Memory = "16Gb",

                        },
                        new Computer()
                        {
                            IdGuid = 5,
                            HardDrive = "SSD 512Mb",
                            Memory = "16Gb",
                            Name = "Macbook Pro",
                            Price = 3440,

                        },
                    },
                    TotalAmount = 6070
                },
            };
        }
    }
}
