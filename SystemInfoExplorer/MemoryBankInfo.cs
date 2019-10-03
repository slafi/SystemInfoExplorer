using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    /// <summary>
    /// \class MemoryBankInfo 
    /// Captures the main properties of a MemoryBankInfo structure.
    /// It uses a subset of the properties defined in the WMI class: Win32_PhysicalMemory
    /// For more information, <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-physicalmemory">Win32_PhysicalMemory class</see>
    /// </summary>
    public class MemoryBankInfo
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MemoryBankInfo()
        {
        }

        /// <summary>
        /// The memory bank info identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Physically labeled bank where the memory is located.
        /// </summary>
        public string BankLabel { get; set; }

        /// <summary>
        /// Data width of the physical memory—in bits.
        /// </summary>
        public int DataWidth { get; set; }

        /// <summary>
        /// Description of an object.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Label of the socket or circuit board that holds the memory.
        /// </summary>
        public string DeviceLocator { get; set; }

        /// <summary>
        /// Name of the organization responsible for producing the physical element.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Manufacturer-allocated number to identify the physical element.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Stock keeping unit number for the physical element.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// The raw SMBIOS memory type. 
        /// </summary>
        public int SMBIOSMemoryType { get; set; }

        /// <summary>
        /// Speed of the physical memory—in nanoseconds.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Current status of the object. 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Name for the physical element.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Additional data, beyond asset tag information, that can be used to identify a physical element.
        /// </summary>
        public string OtherIdentifyingInfo { get; set; }

        /// <summary>
        /// Part number assigned by the organization responsible for producing or manufacturing the physical element.
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// Unique identifier for the physical memory device that is represented by an instance of Win32_PhysicalMemory. 
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Total width, in bits, of the physical memory, including check or error correction bits. 
        /// </summary>
        public int TotalWidth { get; set; }

        /// <summary>
        /// Type of physical memory represented.
        /// </summary>
        public int TypeDetail { get; set; }

        /// <summary>
        /// Version of the physical element.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Position of the physical memory in a row.
        /// </summary>
        public int PositionInRow { get; set; }

        /// <summary>
        /// Implementation form factor for the chip.
        /// </summary>
        public MEM_BANK_FORM_FACTOR FormFactor { get; set; }

        /// <summary>
        /// Total capacity of the physical memory—in bytes.
        /// </summary>
        public long Capacity { get; set; }

        /// <summary>
        /// This function parses the management object structure to extract the memory bank info fields.
        /// </summary>
        /// <param name="mgtObject">Management object containing the different memory bank info fields</param>
        /// <returns>returns 0 if success, -1 if an exception occured</returns>
        public int GetMemInfo(ManagementObject mgtObject)
        {
            try
            {
                Capacity = long.Parse(mgtObject["Capacity"].ToString());

                Id = (mgtObject["Name"] == null) ? "" : mgtObject["Name"].ToString();
                BankLabel = (mgtObject["BankLabel"] == null) ? "" : mgtObject["BankLabel"].ToString();
                Description = (mgtObject["Description"] == null) ? "" : mgtObject["Description"].ToString();
                DeviceLocator = (mgtObject["DeviceLocator"] == null) ? "" : mgtObject["DeviceLocator"].ToString();
                Manufacturer = (mgtObject["Manufacturer"] == null) ? "" : mgtObject["Manufacturer"].ToString();

                SerialNumber = mgtObject["SerialNumber"].ToString();
                SKU = (mgtObject["SKU"] == null) ? "" : mgtObject["SKU"].ToString();
                Status = (mgtObject["Status"] == null) ? "" : mgtObject["Status"].ToString();
                Model = (mgtObject["Model"] == null) ? "" : mgtObject["Model"].ToString();
                OtherIdentifyingInfo = (mgtObject["OtherIdentifyingInfo"] == null) ? "" : mgtObject["OtherIdentifyingInfo"].ToString();
                PartNumber = (mgtObject["PartNumber"] == null) ? "" : mgtObject["PartNumber"].ToString();

                DataWidth = int.Parse(mgtObject["DataWidth"].ToString());
                Speed = int.Parse(mgtObject["Speed"].ToString());
                SMBIOSMemoryType = int.Parse(mgtObject["SMBIOSMemoryType"].ToString());

                Tag = (mgtObject["Tag"] == null) ? "" : mgtObject["Tag"].ToString();
                Version = (mgtObject["Version"] == null) ? "" : mgtObject["Version"].ToString();
                TotalWidth = int.Parse(mgtObject["TotalWidth"].ToString());
                TypeDetail = int.Parse(mgtObject["TypeDetail"].ToString());
                PositionInRow = (mgtObject["PositionInRow"] == null) ? -1 : int.Parse(mgtObject["PositionInRow"].ToString());
                FormFactor = GetMemBankFormFactor(int.Parse(mgtObject["FormFactor"].ToString()));

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
        /// This functions converts the memory bank form factor from enumeration to string
        /// </summary>
        /// <param name="formfactor">the memory bank form factor (int)</param>
        /// <returns>the memory bank form factor (string)</returns>
        protected MEM_BANK_FORM_FACTOR GetMemBankFormFactor(int formfactor)
        {
            return (MEM_BANK_FORM_FACTOR)formfactor;
        }
    }
}
