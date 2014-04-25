using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsertBenchmark
{
    class Program
    {
        private static Stopwatch stopwatch { get; set; }

        static void Main(string[] args)
        {
            Setup();

            int numPasses = 5;
            Perform(50, numPasses);
            Perform(100, numPasses);
            Perform(1000, numPasses);
            Perform(5000, numPasses);
            Perform(10000, numPasses);
            Perform(50000, numPasses);
            Perform(100000, numPasses);
            Perform(350000, numPasses);
        }

        private static void Setup()
        {
            stopwatch = new Stopwatch();

            // Prime the SQL connection pool, and preload assemblies.
            // We want these factors excluded from our calculations.
            var items = GenerateItems(1);
            BulkCopyService.Insert(items);
            XmlInsertService.Insert(items);
            StandardInsertService.Insert(items);
        }

        private static void Perform(int numItems, int numPasses)
        {
            var items = GenerateItems(numItems);

            // Time bulk copy
            Time(() =>
            {
                for (int x = 0; x < numPasses; x++)
                {
                    BulkCopyService.Insert(items);
                }
            });
            Log("BCP", numItems, numPasses);

            // Time XML insert
            Time(() =>
            {
                for (int x = 0; x < numPasses; x++)
                {
                    XmlInsertService.Insert(items);
                }
            });
            Log("XML", numItems, numPasses);

            // Time standard inserts
            Time(() =>
            {
                for (int x = 0; x < numPasses; x++)
                {
                    StandardInsertService.Insert(items);
                }
            });
            Log("STD", numItems, numPasses);

            Console.WriteLine();
        }

        private static List<Item> GenerateItems(int numItems)
        {
            List<Item> items = new List<Item>();

            for (int i = 0; i < numItems; i++)
            {
                items.Add(new Item()
                {
                    Name = "Item " + i.ToString(),
                    Width = i,
                    Height = i,
                    Depth = i
                });
            }

            return items;
        }

        private static void Time(Action a)
        {
            stopwatch.Restart();
            a();
            stopwatch.Stop();
        }

        private static void Log(string name, int numItems, int numPasses)
        {
            var rate = Math.Round(numItems * numPasses / (decimal)stopwatch.ElapsedMilliseconds * 1000);
            Console.WriteLine("{0}: {1} items, {2}X, {3} ms, {4} items/s", name, numItems, numPasses, stopwatch.ElapsedMilliseconds, rate);
        }
    }
}
