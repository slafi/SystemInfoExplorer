using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    /// <summary>
    /// \class Explorer
    /// A class that instantiates and calls all the classes in charge of discovering the installed hardware / software.
    /// </summary>
    public class Explorer
    {
        /// <summary>
        /// Default construct which initializes all hardware objects
        /// </summary>
        public Explorer()
        {
            System_CPUs = new List<CPUInfo>();
            System_Memory = new List<MemoryBankInfo>();
            System_VideoControllers = new List<VideoControllerInfo>();
            PlatformInformation = new PlatformInfo();
            MemoryInformation = new MemoryInfo();
            System_DiskDrives = new List<DiskDriveInfo>();
            System_DiskPartitions = new List<DiskPartition>();
        }

        /// <summary>
        /// The platform information
        /// </summary>
        public PlatformInfo PlatformInformation { get; set; }

        /// <summary>
        /// List of system CPUs
        /// </summary>
        public List<CPUInfo> System_CPUs { get; set; }

        /// <summary>
        /// List of memory banks information
        /// </summary>
        public List<MemoryBankInfo> System_Memory { get; set; }

        /// <summary>
        /// General information about installed memory
        /// </summary>
        public MemoryInfo MemoryInformation { get; set; }
        
        /// <summary>
        /// List of video controller information
        /// </summary>
        public List<VideoControllerInfo> System_VideoControllers { get; set; }

        /// <summary>
        /// List of disk drives information
        /// </summary>
        public List<DiskDriveInfo> System_DiskDrives { get; set; }

        /// <summary>
        /// List of disk partitions' information
        /// </summary>
        public List<DiskPartition> System_DiskPartitions { get; set; }

        /// <summary>
        /// This functions queries the different software / hardware records in order to get their properties
        /// </summary>
        /// <returns>0 if success and -1 if an exception arises</returns>
        public int Run()
        {
            try
            {
                int error = 0;

                // Gather generic system information
                PlatformInformation.GetSystemInfo();

                // Get specific CPU information
                GetCPUInfo();

                // Get memory information
                GetMemoryInfo();

                // Compute memory parameters
                MemoryInformation.GetMemoryInfo(System_Memory);
#if DEBUG
                Console.WriteLine(MemoryInformation.ToString());
#endif

                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.output_filename, $"{MemoryInformation.ToString()}\n", true);
                }

                // Get video controller information
                GetVideoControllerInfo();

                // Get disk information
                GetDiskDriveInfo();

                // Get disk partitions
                GetDiskPartitionInfo();

                return error;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"Exception Message: {ex.Message}");
#endif
                return -1;
            }
        }

        /// <summary>
        /// This function queries the Win32_Processor in order to extract the properties of all installed CPUs
        /// <seealso cref="CPUInfo"/>
        /// </summary>
        public void GetCPUInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            ManagementObjectCollection objCol = searcher.Get();

#if DEBUG
            Console.WriteLine($"\n********** Processor Info **********");
            if (objCol != null)
            {
                Console.WriteLine($"Detected CPUs: {objCol.Count}");
            }
#endif

            if (Globals.Enable_File_Output)
            {
                Tools.SaveData(Globals.output_filename, $"\n********** Processor Info **********\n", true);
                if (objCol != null)
                {
                    Tools.SaveData(Globals.output_filename, $"\nDetected CPUs: {objCol.Count}\n", true);
                }
            }

            foreach (ManagementObject mgtObject in objCol)
            {
                CPUInfo cpu = new CPUInfo();
                cpu.GetCpuInfo(mgtObject);

                System_CPUs.Add(cpu);

#if DEBUG
                Console.WriteLine(cpu.ToString());
                Tools.SaveData(Globals.output_filename, $"{cpu.ToString()}\n", true);
#endif

            }
        }

        /// <summary>
        /// This function queries the Win32_PhysicalMemory in order to extract the properties of all installed memory banks
        /// <seealso cref="MemoryBankInfo"/>
        /// </summary>
        public void GetMemoryInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectCollection objCol = searcher.Get();

#if DEBUG
            Console.WriteLine($"\n********** Memory Info **********");
            if (objCol != null)
            {
                Console.WriteLine($"Detected memory banks: {objCol.Count}");
            }
