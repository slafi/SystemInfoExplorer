using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    /// <summary>
    /// \class DiskPartition 
    /// Captures the properties of the disk partitions installed on the computer.
    /// It uses a subset of the properties defined in the WMI class: Win32_DiskPartition
    /// For more information, <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-diskpartition">Win32_DiskPartition</see>
    /// </summary>
    public class DiskPartition
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DiskPartition()
        {
        }

        /// <summary>
        /// The disk partition identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Unique identifier of the disk drive and partition, from the rest of the system.
        /// </summary>
        public string DeviceID { get; set; }

        /// <summary>
        /// Total size of the partition.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Total number of consecutive blocks, each block the size of the value contained 
        /// in the BlockSize property, which form this storage extent.
        /// </summary>
        public long NumberOfBlocks { get; set; }

        /// <summary>
        /// Current status of the object. 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// State of the logical device.
        /// </summary>
        public string StatusInfo { get; set; }

        /// <summary>
        /// Creation class name of the scoping system.
        /// </summary>
        public string SystemCreationClassName { get; set; }

        /// <summary>
        /// Name of the scoping system.
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Type of the partition.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Indicates whether the computer can be booted from this partition.
        /// </summary>
        public bool Bootable { get; set; }

        /// <summary>
        /// Partition is the active partition. The operating system uses the active 
        /// partition when booting from a hard disk.
        /// </summary>
        public bool BootPartition { get; set; }

        /// <summary>
        /// If True, this is the primary partition.
        /// </summary>
        public bool PrimaryPartition { get; set; }

        /// <summary>
        /// If True, the partition information has changed.
        /// </summary>
        public bool RewritePartition { get; set; }

        /// <summary>
        /// This function parses the management object structure to extract the disk partition fields.
        /// </summary>
        /// <param name="mgtObject">Management object containing the different disk partition fields</param>
        /// <returns>returns 0 if success, -1 if an exception occured</returns>
        public int GetDiskPartitionInfo(ManagementObject mgtObject)
        {
            try
            {
                Id = mgtObject["Name"].ToString();
                DeviceID = mgtObject["DeviceID"].ToString();

                Size = (mgtObject["Size"] == null) ? -1 : long.Parse(mgtObject["Size"].ToString());
                NumberOfBlocks = (mgtObject["NumberOfBlocks"] == null) ? -1 : long.Parse(mgtObject["NumberOfBlocks"].ToString());

                Status = (mgtObject["Status"] == null) ? "" : mgtObject["Status"].ToString();
                SystemCreationClassName = mgtObject["SystemCreationClassName"].ToString();
                SystemName = mgtObject["SystemName"].ToString();
                SystemName = mgtObject["Type"].ToString();

                if(mgtObject["Bootable"] != null)
                {
                    bool temp;
                    Boolean.TryParse(mgtObject["Bootable"].ToString(), out temp);
                    Bootable = temp;
                }

                if (mgtObject["BootPartition"] != null)
                {
                    bool temp;
                    Boolean.TryParse(mgtObject["BootPartition"].ToString(), out temp);
                    BootPartition = temp;
                }

                if (mgtObject["PrimaryPartition"] != null)
                {
                    bool temp;
                    Boolean.TryParse(mgtObject["PrimaryPartition"].ToString(), out temp);
                    PrimaryPartition = temp;
                }

                if (mgtObject["RewritePartition"] != null)
                {
                    bool temp;
                    Boolean.TryParse(mgtObject["RewritePartition"].ToString(), out temp);
                    RewritePartition = temp;
                }
                
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
        /// Stringifies the properties of the DiskPartition class.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append($"Name: {Id}\n");
            str.Append($"Size (Bytes): {Size}\n");
            str.Append($"Number Of Blocks: {NumberOfBlocks}\n");

            if(Status != String.Empty)
                str.Append($"Partition Status: {Status}\n");
            
            str.Append($"Primary Partition: {PrimaryPartition}\n");

            return str.ToString();
        }


    }
}
