using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.Customers;
using InterviewTest.Orders;
using InterviewTest.Products;
using InterviewTest.Returns;

namespace InterviewTest
{
    public class Program
    {
        // private static readonly OrderRepository orderRepo = new OrderRepository();
        // private static readonly ReturnRepository returnRepo = new ReturnRepository();

        static void Main(string[] args)
        {
            // ------------------------
            // Coding Challenge Requirements
            // ------------------------


            // ------------------------
            // Code Implementations
            // ------------------------
            // 1: Implement get total sales, returns, and profit in the CustomerBase class.
            // 2: Record when an item was purchased.


            // ------------------------
            // Bug fixes
            // ------------------------
            // ~~ Run the console app after implementing the Code Changes section above! ~~
            // 1: Meyer Truck Equipment's returns are not being processed.
            // 2: Ruxer Ford Lincoln, Inc.'s totals are incorrect.
            

            // ------------------------
            // Bonus
            // ------------------------
            // 1: Create unit tests for the ordering and return process.
            // 2: Create a database and refactor all repositories to save/update/pull from it.

            ProcessTruckAccessoriesExample();

            ProcessCarDealershipExample();

            Console.ReadKey();
        }

        private static void ProcessTruckAccessoriesExample()
        {
            var customer = GetTruckAccessoriesCustomer();
            IOrder order = new Order("TruckAccessoriesOrder123", customer);
            order.AddProduct(new HitchAdapter());
            order.AddProduct(new BedLiner());
            customer.CreateOrder(order);

            IReturn rga = new Return("TruckAccessoriesReturn123", order);
            rga.AddProduct(order.Products.First());
            customer.CreateReturn(rga);

            ConsoleWriteLineResults(customer);
        }

        private static void ProcessCarDealershipExample()
        { 
            var customer = GetCarDealershipCustomer();
            IOrder order = new Order("CarDealerShipOrder123", customer);
            order.AddProduct(new ReplacementBumper());
            order.AddProduct(new SyntheticOil());
            customer.CreateOrder(order);

            IReturn rga = new Return("CarDealerShipReturn123", order);
            rga.AddProduct(order.Products.First());
            customer.CreateReturn(rga);
            ConsoleWriteLineResults(customer);
        }

        private static ICustomer GetTruckAccessoriesCustomer()
        {
            return new TruckAccessoriesCustomer(new OrderRepository(), new ReturnRepository());
        }

        private static ICustomer GetCarDealershipCustomer()
        {
            return new CarDealershipCustomer(new OrderRepository(), new ReturnRepository());
        }

        private static void ConsoleWriteLineResults(ICustomer customer)
        {
            Console.WriteLine(customer.GetName());

            Console.WriteLine($"Total Sales: {customer.GetTotalSales().ToString("c")}");

            Console.WriteLine($"Total Returns: {customer.GetTotalReturns().ToString("c")}");

            Console.WriteLine($"Total Profit: {customer.GetTotalProfit().ToString("c")}");

            List<IOrder> orders = customer.GetOrders();
            Dictionary<String, List<DateTime>> fullList = new Dictionary<String, List<DateTime>>();

            foreach (IOrder order in orders)
            {
                foreach (OrderedProduct op in order.Products)
                {
                    String productKey = op.Product.GetProductNumber();
                    if (!fullList.ContainsKey(productKey)) {
                        fullList.Add(productKey, new List<DateTime>());
                    }
                    fullList[productKey].Add(order.purchaseTime);
                }
            }

            foreach (String productKey in fullList.Keys)
            {
                Console.WriteLine(productKey + " was bought at following times:");
                foreach (DateTime t in fullList[productKey]) {
                    Console.WriteLine(t.ToString());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
