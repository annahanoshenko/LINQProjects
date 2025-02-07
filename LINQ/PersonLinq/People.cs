using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.PersonLinq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>
        {
            new Person{Id =1, Name = "Alice", Age = 30},
            new Person{Id =2, Name = "Bob", Age = 17},
            new Person{Id =3, Name = "Charlie", Age = 25},
            new Person{Id =4, Name = "Alice", Age = 22},
            new Person{Id =5, Name = "David", Age = 30}
        };

            List<Department> departments = new List<Department>
        {
            new Department{PersonId = 1, DepartmentName = "HR"},
            new Department{PersonId = 2, DepartmentName = "IT"},
            new Department{PersonId = 3, DepartmentName = "Finance"}
        };

            //Filtering the people list by age
            var adults = people.Where(p => p.Age >= 18);
            Console.WriteLine("Adults:");

            foreach (var person in adults)
            {
                Console.WriteLine($"{person.Name}, Age: {person.Age}");
            }
            Console.WriteLine();

            //Select: Getting name list
            var names = people.Select(p => p.Name);
            Console.WriteLine("Names:");
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            //OrderBy: Sorting the people list by name 
            //ThenBy: Sorting the people list by age
            var orderedPeople = people.OrderBy(p => p.Name).ThenBy(p => p.Age);
            Console.WriteLine("Ordered people:");
            foreach (var person in orderedPeople)
            {
                Console.WriteLine($"{person.Name}, Age: {person.Age}");
            }
            Console.WriteLine();

            //GroupBy: Grouping the people list by age
            var groupedByName = people.GroupBy(p => p.Name);
            Console.WriteLine("Group by name:");
            foreach (var group in groupedByName)
            {
                Console.WriteLine($"Name: {group.Key}");
                foreach (var person in group)
                {
                    Console.WriteLine($" Id: {person.Id}, Age: {person.Age}");
                }
            }
            Console.WriteLine();

            //Join: Joining the people list with the departments list
            var personDepartments = people.Join(
                departments,
                person => person.Id,
                dept => dept.PersonId,
                (person, dept) => new
                {
                    person.Name,
                    person.Age,
                    dept.DepartmentName
                });
            Console.WriteLine("Person departments:");
            foreach (var item in personDepartments)
            {
                Console.WriteLine($"{item.Name} ({item.Age} y.o.) works in Department: {item.DepartmentName}");
            }
            Console.WriteLine();

            //Distinct: Getting distinct names from the people list
            var uniqueNames = people.Select(p => p.Name).Distinct();
            Console.WriteLine("Unique names:");
            foreach (var name in uniqueNames)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            //Any and All: Checking if there are any people with age less 18
            //and if all people are older than 18
            bool hasMinors = people.Any(p => p.Age < 18);
            bool allAdults = people.All(p => p.Age >= 18);
            Console.WriteLine($"Has minors: {hasMinors}");
            Console.WriteLine($"All adults: {allAdults}");
            Console.WriteLine();

            try
            {
                //First - returns the first element that satisfies the condition
                var firstAdult = people.First(p => p.Age >= 18);
                Console.WriteLine($"First adult: {firstAdult.Name}, Age:{firstAdult.Age}");

                //FirstOrDefault - returns the first element or default value if the list is empty
                var firstMinor = people.FirstOrDefault(p => p.Age >= 18);
                Console.WriteLine($"First minor: {firstMinor?.Name ?? "Not Found"}");

                //Single - returns the only element that satisfies the condition
                var singleAdult = people.Single(p => p.Id == 3);
                Console.WriteLine($"Single person (Id =3): {singleAdult.Name}, Age: {singleAdult.Age}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();

            //Aggregate: Getting the count, sum, average, min and max 
            int countAdult = people.Count(p => p.Age >= 18);
            int sumOfAge = people.Sum(p => p.Age);
            double averageAge = people.Average(p => p.Age);
            int minAge = people.Min(P => P.Age);
            int maxAge = people.Max(p => p.Age);

            Console.WriteLine("Aggregates:");
            Console.WriteLine($"Count of adults: {countAdult}");
            Console.WriteLine($"Sum of ages: {sumOfAge}");
            Console.WriteLine($"Avarage age: {averageAge: F2}");
            Console.WriteLine($"Min age: {minAge}");
            Console.WriteLine($"Max age: {maxAge}");

            //Deferred execution: query is executed during iteration
            var deferredQuery = people.Where(p => p.Age >= 18);
            Console.WriteLine("\nDeferred Execution example:");
            foreach(var person in deferredQuery)
            {
                Console.WriteLine($"{person.Name}, Age: {person.Age}");
            }
            Console.WriteLine();

            //Immediate execution: query is executed immediately
            var immediateList = people.Where(p => p.Age >= 18).ToList();
            Console.WriteLine("\nImmediate Execution Example (ToList): ");
            foreach(var person in immediateList)
            {
                Console.WriteLine($"{person.Name}, Age: {person.Age}");
            }
            Console.WriteLine("\nPress any key to exit:");
            Console.ReadKey();
        }
    }
}
