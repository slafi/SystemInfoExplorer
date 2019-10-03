using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoExplorer
{
    #region Enumerations

    /// <summary>
    /// \enumeration CPU_ARCHITECTURE
    /// An enumeration of CPU architectures as detailed in <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-processor"/>
    /// </summary>
    public enum CPU_ARCHITECTURE { X86 = 0, MIPS = 1, ALPHA = 2, POWERPC = 3, ARM = 5, IA64 = 6, X64 = 9, NONE = -1 }

    /// <summary>
    /// \enumeration CPU_STATUS
    /// An enumeration of CPU statuses as detailed in <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-processor"/>
    /// </summary>
    public enum CPU_STATUS { UNKNOWN = 0, ENABLED = 1, DISABLED_USER = 2, DISABLED_BIOS = 3, IDLE = 4, RESERVED = 5, OTHER = 7, NONE = -1 }

    /// <summary>
    /// \enumeration CPU_FAMILY
    /// An enumeration of CPU families as detailed in <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-processor"/>
    /// </summary>
    public enum CPU_FAMILY
    {
        Other = 1, Unknown = 2, _8086 = 3, _80286 = 4, Intel_80386 = 5, Intel_80486 = 6, _8087 = 7, _80287 = 8, _80387 = 9,
        _80487 = 10, Pentium_brand = 11, Pentium_Pro = 12, Pentium_II = 13, Pentium_MMX = 14, Celeron = 15, Pentium_II_Xeon = 16,
        Pentium_III = 17, M1_Family = 18, M2_Family = 19, K5_Family = 24, K6_Family = 25, K6_2 = 26, K6_3 = 27, AMD_Athlon = 28,
        AMD_Duron = 29, AMD29000 = 30, K6_2PLUS = 31, Power_PC = 32, Power_PC_601 = 33, Power_PC_603 = 34, Power_PC_603_PLUS = 35,
        Power_PC_604 = 36, Power_PC_620 = 37, Power_PC_X704 = 38, Power_PC_750 = 39, Alpha = 48, Alpha_21064 = 49, Alpha_21066 = 50,
        Alpha_21164 = 51, Alpha_21164PC = 52, Alpha_21164a = 53, Alpha_21264 = 54, Alpha_21364 = 55, MIPS = 64, MIPS_R4000 = 65,
        MIPS_R4200 = 66, MIPS_R4400 = 67, MIPS_R4600 = 68, MIPS_R10000 = 69, SPARC = 80, SuperSPARC = 81, MICROSPARC_II = 82,
        MICROSPARC_IIEP = 83, ULTRASPARC = 84, ULTRASPARC_II = 85, ULTRASPARC_IIi = 86, ULTRASPARC_III = 87, ULTRASPARC_IIIi = 88,
        _68040 = 96, _68XXX = 97, _68000 = 98, _68010 = 99, _68020 = 100, _68030 = 101, Hobbit = 112, Crusoe_TM5000 = 120,
        Crusoe_TM3000 = 121, Efficeon_TM8000 = 122, Weitek = 128, Itanium = 130, AMD_Athlon_64 = 131, AMD_Opteron = 132,
        PA_RISC = 144, PA_RISC_8500 = 145, PA_RISC_8000 = 146, PA_RISC_7300LC = 147, PA_RISC_7200 = 148, PA_RISC_7100LC = 149,
        PA_RISC_7100 = 150, V30 = 160, Pentium_III_Xeon = 176, Pentium_III_SpeedStep = 177, Pentium_4 = 178, Intel_Xeon = 179,
        AS400 = 180, Intel_Xeon_MP = 181, AMD_AthlonXP = 182, AMD_AthlonMP = 183, Intel_Itanium_2 = 184, Intel_Pentium_M = 185,
        K7 = 190, IBM390 = 200, G4 = 201, G5 = 202, G6 = 203, Z_Architecture = 204, i860 = 250, i960 = 251, SH_3 = 260,
        SH_4 = 261, ARM = 280, StrongARM = 281, _6X86 = 300, MediaGX = 301, MII = 302, WinChip = 320, DSP = 350, Video_Processor = 500,
        NONE = -1
    }

    /// <summary>
    /// \enumeration CPU_TYPE
    /// An enumeration of CPU types as detailed in <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-processor"/>
    /// </summary>
    public enum CPU_TYPE { OTHER = 1, UNKNOWN = 2, CENTRAL_PROCESSOR = 3, MATH_PROCESSOR = 4, DSP_PROCESSOR = 5, VIDEO_PROCESSOR = 6, NONE = -1 }

    /// <summary>
    /// \enumeration CPU_VOLTAGE
    /// An enumeration of CPU voltages as detailed in <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-processor"/>
    /// </summary>
    public enum CPU_VOLTAGE { UNKNOWN = 0, _5V = 1, _3_3V = 2, _2_9V = 4, NONE = -1 }

    /// <summary>
    /// \enumeration MEM_BANK_FORM_FACTOR
    /// An enumeration of memory bank form factors as detailed in <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-physicalmemory"/>
    /// </summary>
    public enum MEM_BANK_FORM_FACTOR
    {
        UNKNOWN = 0, OTHER = 1, SIP = 2, DIP = 3, ZIP = 4, SOJ = 5, PROPRIETARY = 6, SIMM = 7, DIMM = 8, TSOP = 9,
        PGA = 10, RIMM = 11, SODIMM = 12, SRIMM = 13, SMD = 14, SSMP = 15, QFP = 16, TQFP = 17, SOIC = 18, LCC = 19,
        PLCC = 20, BGA = 21, FPBGA = 22, LGA = 23
    }

    /// <summary>
    /// \enumeration VIDEO_ARCHITECTURE
    /// An enumeration of video controller architectures as detailed in <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-videocontroller"/>
    /// </summary>
    public enum VIDEO_ARCHITECTURE { OTHER = 1, UNKNOWN = 2, CGA = 3, EGA = 4, VGA = 5, SVGA = 6, MDA = 7, HGC = 8,
                                     MCGA = 9, _8514A = 10, XGA = 11, Linear_Frame_Buffer = 12, PC_98 = 160 }

    /// <summary>
    /// \enumeration VIDEO_MEMORY_TYPE
    /// An enumeration of video memory types as detailed in <see href="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-videocontroller"/>
    /// </summary>
    public enum VIDEO_MEMORY_TYPE { OTHER = 1, UKNOWN = 2, VRAM = 3, DRAM = 4, SRAM = 5, WRAM = 6, EDO_RAM = 7,
                                    Burst_Synchronous_DRAM = 8, Pipelined_Burst_SRAM = 9, CDRAM = 10, _3DRAM = 11,
                                    SDRAM = 12, SGRAM = 13 }
    #endregion

    /// <summary>
    /// \Globals
    /// A static class that holds all common global variables
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Default output filename
        /// </summary>
        public static string Output_Filename = "devices.txt";

        /// <summary>
        /// A flag indicating if the logging to an output filename is enabled
        /// </summary>
        public static bool Enable_File_Output = false;
    }
}
