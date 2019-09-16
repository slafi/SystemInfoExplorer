using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    /// <summary>
    /// \class VideoControllerInfo 
    /// captures the main properties of a video controller.
    /// It uses a subset of the properties defined in the WMI class: Win32_VideoController
    /// For more information, <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-videocontroller">Win32_PhysicalMemory class</see>
    /// </summary>
    public class VideoControllerInfo
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public VideoControllerInfo()
        {
        }

        /// <summary>
        /// Video controller identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Current resolution, color, and scan mode settings of the video controller.
        /// </summary>
        public string VideoModeDescription { get; set; }

        /// <summary>
        /// Free-form string describing the video processor.
        /// </summary>
        public string VideoProcessor { get; set; }

        /// <summary>
        /// Name of the scoping system.
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Description of the object.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Current status of the object. Various operational and nonoperational statuses can be defined.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Memory size of the video adapter.
        /// </summary>
        public int AdapterRAM { get; set; }

        /// <summary>
        /// Size of the system's color table. The device must have a color depth of 
        /// no more than 8 bits per pixel; otherwise, this property is not set.
        /// </summary>
        public int ColorTableEntries { get; set; }

        /// <summary>
        /// Name or identifier of the digital-to-analog converter (DAC) chip. 
        /// The character set of this property is alphanumeric.
        /// </summary>
        public string AdapterDACType { get; set; }

        /// <summary>
        /// Last error code reported by the logical device.
        /// </summary>
        public int LastErrorCode { get; set; }

        /// <summary>
        /// Maximum amount of memory supported in bytes.
        /// </summary>
        public int MaxMemorySupported { get; set; }

        /// <summary>
        /// Maximum number of directly addressable entities supportable by this controller.
        /// A value of 0 (zero) should be used if the number is unknown.
        /// </summary>
        public int MaxNumberControlled { get; set; }

        /// <summary>
        /// Maximum refresh rate of the video controller in hertz.
        /// </summary>
        public int MaxRefreshRate { get; set; }

        /// <summary>
        /// Minimum refresh rate of the video controller in hertz.
        /// </summary>
        public int MinRefreshRate { get; set; }

        /// <summary>
        /// Type of video architecture.
        /// </summary>
        public VIDEO_ARCHITECTURE VideoArchitecture { get; set; }

        /// <summary>
        /// Type of video memory.
        /// </summary>
        public VIDEO_MEMORY_TYPE VideoMemoryType { get; set; }

        /// <summary>
        /// Current video mode.
        /// </summary>
        public int VideoMode { get; set; }

        /// <summary>
        /// Number of bits used to display each pixel.
        /// </summary>
        public int CurrentBitsPerPixel { get; set; }

        /// <summary>
        /// Current number of horizontal pixels.
        /// </summary>
        public int CurrentHorizontalResolution { get; set; }

        /// <summary>
        /// Number of colors supported at the current resolution.
        /// </summary>
        public long CurrentNumberOfColors { get; set; }

        /// <summary>
        /// Number of columns for this video controller (if in character mode).
        /// </summary>
        public long CurrentNumberOfColumns { get; set; }

        /// <summary>
        /// Number of rows for this video controller (if in character mode). 
        /// </summary>
        public long CurrentNumberOfRows { get; set; }

        /// <summary>
        /// Frequency at which the video controller refreshes the image for the monitor.
        /// </summary>
        public int CurrentRefreshRate { get; set; }

        /// <summary>
        /// Current scan mode.
        /// </summary>
        public int CurrentScanMode { get; set; }

        /// <summary>
        /// Current number of vertical pixels.
        /// </summary>
        public int CurrentVerticalResolution { get; set; }

        /// <summary>
        /// Current number of device-specific pens.
        /// </summary>
        public int DeviceSpecificPens { get; set; }

        /// <summary>
        /// Dither type of the video controller.
        /// </summary>
        public int DitherType { get; set; }

        /// <summary>
        /// Last modification date and time of the currently installed video driver.
        /// </summary>
        public string DriverDate { get; set; }

        /// <summary>
        /// This function parses the management object structure to extract the video controller info fields.
        /// </summary>
        /// <param name="mgtObject">Management object containing the different video controller info fields</param>
        /// <returns>returns 0 if success, -1 if an exception occured</returns>
        public int GetVideoControllerInfo(ManagementObject mgtObject)
        {
            try
            {
                CurrentBitsPerPixel = (mgtObject["CurrentBitsPerPixel"] == null) ? -1 : int.Parse(mgtObject["CurrentBitsPerPixel"].ToString());
                CurrentHorizontalResolution = (mgtObject["CurrentHorizontalResolution"] == null) ? -1 : int.Parse(mgtObject["CurrentHorizontalResolution"].ToString());
                CurrentNumberOfColors = (mgtObject["CurrentNumberOfColors"] == null) ? -1 : long.Parse(mgtObject["CurrentNumberOfColors"].ToString());
                CurrentNumberOfColumns = (mgtObject["CurrentNumberOfColumns"] == null) ? -1 : long.Parse(mgtObject["CurrentNumberOfColumns"].ToString());
                CurrentNumberOfRows = (mgtObject["CurrentNumberOfRows"] == null) ? -1 : long.Parse(mgtObject["CurrentNumberOfRows"].ToString());
                CurrentRefreshRate = (mgtObject["CurrentRefreshRate"] == null) ? -1 : int.Parse(mgtObject["CurrentRefreshRate"].ToString());

                CurrentScanMode = (mgtObject["CurrentScanMode"] == null) ? -1 : int.Parse(mgtObject["CurrentScanMode"].ToString());
                CurrentVerticalResolution = (mgtObject["CurrentVerticalResolution"] == null) ? -1 : int.Parse(mgtObject["CurrentVerticalResolution"].ToString());
                DeviceSpecificPens = (mgtObject["DeviceSpecificPens"] == null) ? -1 : int.Parse(mgtObject["DeviceSpecificPens"].ToString());

                DitherType = (mgtObject["DitherType"] == null) ? -1 : int.Parse(mgtObject["DitherType"].ToString());

                Id = mgtObject["Name"].ToString();

                VideoModeDescription = mgtObject["VideoModeDescription"].ToString();
                VideoProcessor = mgtObject["VideoProcessor"].ToString();
                SystemName = mgtObject["SystemName"].ToString();
                Description = mgtObject["Description"].ToString();
                Status = mgtObject["Status"].ToString();

                AdapterRAM = (mgtObject["AdapterRAM"] == null) ? -1 : int.Parse(mgtObject["AdapterRAM"].ToString());
                ColorTableEntries = (mgtObject["ColorTableEntries"] == null) ? -1 : int.Parse(mgtObject["ColorTableEntries"].ToString());
                AdapterDACType = (mgtObject["AdapterDACType"] == null) ? "" : mgtObject["AdapterDACType"].ToString();

                LastErrorCode = (mgtObject["LastErrorCode"] == null) ? -1 : int.Parse(mgtObject["LastErrorCode"].ToString());
                MaxMemorySupported = (mgtObject["MaxMemorySupported"] == null) ? -1 : int.Parse(mgtObject["MaxMemorySupported"].ToString());
                MaxNumberControlled = (mgtObject["MaxNumberControlled"] == null) ? -1 : int.Parse(mgtObject["MaxNumberControlled"].ToString());
                MaxRefreshRate = (mgtObject["MaxRefreshRate"] == null) ? -1 : int.Parse(mgtObject["MaxRefreshRate"].ToString());
                MinRefreshRate = (mgtObject["MinRefreshRate"] == null) ? -1 : int.Parse(mgtObject["MinRefreshRate"].ToString());
                LastErrorCode = (mgtObject["LastErrorCode"] == null) ? -1 : int.Parse(mgtObject["LastErrorCode"].ToString());

                VideoArchitecture = GetVideoArchitecture((mgtObject["VideoArchitecture"] == null) ? 2 : int.Parse(mgtObject["VideoArchitecture"].ToString()));
                VideoMemoryType = GetVideoMemoryType((mgtObject["VideoMemoryType"] == null) ? 2 : int.Parse(mgtObject["VideoMemoryType"].ToString()));
                VideoMode = (mgtObject["VideoMode"] == null) ? -1 : int.Parse(mgtObject["VideoMode"].ToString());

                if (mgtObject["DriverDate"] != null)
                {
                    string str = mgtObject["DriverDate"].ToString().TrimEnd();
                    int year = int.Parse(str.Substring(0, 4));
                    int month = int.Parse(str.Substring(4, 2));
                    int day = int.Parse(str.Substring(6, 2));

                    int hour = int.Parse(str.Substring(8, 2));
                    int minute = int.Parse(str.Substring(10, 2));
                    int second = int.Parse(str.Substring(12, 2));

                    DateTime date = new DateTime(year, month, day, hour, minute, second);

                    DriverDate = date.ToLocalTime().ToString();
                }
                else
                {
                    DriverDate = "";
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
        /// This functions converts the video controller architecture from enumeration to string
        /// </summary>
        /// <param name="architecture">the video controller architecture (int)</param>
        /// <returns>the video controller architecture (string)</returns>
        protected VIDEO_ARCHITECTURE GetVideoArchitecture(int architecture)
        {
            return (VIDEO_ARCHITECTURE)architecture;
        }

        /// <summary>
        /// This functions converts the video controller architecture from enumeration to string
        /// </summary>
        /// <param name="memtype">the video controller architecture (int)</param>
        /// <returns>the video controller architecture (string)</returns>
        protected VIDEO_MEMORY_TYPE GetVideoMemoryType(int memtype)
        {
            return (VIDEO_MEMORY_TYPE)memtype;
        }

        /// <summary>
        /// Stringifies the properties of the VideoControllerInfo class.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append($"Name: {Id}\n");
            str.Append($"Video Architecture: {VideoArchitecture}\n");
            str.Append($"Video Memory Type: {VideoMemoryType}\n");
            str.Append($"Video Controller Status: {Status}\n");
            str.Append($"Adapter RAM (Bytes): {AdapterRAM}\n");
            str.Append($"Driver Date: {DriverDate}\n");
            str.Append($"Video Mode Description: {VideoModeDescription}\n");

            return str.ToString();
        }
    }
}
