using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    /// <summary>
    /// \class CPUInfo 
    /// Captures the properties of the CPUs installed on the computer.
    /// It uses a subset of the properties defined in the WMI class: Win32_Processor
    /// For more information, <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-processor">Win32_Processor</see>
    /// </summary>
    public class CPUInfo
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CPUInfo()
        {
        }                

        /// <summary>
        /// CPU identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// On a 32-bit operating system, the value is 32 and on a 64-bit operating system it is 64.
        /// </summary>
        public int AddressWidth { get; set; }

        /// <summary>
        /// Processor architecture used by the platform.
        /// </summary>
        public CPU_ARCHITECTURE Architecture { get; set; }

        /// <summary>
        /// Current status of the processor. Status changes indicate processor usage, 
        /// but not the physical condition of the processor.
        /// </summary>
        public CPU_STATUS CpuStatus { get; set; }

        /// <summary>
        /// On a 32-bit processor, the value is 32 and on a 64-bit processor it is 64.
        /// </summary>
        public int DataWidth { get; set; }

        /// <summary>
        /// Unique identifier of a processor on the system.
        /// </summary>
        public string DeviceID { get; set; }

        /// <summary>
        /// Processor family type.
        /// </summary>
        public CPU_FAMILY Family { get; set; }

        /// <summary>
        /// Name of the processor manufacturer.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Maximum speed of the processor, in MHz.
        /// </summary>
        public int MaxClockSpeed { get; set; }

        /// <summary>
        /// The part number of this processor as set by the manufacturer.
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// The serial number of this processor This value is set by the manufacturer and normally not changeable.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Globally unique identifier for the processor. This identifier may only be unique within a processor family.
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>
        /// Primary function of the processor.
        /// </summary>
        public int ProcessorType { get; set; }

        /// <summary>
        /// Processor information that describes the processor features.
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// Load capacity of each processor, averaged to the last second. 
        /// Processor loading refers to the total computing burden for each 
        /// processor at one time.
        /// </summary>
        public int LoadPercentage { get; set; }

        /// <summary>
        /// Current speed of the processor, in MHz.
        /// </summary>
        public int CurrentClockSpeed { get; set; }

        /// <summary>
        /// Voltage of the processor. 
        /// </summary>
        public CPU_VOLTAGE CurrentVoltage { get; set; }

        /// <summary>
        /// Number of cores for the current instance of the processor. 
        /// A core is a physical processor on the integrated circuit. 
        /// </summary>
        public int NumberOfCores { get; set; }

        /// <summary>
        /// The number of enabled cores per processor socket.
        /// </summary>
        public int NumberOfEnabledCore { get; set; }

        /// <summary>
        /// Number of logical processors for the current instance of the processor. 
        /// For processors capable of hyperthreading, this value includes only the 
        /// processors which have hyperthreading enabled.
        /// </summary>
        public int NumberOfLogicalProcessors { get; set; }

        /// <summary>
        /// System revision level that depends on the architecture. 
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Size of the Level 2 processor cache. A Level 2 cache is an external 
        /// memory area that has a faster access time than the main RAM memory.
        /// </summary>
        public int L2CacheSize { get; set; }

        /// <summary>
        /// Clock speed of the Level 2 processor cache. A Level 2 cache is an external 
        /// memory area that has a faster access time than the main RAM memory.
        /// </summary>
        public int L2CacheSpeed { get; set; }

        /// <summary>
        /// Size of the Level 3 processor cache. A Level 3 cache is an external 
        /// memory area that has a faster access time than the main RAM memory.
        /// </summary>
        public int L3CacheSize { get; set; }

        /// <summary>
        /// Clockspeed of the Level 3 property cache. A Level 3 cache is an external 
        /// memory area that has a faster access time than the main RAM memory.
        /// </summary>
        public int L3CacheSpeed { get; set; }

        /// <summary>
        /// The number of threads per processor socket.
        /// </summary>
        public int ThreadCount { get; set; }

        /// <summary>
        /// If True, the Firmware has enabled virtualization extensions.
        /// </summary>
        public string VirtualizationFirmwareEnabled { get; set; }

        /// <summary>
        /// This function parses the management object structure to extract the CPU info fields.
        /// </summary>
        /// <param name="mgtObject">Management object containing the different CPU info fields</param>
        /// <returns>returns 0 if success, -1 if an exception occured</returns>
        public int GetCpuInfo(ManagementObject mgtObject)
        {
            try
            {
                Id = (mgtObject["Name"] == null) ? "" : Regex.Replace(mgtObject["Name"].ToString(), @"\s+", " ");
                AddressWidth = int.Parse(mgtObject["AddressWidth"].ToString());
                CpuStatus = GetCpuStatus(int.Parse(mgtObject["CpuStatus"].ToString()));
                DataWidth = int.Parse(mgtObject["DataWidth"].ToString());
                DeviceID = mgtObject["DeviceID"].ToString();
                Family = GetCpuFamily(int.Parse(mgtObject["Family"].ToString()));
                Manufacturer = mgtObject["Manufacturer"].ToString();
                MaxClockSpeed = int.Parse(mgtObject["MaxClockSpeed"].ToString());
                CurrentClockSpeed = int.Parse(mgtObject["CurrentClockSpeed"].ToString());
                PartNumber = mgtObject["PartNumber"].ToString();
                SerialNumber = mgtObject["SerialNumber"].ToString().Trim();
                UniqueId = mgtObject["UniqueId"] == null ? "" : mgtObject["UniqueId"].ToString();
                ProcessorType = int.Parse(mgtObject["ProcessorType"].ToString());
                ProcessorId = mgtObject["ProcessorId"].ToString();
                LoadPercentage = int.Parse(mgtObject["LoadPercentage"].ToString());
                Architecture = GetCpuArchitecture(int.Parse(mgtObject["Architecture"].ToString()));

                CurrentVoltage = (mgtObject["CurrentVoltage"] == null) ? CPU_VOLTAGE.UNKNOWN : GetCpuCurrentVoltage(int.Parse(mgtObject["CurrentVoltage"].ToString()));
                NumberOfLogicalProcessors = int.Parse(mgtObject["NumberOfLogicalProcessors"].ToString());
                NumberOfCores = int.Parse(mgtObject["NumberOfCores"].ToString());
                NumberOfEnabledCore = int.Parse(mgtObject["NumberOfEnabledCore"].ToString());

                Level = int.Parse(mgtObject["Level"].ToString());
                L2CacheSize = (mgtObject["L2CacheSize"] == null) ? -1 : int.Parse(mgtObject["L2CacheSize"].ToString());
                L2CacheSpeed = (mgtObject["L2CacheSpeed"] == null) ? -1 : int.Parse(mgtObject["L2CacheSpeed"].ToString());
                L3CacheSize = (mgtObject["L3CacheSize"] == null) ? -1 : int.Parse(mgtObject["L3CacheSize"].ToString());
                L3CacheSpeed = (mgtObject["L3CacheSpeed"] == null) ? -1 : int.Parse(mgtObject["L3CacheSpeed"].ToString());

                ThreadCount = (mgtObject["ThreadCount"] == null) ? -1 : int.Parse(mgtObject["ThreadCount"].ToString());

                bool virtualFlag = false;
                Boolean.TryParse(mgtObject["VirtualizationFirmwareEnabled"].ToString(), out virtualFlag);
                VirtualizationFirmwareEnabled = ((bool)virtualFlag) ? "ENABLED" : "DISABLED";

                return 0;
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
        /// Stringifies the properties of the CPUInfo class.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append($"Device ID: {DeviceID}\n");
            str.Append($"Name: {Id}\n");
            str.Append($"Current Clock Speed (MHz): {CurrentClockSpeed}, Max. Clock Speed (MHz): {MaxClockSpeed}\n");
            str.Append($"Architecture: {Architecture}\n");

            if (Manufacturer != String.Empty)
                str.Append($"Manufacturer: {Manufacturer}\n");

            str.Append($"NumberOfCores: {NumberOfCores}\n");
            str.Append($"Number Of Logical Processors: {NumberOfLogicalProcessors}\n");
            str.Append($"Number Of Enabled Core: {NumberOfEnabledCore}\n");

            return str.ToString();
        }

        /// <summary>
        /// This functions converts the CPU architecture from enumeration to string
        /// </summary>
        /// <param name="architecture">the CPU architecture (int)</param>
        /// <returns>the CPU architecture (string)</returns>
        protected CPU_ARCHITECTURE GetCpuArchitecture(int architecture)
        {
            switch (architecture)
            {
                case 0:
                    return CPU_ARCHITECTURE.X86;
                case 1:
                    return CPU_ARCHITECTURE.MIPS;
                case 2:
                    return CPU_ARCHITECTURE.ALPHA;
                case 3:
                    return CPU_ARCHITECTURE.POWERPC;
                case 5:
                    return CPU_ARCHITECTURE.ARM;
                case 6:
                    return CPU_ARCHITECTURE.IA64;
                case 9:
                    return CPU_ARCHITECTURE.X64;
                default:
                    return CPU_ARCHITECTURE.NONE;
            }
        }

        /// <summary>
        /// This functions converts the CPU status from enumeration to string
        /// </summary>
        /// <param name="status">the CPU status (int)</param>
        /// <returns>the CPU status (string)</returns>
        protected CPU_STATUS GetCpuStatus(int status)
        {
            switch (status)
            {
                case 0:
                    return CPU_STATUS.UNKNOWN;
                case 1:
                    return CPU_STATUS.ENABLED;
                case 2:
                    return CPU_STATUS.DISABLED_USER;
                case 3:
                    return CPU_STATUS.DISABLED_BIOS;
                case 4:
                    return CPU_STATUS.IDLE;
                case 5:
                case 6:
                    return CPU_STATUS.RESERVED;
                case 7:
                    return CPU_STATUS.OTHER;
                default:
                    return CPU_STATUS.NONE;
            }
        }

        /// <summary>
        /// This functions converts the CPU family from enumeration to string
        /// </summary>
        /// <param name="family">the CPU family (int)</param>
        /// <returns>the CPU family (string)</returns>
        protected CPU_FAMILY GetCpuFamily(int family)
        {
            return (CPU_FAMILY)family;
        }

        /// <summary>
        /// This functions converts the CPU current voltage from enumeration to string
        /// </summary>
        /// <param name="voltage">the CPU current voltage (int)</param>
        /// <returns>the CPU current voltage (string)</returns>
        protected CPU_VOLTAGE GetCpuCurrentVoltage(int voltage)
        {
            return (CPU_VOLTAGE)voltage;
        }

    }

    
}
