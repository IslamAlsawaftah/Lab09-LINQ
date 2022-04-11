using Lab09_LINQ.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab09_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Root data =  GetData();
                Neighborhoods(data);
                NeighborhoodsWithNoNames(data);
                RemoveDuplicates(data);
                ConsolidateIntoOneSingleQuery(data);
                LinqMethod(data);
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine("Message: " + e.Message);
            }
        }
        public static void Neighborhoods(Root root) // pass json object
        {
            var q1 = from Feature in root.features
                     select Feature.properties.neighborhood;
            int count = 1;
            Console.WriteLine("All neighborhoods");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); //  147 neighborhoods
                count++;
            }
            
        }

        public static void NeighborhoodsWithNoNames(Root root)
        {
            var q1 = from Feature in root.features
                     where Feature.properties.neighborhood != ""
                     select Feature.properties.neighborhood;
            int count = 1;
            Console.WriteLine("Filter out all the neighborhoods that do not have any names");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); // Final Total: 143
                count++;
            }

        }
        public static void RemoveDuplicates(Root root)
        {
            var q1 = (from Feature in root.features
                      where Feature.properties.neighborhood != ""
                      select Feature.properties.neighborhood).Distinct();
            int count = 1;
            Console.WriteLine("All neighborhoods without duplicates");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); // Final Total: 39 neighborhoods
                count++;
            }
        }
        public static void ConsolidateIntoOneSingleQuery(Root root)
        {
            // write query in one line 
            var q1 = root.features
                .Select(Feature => new { Feature.properties.neighborhood })
                .OrderByDescending(Feature => Feature.neighborhood)
                .Where(Feature => Feature.neighborhood != "");
     
            int count = 1;
            Console.WriteLine("Filter out all the neighborhoods that do not have any names");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); // Final Total: 143
                count++;
            }
        }
        public static void LinqMethod(Root root)
        {
            var q1 = root.features
                // creation of new object because i dont want to select the whole object
                .Select(f => new {f.properties.neighborhood}); // lambda statement over select method
            int count = 1;
            Console.WriteLine("All neighborhoods");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); //  147 neighborhoods
                count++;
            }
        }
        static Root GetData()
        {
            Root root = JsonConvert.DeserializeObject<Root>(File.ReadAllText("../../../data.json"));
            return root;
        }
    }
}
