using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session4
{
    class Vehicle
    {
        //protected int width, height;
        protected string color;
        protected int numberOfWheels;
        public Vehicle(string c, int n)
        {
            color = c;
            numberOfWheels = n;
        }
        public virtual string Run()
        {
            Console.WriteLine("Run from parent class");
            return String.Format("Vehicle color is: {0} and has : {1} wheels", color, numberOfWheels);
        }
    }
    class Car : Vehicle
    {
        public Car(string c = "", int n = 0) : base(c, n)
        {

        }
        public override string Run()
        {
            Console.WriteLine("Display specification of car");
            return String.Format("Car color is: {0} and has : {1} wheels", color, numberOfWheels);
        }
    }
    class Bike : Vehicle
    {
        public Bike(string c = "", int n = 0) : base(c, n)
        {

        }
        public override string Run()
        {
            Console.WriteLine("Display specification of bike");
            return String.Format("Bike color is: {0} and has : {1} wheels", color, numberOfWheels);
        }
    }
    class Caller
    {
        public void CallRun(Vehicle vh)
        {
            string a;
            a = vh.Run();
            Console.WriteLine("Executing.......: {0}", a);
        }
    }
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Caller caller = new Caller();
        //    Vehicle vehicle = new Vehicle("Blue", 4);
        //    Car car = new Car("Red", 4);
        //    Bike bike = new Bike("Black", 2);
        //    caller.CallRun(vehicle);
        //    caller.CallRun(car);
        //    caller.CallRun(bike);
        //    Console.ReadKey();
        //}
    }
}