#endif

            if (Globals.Enable_File_Output)
            {
                Tools.SaveData(Globals.output_filename, $"\n********** Memory Info **********\n", true);
                if (objCol != null)
                {
                    Tools.SaveData(Globals.output_filename, $"\nDetected memory banks: {objCol.Count}\n", true);
                }
            }

            foreach (ManagementObject mgtObject in objCol)
            {
                MemoryBankInfo mem = new MemoryBankInfo();
                mem.GetMemInfo(mgtObject);                

                System_Memory.Add(mem);
#if DEBUG
                Console.WriteLine(mem.ToString());
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.output_filename, $"{mem.ToString()}\n", true);
                }
            }

        }

        /// <summary>
        /// This function queries the Win32_VideoController in order to extract the properties of all installed video controllers
        /// <seealso cref="VideoControllerInfo"/>
        /// </summary>
        public void GetVideoControllerInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            ManagementObjectCollection objCol = searcher.Get();

#if DEBUG
            Console.WriteLine($"\n********** Video Controllers **********");
            if (objCol != null)
            {
                Console.WriteLine($"\nDetected video controllers: {objCol.Count}");
            }
#endif

            if (Globals.Enable_File_Output)
            {
                Tools.SaveData(Globals.output_filename, $"\n********** Video Controllers **********\n", true);
                if (objCol != null)
                {
                    Tools.SaveData(Globals.output_filename, $"\nDetected video controllers: {objCol.Count}\n", true);
                }
            }

            foreach (ManagementObject mgtObject in objCol)
            {
                VideoControllerInfo vid = new VideoControllerInfo();
                vid.GetVideoControllerInfo(mgtObject);

                System_VideoControllers.Add(vid);

#if DEBUG
                Console.WriteLine(vid.ToString());
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.output_filename, $"{vid.ToString()}\n", true);
                }
            }
        }

        /// <summary>
        /// This function queries the Win32_DiskDrive in order to extract the properties of all installed disk drives
        /// <seealso cref="DiskDriveInfo"/>
        /// </summary>
        public void GetDiskDriveInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            ManagementObjectCollection objCol = searcher.Get();

#if DEBUG
            Console.WriteLine($"\n********** Disk Drives **********");
            if (objCol != null)
            {
                Console.WriteLine($"Detected disk drives: {objCol.Count}");
            }
#endif

            if (Globals.Enable_File_Output)
            {
                Tools.SaveData(Globals.output_filename, $"\n********** Disk Drives **********\n", true);
                if (objCol != null)
                {
                    Tools.SaveData(Globals.output_filename, $"\nDetected disk drives: {objCol.Count}\n", true);
                }
            }

            foreach (ManagementObject mgtObject in objCol)
            {
                DiskDriveInfo disk = new DiskDriveInfo();
                disk.GetDiskDriveInfo(mgtObject);

                System_DiskDrives.Add(disk);

#if DEBUG
                Console.WriteLine(disk.ToString());
                Tools.SaveData(Globals.output_filename, $"{disk.ToString()}\n", true);
#endif
            }

        }

        /// <summary>
        /// This function queries the Win32_DiskPartition in order to extract the properties of all available disk partitions
        /// <seealso cref="DiskPartition"/>
        /// </summary>
        public void GetDiskPartitionInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskPartition");
            ManagementObjectCollection objCol = searcher.Get();


#if DEBUG
            Console.WriteLine($"\n********** Disk Partitions **********");
            if (objCol != null)
            {
                Console.WriteLine($"Detected partitions: {objCol.Count}");
            }
#endif
            if (Globals.Enable_File_Output)
            {
                Tools.SaveData(Globals.output_filename, $"\n********** Disk Partitions **********\n", true);
                if (objCol != null)
                {
                    Tools.SaveData(Globals.output_filename, $"\nDetected partitions: {objCol.Count}\n", true);
                }
            }

            foreach (ManagementObject mgtObject in objCol)
            {
                DiskPartition partition = new DiskPartition();
                partition.GetDiskPartitionInfo(mgtObject);

                System_DiskPartitions.Add(partition);

#if DEBUG
                Console.WriteLine(partition.ToString());
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.output_filename, $"{partition.ToString()}\n", true);
                }
            }

        }

    }
}
