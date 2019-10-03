# Project Description

This project uses the [Windows Management Instrumentation (WMI)](https://docs.microsoft.com/en-us/windows/win32/wmisdk/wmi-start-page) API in order to explore the hardware items installed in a computer. It also allows to instantly query a set of performance counters in order to retrieve the current state of some hardware parameters.

## Getting Started

The code is structured under the namespace `SystemInfoExplorer`. It is built into a DLL library that can be added to any Windows-compatible project.

### Hardware Explorer

The library allows to explore the platform environment and the hardware installed into a computer by querying a subset of WMI classes. It returns information about the following items:

| Item        | Class           | Corresponding WMI class  | Description  |
| ------------- | ------------- | ----- | ----- |
| CPU      | `CPUInfo` | `Win32_Processor` | Provides information about an installed CPU |
| Memory bank      | `MemoryBankInfo` | `Win32_PhysicalMemory` | Provides information about an individual memory bank |
| Memory      | `MemoryInfo` | None | Uses the class `MemoryBankInfo` to provide insights about the overall installed memory |
| Video controller      | `VideoControllerInfo` | `Win32_VideoController` | Provides information about an installed video controller |
| Disk partition      | `DiskPartition` |  `Win32_DiskPartition` | Provides information about an individual disk partition |
| Disk drive      | `DiskDriveInfo` | `Win32_DiskDrive` | Provides information about an installed disk drive |
| Platform      | `PlatformInfo` | None | Provides information about the operating system environment (e.g., OS, machine name, etc.) |


### Telemetry Information

The class `QuickSystemStats` allows to retrieve the current state of a subset of the system parameters (e.g., CPU usage, free memory size, etc.). The following table provides the list of available parameters:

| Parameter        | Unit | Device           | Description  |
| ------------- | :-----: | :-------------: | ----- |
| CpuUsage     | Percent (%) | CPU | The usage percentage of the CPU |
| ThreadCount     | - | CPU      |   The number of threads |
| HandlesCount | - | CPU     |    the number of handles |
| CPUContextSwitches | switches/second | CPU     |    The number of CPU context switches per second |
| SystemCallsCount | calls/second | CPU     |    The number of system calls per second |
| TotalMemSize | MegaBytes | Memory     |    The total size of available memory |
| FreeMemSize | MegaBytes | Memory     |    The size of the free memory |
| MemUsagePercent | Percent (%) | Memory     |    The percentage of memory usage |
| BytesReadFromDisk     | Bytes/second | Disk      |   The number of bytes read from disk per second |
| BytesWrittenToDisk     | Bytes/second | Disk      |   The number of bytes written to disk per second |
| AvgTimeDiskReadPerSecond | - | Disk     |    The average reading operations from disk per second |
| AvgTimeDiskWritePerSecond | - | Disk     |    The average writing operations to disk per second |


## Usage

To explore the hardware installed on the computer, the class `SystemInfoExplorer` should be instantiated as follows:

```c#
// Add namespace to the C# file
using SystemInfoExplorer;

// Instantiate the explorer class
Explorer explorer = new Explorer();

// If the results logging is required, the file output should be enabled (disabled by default)
SystemInfoExplorer.Globals.Enable_File_Output = true;

// The default log filename is "devices.txt". It can be altered if required as follows:
SystemInfoExplorer.Globals.Output_Filename = filename;

// Invoke the run method to retrieve information from WMI structure (this may take few seconds)
explorer.Run();
```

To retrieve the system current state, the `QuickSystemStats` class can be used as follows:

```c#
// Instantiate the QuickSystemStats class
QuickSystemStats counters = new QuickSystemStats();

// Invoke the method GetStats() to retrieve the current system parameters
counters.GetStats();

// Use the overridden method ToString() to display / log the query results
Console.WriteLine($"{counters.ToString()}");
```

## Deployment

To use the `SystemInfoExplorer` dynamic link library (DLL), you need to add a reference to your project.

## Built With

* [Microsoft Visual Studio](https://visualstudio.microsoft.com/) - Microsoft Visual Studio
* [.NET Framework 4.7](https://dotnet.microsoft.com/download/dotnet-framework/net47) - Dependency Management

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
