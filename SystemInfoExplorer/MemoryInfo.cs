using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    /// <summary>
    /// \class MemoryInfo 
    /// Provides an overview about the overall memory installed on the computer.
    /// <seealso cref="MemoryBankInfo">
    /// </summary>
    public class MemoryInfo
    {
        /// <summary>
        /// Default constructors
        /// </summary>
        public MemoryInfo()
        {
        }

        /// <summary>
        /// Number of computer memory banks
        /// </summary>
        public int NoOfMemoryBanks { get; set; }

        /// <summary>
        /// The data width of the memory banks
        /// </summary>
        public int DataWidth { get; set; }

        /// <summary>
        /// The total memory size in bytes
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// The total memory size in mega-bytes
        /// </summary>
        public long TotalSizeMegaBytes { get; set; }

        /// <summary>
        /// It uses the information provided in a list of MemoryBankInfo instances in order to set the properties
        /// of the MemoryInfo class
        /// </summary>
        /// <param name="banks">List of memory bank info instances (List<MemoryBankInfo>)</param>
        /// <returns>0 if success, -1 if exception and -2 if input is null</returns>
        public int GetMemoryInfo(List<MemoryBankInfo> banks)
        {
            try
            {
                if (banks == null)
                    return -2;
                
                this.NoOfMemoryBanks = banks.Count;
                this.DataWidth       = banks[0].DataWidth;

                long size = 0;
                foreach (var item in banks)
                {                    
                    size += item.Capacity;
                }

                this.TotalSize = size;
                this.TotalSizeMegaBytes = size / (1024 * 1024);
#if DEBUG
                Console.WriteLine($"Size (bytes): {size}\nSize (Mb): {size / (1024 * 1024)}");
#endif

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
        /// Stringifies the properties of the MemoryInfo class.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append($"Number of Memory Banks: {NoOfMemoryBanks}\n");
            str.Append($"Memory Data Width: {DataWidth}\n");
            str.Append($"Memory Total Size (Bytes): {TotalSize}\n");
            str.Append($"Memory Total Size (MegaBytes): {TotalSizeMegaBytes}\n");

            return str.ToString();
        }
    }
}
