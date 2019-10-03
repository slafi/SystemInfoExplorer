using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;


namespace SystemInfoExplorer
{
    /// <summary>
    /// \class QuickSystemStats
    /// A shortcut class which provides some real-time statistics of the system (e.g., CPU usage, free memory size, etc.).
    /// </summary>
    public class QuickSystemStats
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public QuickSystemStats()
        {
            TotalMemSize = GetTotalRAMSize();
        }

        /// <summary>
        /// CPU usage percentage
        /// </summary>
        public double CpuUsage { get; private set; }

        /// <summary>
        /// Number of running threads
        /// </summary>
        public int ThreadCount { get; private set; }

        /// <summary>
        /// The size of the free memory
        /// </summary>
        public double FreeMemSize { get; private set; }

        /// <summary>
        /// The percentage of used memory
        /// </summary>
        public double MemUsagePercent { get; private set; }

        /// <summary>
        /// The total memory size
        /// </summary>
        public double TotalMemSize { get; private set; }

        /// <summary>
        /// Number of context switches
        /// </summary>
        public int CPUContextSwitches { get; private set; }

        /// <summary>
        /// The number of handles count
        /// </summary>
        public int HandlesCount { get; private set; }

        /// <summary>
        /// The number of system calls being serviced by the CPU per second
        /// </summary>
        public int SystemCallsCount { get; private set; }

        /// <summary>
        /// The total number of bytes read from the disk
        /// </summary>
        public int BytesReadFromDisk { get; private set; }

        /// <summary>
        /// The total number of bytes written to the disk
        /// </summary>
        public int BytesWrittenToDisk { get; private set; }

        /// <summary>
        /// The average time of disk reading operations
        /// </summary>
        public double AvgTimeDiskReadPerSeond { get; private set; }

        /// <summary>
        /// The average time of disk writing operations
        /// </summary>
        public double AvgTimeDiskWritePerSeond { get; private set; }

        /// <summary>
        /// This function gets the CPU usage, free memory size and memory usage percent
        /// </summary>
        public int GetStats()
        {
            try
            {
                FreeMemSize = int.Parse(GetFreeMemorySize().ToString());

                CpuUsage = Double.Parse(GetCPUUsage().ToString());
                
                MemUsagePercent = (TotalMemSize > 0) ? (TotalMemSize - FreeMemSize) * 100.0 / TotalMemSize : 0.0;

                ThreadCount = int.Parse(GetThreadCount().ToString());

                CPUContextSwitches = int.Parse(GetContextSwitchesCount().ToString());

                HandlesCount = int.Parse(GetHandlesCount().ToString());

                SystemCallsCount = int.Parse(GetSystemCallsCount().ToString());

                BytesReadFromDisk = int.Parse(GetDiskReadBytesCount().ToString());

                BytesWrittenToDisk = int.Parse(GetDiskWriteBytesCount().ToString());

                AvgTimeDiskReadPerSeond = Double.Parse(GetDiskReadPerSeond().ToString());

                AvgTimeDiskWritePerSeond = Double.Parse(GetDiskWritePerSeond().ToString());

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Builds a string with the class members
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            StringBuilder oStr = new StringBuilder();

            oStr.Append($"Total memory size: {TotalMemSize} Mbytes\n");
            oStr.Append($"Free memory: {FreeMemSize} Mbytes\n");
            oStr.Append($"Memory usage: {MemUsagePercent}%\n");
            oStr.Append($"CPU usage: {CpuUsage}%\n");
            oStr.Append($"Context switches: {CPUContextSwitches}\n");
            oStr.Append($"No. threads: {ThreadCount}\n");
            oStr.Append($"No. handles: {HandlesCount}\n");
            oStr.Append($"System calls: {SystemCallsCount}\n");
            oStr.Append($"Bytes read from the disk: {BytesReadFromDisk} bytes\n");
            oStr.Append($"Bytes written to the disk: {BytesWrittenToDisk} bytes\n");
            oStr.Append($"Avg. disk reading time: {AvgTimeDiskReadPerSeond}s\n");
            oStr.Append($"Avg. disk writing time: {AvgTimeDiskWritePerSeond}s\n");

            return oStr.ToString();
        }

        /// <summary>
        /// Get the total size of the RAM memory
        /// </summary>
        /// <returns>Double value</returns>
        private double GetTotalRAMSize()
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

        /// <summary>
        /// Gets the current CPU usage time
        /// </summary>
        /// <returns>Object</returns>
        private object GetCPUUsage()
        {

            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            // will always start at 0
            dynamic firstValue = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(100);

            // now matches task manager reading
            dynamic secondValue = cpuCounter.NextValue();

            return secondValue;
        }

        /// <summary>
        /// Reports the total size of free memory
        /// </summary>
        /// <returns>Object</returns>
        private object GetFreeMemorySize()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");            

            return ramCounter.NextValue();
        }

        /// <summary>
        /// Reports the total number of threads running on CPU
        /// </summary>
        /// <returns>Object</returns>
        private object GetThreadCount()
        {
            PerformanceCounter threadCount = new PerformanceCounter("Process", "Thread Count", "_Total");

            return threadCount.NextValue();
        }

        /// <summary>
        /// Gets the total number of context switches (thread changes)
        /// </summary>
        /// <returns>Object</returns>
        private object GetContextSwitchesCount()
        {
            PerformanceCounter contextSwitchesCount = new PerformanceCounter("System", "Context Switches/sec", null);

            return contextSwitchesCount.NextValue();
        }

        /// <summary>
        /// Reports the number of handles that processes opened for objects they create
        /// </summary>
        /// <returns>Object</returns>
        private object GetHandlesCount()
        {
            PerformanceCounter threadHandlesCount = new PerformanceCounter("Process", "Handle Count", "_Total");

            return threadHandlesCount.NextValue();
        }

        /// <summary>
        /// Reports the number of system calls being serviced by the CPU per second
        /// </summary>
        /// <returns>Object</returns>
        private object GetSystemCallsCount()
        {
            PerformanceCounter systemCallsCount = new PerformanceCounter("System", "System Calls/sec", null);

            return systemCallsCount.NextValue();
        }

        /// <summary>
        /// Reports the total number of bytes read from the disk
        /// </summary>
        /// <returns>Object</returns>
        private object GetDiskReadBytesCount()
        {
            PerformanceCounter diskReadBytesCount = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");

            return diskReadBytesCount.NextValue();
        }

        /// <summary>
        /// Reports the total number of bytes written to the disk
        /// </summary>
        /// <returns>Object</returns>
        private object GetDiskWriteBytesCount()
        {
            PerformanceCounter diskWriteBytesCount = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");

            return diskWriteBytesCount.NextValue();
        }

        /// <summary>
        /// Reports the average time in reading operations from the disk
        /// </summary>
        /// <returns>Object</returns>
        private object GetDiskReadPerSeond()
        {
            PerformanceCounter diskReadPerSeond = new PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Read", "_Total");

            return diskReadPerSeond.NextValue();
        }

        /// <summary>
        /// Reports the average time in writing operations to the disk
        /// </summary>
        /// <returns>Object</returns>
        private object GetDiskWritePerSeond()
        {
            PerformanceCounter diskWritePerSeond = new PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Write", "_Total");

            return diskWritePerSeond.NextValue();
        }
    }
}
