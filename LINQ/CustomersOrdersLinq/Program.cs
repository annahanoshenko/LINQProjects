using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LINQ.CustomersOrdersLinq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John", City = "New York" },
                new Customer { Id = 2, Name = "Steve", City = "Phoenix" },
                new Customer { Id = 3, Name = "Bill", City = "Dallas" }
            };

            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 101, CustomerId = 1, OrderDate = new DateTime(2013, 1, 22), Total = 350 },
                new Order { OrderId = 102, CustomerId = 2, OrderDate = new DateTime(2013, 1, 20), Total = 200 },
                new Order { OrderId = 103, CustomerId = 2, OrderDate = new DateTime(2013, 1, 20), Total = 500 },
                new Order { OrderId = 104, CustomerId = 3, OrderDate = new DateTime(2013, 1, 22), Total = 250 },
                new Order { OrderId = 105, CustomerId = 2, OrderDate = new DateTime(2013, 1, 15), Total = 400 },
            };

            //Filtering: order summa > 100
            var highValueOrders = orders.Where(o => o.Total > 100);
            Console.WriteLine("Order summa more 100:");
            foreach (var order in highValueOrders)
            {
                Console.WriteLine($"OrderId: {order.OrderId}, Total: {order.Total}");
            }
            Console.WriteLine();

            //Sorting: orders by date in ascending 
            var orderByDate = orders.OrderBy(o => o.OrderDate);
            Console.WriteLine("Order by date in ascending:");
            foreach(var order in orderByDate)
            {
                Console.WriteLine($"OrderId: {order.OrderId}, Date: {order.OrderDate}");
            }
            Console.WriteLine();

            //Sorting: orders by total price in descending 
            var orderByTotal = orders.OrderBy(o => o.Total);
            Console.WriteLine("Order by total price:");
            foreach(var order in orderByTotal)
            {
                Console.WriteLine($"OrderId: {order.OrderId}, Total: {order.Total}");
            }
            Console.WriteLine();

            //Grouping: Group orders by customers
            var orderGroupedByCustomers = orders.GroupBy(o => o.CustomerId);
            Console.WriteLine("Orders grouped by customers:");
            foreach(var group in orderGroupedByCustomers)
            {
                Console.WriteLine($"CustomerId: {group.Key}");
                foreach(var order in group)
                {
                    Console.WriteLine($"OrderId: {order.OrderId}, Total: {order.Total}");
                }
            }
            Console.WriteLine();

            //Joining: Join customers and orders data
            var orderDetails = orders.Join(
                customers,
                order => order.CustomerId,
                customer => customer.Id,
                (order, customer) => new
                {
                    customer.Name,
                    order.OrderId,
                    customer.City
                }
             );
            Console.WriteLine("Order details:");
            foreach(var detail in orderDetails)
            {
                Console.WriteLine($"Customer: {detail.Name}, OrderId: {detail.OrderId}, City: {detail.City}");
            }
            Console.WriteLine();

            //Total order summa for each client
            var totalByCustomer = orders.GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalSum = g.Sum(o => o.Total)
                });
            Console.WriteLine("Total order summa for each client:");
            foreach(var item in totalByCustomer)
            {
                var customerName = customers.First(c => c.Id == item.CustomerId).Name;
                Console.WriteLine($"{customerName} (CustomerId: {item.CustomerId} - Total summa: {item.TotalSum})");
            }
            Console.WriteLine();

            //Deferred execution
            var defferdExecution = orders.Where(o => o.Total > 100);
            Console.WriteLine("Deferred Execution: Orders with amount > 100 (before data change):");
            foreach(var order in defferdExecution)
            {
                Console.WriteLine($"OrderId: {order.OrderId}, Total: {order.Total}");
            }
            Console.WriteLine();

            //reduce the amount of the first order below 100
            orders[0].Total = 90;
            Console.WriteLine("Deferred Execution: Orders with amount > 100 (before data change):");
            foreach(var order in defferdExecution)
            {
                Console.WriteLine($"OrderId: {order.OrderId}, Total: {order.Total}");
            }
            Console.WriteLine();

            // Immediate execution
            var immediateOrders = orders.Where(o=> o.Total> 100).ToList();
            Console.WriteLine("Immediate Execution (ToList): Orders with amount > 100 (before data change):");
            foreach(var order in immediateOrders)
            {
                Console.WriteLine($"OrderId: {order.OrderId}, Total: {order.Total}");
            }
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    }
}
