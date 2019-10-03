using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SystemInfoExplorer;

namespace ExampleApp
{
    class Program
    {
        /// <summary>
        /// Main program entry point
        /// </summary>
        /// <param name="args">Command-line arguments</param>
        static int Main(string[] args)
        {

            if (args.Length <= 1 || args.Length > 2)
            {
                System.Console.WriteLine("The input arguments are invalid.\n");
                System.Console.WriteLine("Usage: ExampleApp.exe [-e filename] [-s iterations]");
                System.Console.WriteLine("   -e\tRun hardware explorer and log information to output file]");
                System.Console.WriteLine("   -s\tGet system statistics");
                return -1;
            }

            var option = args[0].Trim();

            if(option.ToLower() == "-e")
            {
                Console.WriteLine("Running the hardware explorer instance... (This may take few minutes)");

                // Explore system info
                ExploreSystemInfo(args[1].Trim());                
            }

            if (option.ToLower() == "-s")
            {
                Console.WriteLine("Running the quick statistics instance...");

                int iterations = 0;
                bool error = int.TryParse(args[1], out iterations);

                if(!error && iterations > 0)
                {
                    // Get the CPU usage, used memory size and percentage
                    GetQuickStats(iterations);
                }
                else
                {
                    Console.WriteLine($"Invalid number of iterations (>0)");
                }
                
            }

#if DEBUG
            // Pause console
            Pause();
#endif
            return 0;
        }

        /// <summary>
        /// Runs the Explorer instance to log the system information in a text file
        /// </summary>
        static void ExploreSystemInfo(string filename)
        {
            Explorer exp = new Explorer();
            SystemInfoExplorer.Globals.Enable_File_Output = true;
            SystemInfoExplorer.Globals.Output_Filename = filename;

            exp.Run();
        }

        /// <summary>
        /// Runs the QuickSystemStats instance to get CPU usage, used memory size and other system statistics
        /// </summary>
        static void GetQuickStats(int iterations)
        {
            var counters = new QuickSystemStats();

            for (int i = 0; i < iterations; i++)
            {
                counters.GetStats();

                Console.WriteLine($"{counters.ToString()}");

                System.Threading.Thread.Sleep(1000);
            }
            
        }

        /// <summary>
        /// List available performance counters
        /// </summary>
        static void ListPerformanceCounters()
        {
            PerformanceCounterCategory[] categories = PerformanceCounterCategory.GetCategories();
            foreach (PerformanceCounterCategory category in categories)
            {
                Console.WriteLine("Category name: {0}", category.CategoryName);
                Console.WriteLine("Category type: {0}", category.CategoryType);
                Console.WriteLine("Category help: {0}", category.CategoryHelp);
                Console.WriteLine("");
                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// List the counters available for a given category name
        /// </summary>
        /// <param name="categoryName">Category name (string)</param>
        static void ListCategoryAvailableCounters(string categoryName)
        {
            //Get all performance categories
            PerformanceCounterCategory[] perfCats = PerformanceCounterCategory.GetCategories();

            //Get single category by category name.
            PerformanceCounterCategory cat = perfCats.Where(c => c.CategoryName == categoryName).FirstOrDefault();

            Console.WriteLine("Category Name: {0}", cat.CategoryName);

            //Get all instances available for category
            string[] instances = cat.GetInstanceNames();
            if (instances.Length == 0)
            {
                //This block will execute when category has no instance.
                //loop all the counters available withing category
                foreach (PerformanceCounter counter in cat.GetCounters())
                    Console.WriteLine("     Counter Name: {0}", counter.CounterName);
            }
            else
            {
                //This block will execute when category has one or more instances.
                foreach (string instance in instances)
                {
                    Console.WriteLine("  Instance Name: {0}", instance);
                    if (cat.InstanceExists(instance))
                        //loop all the counters available withing category
                        foreach (PerformanceCounter counter in cat.GetCounters(instance))
                            Console.WriteLine("     Counter Name: {0}", counter.CounterName);
                }
            }
        }

        /// <summary>
        /// Pause the console
        /// </summary>
        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue. . .");
            Console.ReadKey();
        }
    }
}
