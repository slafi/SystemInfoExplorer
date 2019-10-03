using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    /// <summary>
    /// \class PlatformInfo 
    /// Captures the properties of the computer's platform.
    /// </summary>
    public class PlatformInfo
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PlatformInfo()
        {
        }

        /// <summary>
        /// A flag indicating the operating system is 64-bit
        /// </summary>
        public bool is64BitOperatingSystem;

        /// <summary>
        /// The current machine name
        /// </summary>
        public string machineName;

        /// <summary>
        /// The operating system name
        /// </summary>
        public OperatingSystem os;

        /// <summary>
        /// The platform identifier
        /// </summary>
        public PlatformID platform;

        /// <summary>
        /// The unstalled service pack
        /// </summary>
        public string servicePack;

        /// <summary>
        /// The operating system version
        /// </summary>
        public string version;

        /// <summary>
        /// The number of computer processors
        /// </summary>
        public int processorCount;

        /// <summary>
        /// The list of logical drives
        /// </summary>
        public string[] logicalDrives;

        /// <summary>
        /// A dictionary containing the list of enviroment variables
        /// </summary>
        public IDictionary envVars;

        /// <summary>
        /// CLR version
        /// </summary>
        public Version clrVersion;

        /// <summary>
        /// Gets the system information (including OS, logical drives, env. variables, etc.)
        /// </summary>
        /// <returns>0 if success, -1 if an exception had occured</returns>
        public int GetSystemInfo()
        {
            try
            {
#if DEBUG
                Console.WriteLine($"********** Platform General Information **********");                
#endif

                if(Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"********** Platform General Information **********\n", true);
                }

                // returns true on my PC as it is a 64-bit OS
                is64BitOperatingSystem = Environment.Is64BitOperatingSystem;

#if DEBUG
                Console.WriteLine("is64BitOperatingSystem: " + is64BitOperatingSystem);                
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"is64BitOperatingSystem: {is64BitOperatingSystem}\n", true);
                }

                // returns the machine name
                machineName = Environment.MachineName;
#if DEBUG
                Console.WriteLine("machineName: " + machineName);
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"machineName: {machineName}\n", true);
                }

                // returns information about the operating system version, build, major, minor etc.
                os = Environment.OSVersion;

#if DEBUG
                Console.WriteLine("OS: " + os);
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"OS: {os}\n", true);
                }

                // returns the platform id as an enumeration
                platform = os.Platform;

#if DEBUG
                Console.WriteLine("Platform: " + os.Platform);
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"Platform: {os.Platform}\n", true);
                }

                // the currently installed service pack
                servicePack = os.ServicePack;

#if DEBUG
                Console.WriteLine("ServicePack: " + os.ServicePack);
                
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"ServicePack: {os.ServicePack}\n", true);
                }

                // retrieve the current CLR version
                clrVersion = Environment.Version;

#if DEBUG
                Console.WriteLine("CLR version: " + clrVersion);
#endif

                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"CLR version: {clrVersion}\n", true);
                }

                //the toString version of the OS                
                version = os.VersionString;

#if DEBUG
                Console.WriteLine("VersionString: " + os.VersionString);
#endif
                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"VersionString: {os.VersionString}\n", true);
                }

                //I have 4 processors on this PC
                processorCount = Environment.ProcessorCount;

#if DEBUG
                Console.WriteLine("processorCount: " + processorCount);
#endif

                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"processorCount: {processorCount}\n", true);
                }

                // returns a list of logical drives: e.g., C: and D:
                logicalDrives = Environment.GetLogicalDrives();

                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"Logical Drives:\n", true);
                    foreach (string key in logicalDrives)
                    {
                        Tools.SaveData(Globals.Output_Filename, $"{key} |\t", true);
                    }
                    Tools.SaveData(Globals.Output_Filename, $"\n", true);
                }

                // this is how to find all environmental variables of the system and iterate through them
                envVars = Environment.GetEnvironmentVariables();

                if (Globals.Enable_File_Output)
                {
                    Tools.SaveData(Globals.Output_Filename, $"Environment variables:\n", true);
                    foreach (string key in envVars.Keys)
                    {
                        Debug.WriteLine(string.Concat("key: ", key, ": ", envVars[key]));

                        Tools.SaveData(Globals.Output_Filename, $"{string.Concat("\t", key, ": ", envVars[key])}\n", true);
                    }
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
    }
}
