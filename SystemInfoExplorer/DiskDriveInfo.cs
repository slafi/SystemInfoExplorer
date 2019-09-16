using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    /// <summary>
    /// \class DiskDriveInfo 
    /// Captures the properties of the disk drives installed on the computer.
    /// It uses a subset of the properties defined in the WMI class: Win32_DiskDrive
    /// For more information, <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-diskdrive">Win32_DiskDrive</see>
    /// </summary>
    public class DiskDriveInfo
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DiskDriveInfo()
        {
        }

        /// <summary>
        /// Disk drive identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Unique identifier of the disk drive with other devices on the system.
        /// </summary>
        public string DeviceID { get; set; }

        /// <summary>
        /// Manufacturer's model number of the disk drive.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Number allocated by the manufacturer to identify the physical media.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Number allocated by the manufacturer to identify the physical media.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Disk identification. This property can be used to identify a shared resource.
        /// </summary>
        public int Signature { get; set; }

        /// <summary>
        /// Current status of the object.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// State of the logical device. 
        /// </summary>
        public string StatusInfo { get; set; }

        /// <summary>
        /// Value of the scoping computer's CreationClassName property.
        /// </summary>
        public string SystemCreationClassName { get; set; }

        /// <summary>
        /// Name of the scoping system.
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Total number of cylinders on the physical disk drive.
        /// </summary>
        public long TotalCylinders { get; set; }

        /// <summary>
        /// Total number of heads on the disk drive.
        /// </summary>
        public long TotalHeads { get; set; }

        /// <summary>
        /// Total number of sectors on the physical disk drive.
        /// </summary>
        public long TotalSectors { get; set; }

        /// <summary>
        /// Total number of tracks on the physical disk drive.
        /// </summary>
        public long TotalTracks { get; set; }

        /// <summary>
        /// Size of the disk drive.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Maximum number of media which can be supported or inserted.
        /// </summary>
        public int NumberOfMediaSupported { get; set; }

        /// <summary>
        /// Number of partitions on this physical disk drive that are 
        /// recognized by the operating system.
        /// </summary>
        public int Partitions { get; set; }

        /// <summary>
        /// Number of tracks in each cylinder on the physical disk drive.
        /// </summary>
        public int TracksPerCylinder { get; set; }

        /// <summary>
        /// This function parses the management object structure to extract the disk drive info fields.
        /// </summary>
        /// <param name="mgtObject">Management object containing the different disk drive info fields</param>
        /// <returns>returns 0 if success, -1 if an exception occured</returns>
        public int GetDiskDriveInfo(ManagementObject mgtObject)
        {
            try
            {
                Id = mgtObject["Name"].ToString();
                DeviceID = mgtObject["DeviceID"].ToString();
                Model = mgtObject["Model"].ToString();
                Manufacturer = mgtObject["Manufacturer"].ToString();

                SerialNumber = mgtObject["SerialNumber"].ToString().Trim();
                Status = mgtObject["Status"].ToString();
                SystemCreationClassName = mgtObject["SystemCreationClassName"].ToString();
                SystemName = mgtObject["SystemName"].ToString();

                TotalCylinders = (mgtObject["TotalCylinders"] == null) ? -1 : long.Parse(mgtObject["TotalCylinders"].ToString());
                TotalHeads = (mgtObject["TotalHeads"] == null) ? -1 : long.Parse(mgtObject["TotalHeads"].ToString());
                TotalSectors = (mgtObject["TotalSectors"] == null) ? -1 : long.Parse(mgtObject["TotalSectors"].ToString());
                TotalTracks = (mgtObject["TotalTracks"] == null) ? -1 : long.Parse(mgtObject["TotalTracks"].ToString());
                Size = (mgtObject["Size"] == null) ? -1 : long.Parse(mgtObject["Size"].ToString());

                NumberOfMediaSupported = (mgtObject["NumberOfMediaSupported"] == null) ? -1 : int.Parse(mgtObject["NumberOfMediaSupported"].ToString());
                Partitions = (mgtObject["Partitions"] == null) ? -1 : int.Parse(mgtObject["Partitions"].ToString());
                StatusInfo = GetStatusInfo((mgtObject["StatusInfo"] == null) ? -1 : int.Parse(mgtObject["StatusInfo"].ToString()));
                TracksPerCylinder = (mgtObject["TracksPerCylinder"] == null) ? -1 : int.Parse(mgtObject["TracksPerCylinder"].ToString());
                //Signature = (mgtObject["Signature"] == null) ? -1 : int.Parse(mgtObject["Signature"].ToString());

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
        /// This functions converts the disk drive status from enumeration to string
        /// </summary>
        /// <param name="status">the disk drive status (int)</param>
        /// <returns>the disk drive status (string)</returns>
        protected string GetStatusInfo(int status)
        {
            switch (status)
            {
                case 1:
                    return "OTHER";
                case 2:
                    return "UNKNWON";
                case 3:
                    return "ENABLED";
                case 4:
                    return "DISABLED";
                case 5:
                    return "NOT APPLICABLE";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Stringifies the properties of the CPUInfo class.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append($"Name: {Id}\n");

            if (Manufacturer != String.Empty)
                str.Append($"Manufacturer: {Manufacturer}\n");

            if(SerialNumber != String.Empty)
                str.Append($"SerialNumber: {SerialNumber}\n");

            if (Model != String.Empty)
                str.Append($"Model: {Model}\n");

            if (Size >= 0)
                str.Append($"Size (Bytes): {Size}\n");

            str.Append($"Partitions: {Partitions}\n");

            if (Status != String.Empty)
                str.Append($"Status: {Status}\n");

            return str.ToString();
        }

    }
}
