using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;


namespace SystemInfoExplorer
{
    public class RealTimeCounters
    {
        public RealTimeCounters()
        {
            TotalMemSize = GetTotalRAMSize();

            if (TotalMemSize == 0.0)
                TotalMemSize = 1.0;
        }

        public double CpuUsage { get; set; }
        public double ThreadCount { get; set; }
        public double FreeMemSize { get; set; }
        public double MemUsagePercent { get; set; }
        public double TotalMemSize { get; set; }

        PerformanceCounter ramCounter;

        public void GetCounters()
        {
            CpuUsage = Double.Parse(getCPUCounter().ToString());

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            FreeMemSize = ramCounter.NextValue();

            MemUsagePercent = (TotalMemSize - FreeMemSize) * 100.0 / TotalMemSize;

#if DEBUG
            Console.WriteLine($"CPU Usage (%): {CpuUsage}");
            Console.WriteLine($"Free Memory (Mb): {FreeMemSize}");
            Console.WriteLine($"Memory Usage (%): {MemUsagePercent}");
#endif

        }

        private static double GetTotalRAMSize()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            double size = 0;

            foreach (ManagementObject item in moc)
            {
                size += Math.Round(Convert.ToDouble(item.Properties["TotalPhysicalMemory"].Value) / 1048576, 0);
            }

            return size;
        }

        public object getCPUCounter()
        {

            PerformanceCounter cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            // will always start at 0
            dynamic firstValue = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(100);
            // now matches task manager reading
            dynamic secondValue = cpuCounter.NextValue();

            return secondValue;

        }


    }
}
