using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Model;
using Exercise.Repository;

namespace Exercise.Service
{
    public class InventoryService:IProductRepository
    {
        public List<Product> InitialiseProduct()
        {
            var products = new List<Product>()
            {
                new Lawmower()
                {
                    IdGuid = 1,
                    Brand = "Masport",
                    FuelEfficiency = "Very Effective",
                    IsVehicle = false,
                    Name = "Lawnmower Masport",
                    Price = 120,
                    StockAvailable = 30
                },
                new Lawmower()
                {
                    IdGuid = 2,
                    Brand = "Morrison",
                    FuelEfficiency = "Very Effective",
                    IsVehicle = false,
                    Name = "Lawnmower Morrison",
                    Price = 50,
                    StockAvailable = 40
                },
                new Lawmower()
                {
                    IdGuid = 3,
                    Brand = "Flymo",
                    FuelEfficiency = "Very Effective",
                    IsVehicle = false,
                    Name = "Lawnmower Flymo",
                    Price = 40,
                    StockAvailable = 20
                },

                new Computer()
                {
                    IdGuid = 4,
                    Name = "HP ProBook 450",
                    Price = 450,
                    HardDrive = "1TB",
                    Memory = "16Gb",
                    StockAvailable = 80

                },
                new Computer()
                {
                    IdGuid = 5,
                    HardDrive = "SSD 512Mb",
                    Memory = "16Gb",
                    Name = "Macbook Pro",
                    Price = 3440,
                    StockAvailable = 30

                },
                new Computer()
                {
                    IdGuid = 6,
                    HardDrive = "SSD 218Mb",
                    Memory = "16Gb",
                    Name = "Asus Pro",
                    Price = 2140,
                    StockAvailable = 20
                },
                new Computer()
                {
                    IdGuid = 7,
                    HardDrive = "HDD 1TB",
                    Memory = "16Gb",
                    Name = "MSI Pro",
                    Price = 3456,
                    StockAvailable = 20
                },
            };
            return products;

        }
    }
}
