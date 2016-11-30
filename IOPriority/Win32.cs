
using System;
using System.Runtime.InteropServices;

namespace IOPriority
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct STARTUPINFO
    {
        public Int32 cb;
        public string lpReserved;
        public string lpDesktop;
        public string lpTitle;
        public Int32 dwX;
        public Int32 dwY;
        public Int32 dwXSize;
        public Int32 dwYSize;
        public Int32 dwXCountChars;
        public Int32 dwYCountChars;
        public Int32 dwFillAttribute;
        public Int32 dwFlags;
        public Int16 wShowWindow;
        public Int16 cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public int dwProcessId;
        public int dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class SECURITY_ATTRIBUTES
    {
        public int nLength;
        public unsafe byte* lpSecurityDescriptor;
        public int bInheritHandle;
    }

    public enum PROCESS_INFORMATION_CLASS : int
    {
        ProcessBasicInformation = 0,
        ProcessQuotaLimits,
        ProcessIoCounters,
        ProcessVmCounters,
        ProcessTimes,
        ProcessBasePriority,
        ProcessRaisePriority,
        ProcessDebugPort,
        ProcessExceptionPort,
        ProcessAccessToken,
        ProcessLdtInformation,
        ProcessLdtSize,
        ProcessDefaultHardErrorMode,
        ProcessIoPortHandlers,
        ProcessPooledUsageAndLimits,
        ProcessWorkingSetWatch,
        ProcessUserModeIOPL,
        ProcessEnableAlignmentFaultFixup,
        ProcessPriorityClass,
        ProcessWx86Information,
        ProcessHandleCount,
        ProcessAffinityMask,
        ProcessPriorityBoost,
        ProcessDeviceMap,
        ProcessSessionInformation,
        ProcessForegroundInformation,
        ProcessWow64Information,
        ProcessImageFileName,
        ProcessLUIDDeviceMapsEnabled,
        ProcessBreakOnTermination,
        ProcessDebugObjectHandle,
        ProcessDebugFlags,
        ProcessHandleTracing,
        ProcessIoPriority,
        ProcessExecuteFlags,
        ProcessResourceManagement,
        ProcessCookie,
        ProcessImageInformation,
        ProcessCycleTime,
        ProcessPagePriority,
        ProcessInstrumentationCallback,
        ProcessThreadStackAllocation,
        ProcessWorkingSetWatchEx,
        ProcessImageFileNameWin32,
        ProcessImageFileMapping,
        ProcessAffinityUpdateMode,
        ProcessMemoryAllocationMode,
        MaxProcessInfoClass
    }

    public enum STANDARD_RIGHTS : uint
    {
        WRITE_OWNER = 524288,
        WRITE_DAC = 262144,
        READ_CONTROL = 131072,
        DELETE = 65536,
        SYNCHRONIZE = 1048576,
        STANDARD_RIGHTS_REQUIRED = 983040,
        STANDARD_RIGHTS_WRITE = READ_CONTROL,
        STANDARD_RIGHTS_EXECUTE = READ_CONTROL,
        STANDARD_RIGHTS_READ = READ_CONTROL,
        STANDARD_RIGHTS_ALL = 2031616,
        SPECIFIC_RIGHTS_ALL = 65535,
        ACCESS_SYSTEM_SECURITY = 16777216,
        MAXIMUM_ALLOWED = 33554432,
        GENERIC_WRITE = 1073741824,
        GENERIC_EXECUTE = 536870912,
        GENERIC_READ = UInt16.MaxValue,
        GENERIC_ALL = 268435456
    }

    public enum PROCESS_RIGHTS : uint
    {
        PROCESS_TERMINATE = 1,
        PROCESS_CREATE_THREAD = 2,
        PROCESS_SET_SESSIONID = 4,
        PROCESS_VM_OPERATION = 8,
        PROCESS_VM_READ = 16,
        PROCESS_VM_WRITE = 32,
        PROCESS_DUP_HANDLE = 64,
        PROCESS_CREATE_PROCESS = 128,
        PROCESS_SET_QUOTA = 256,
        PROCESS_SET_INFORMATION = 512,
        PROCESS_QUERY_INFORMATION = 1024,
        PROCESS_SUSPEND_RESUME = 2048,
        PROCESS_QUERY_LIMITED_INFORMATION = 4096,
        PROCESS_ALL_ACCESS = STANDARD_RIGHTS.STANDARD_RIGHTS_REQUIRED | STANDARD_RIGHTS.SYNCHRONIZE | 65535
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_BASIC_INFORMATION
    {
        public int ExitStatus;
        public int PebBaseAddress;
        public int AffinityMask;
        public int BasePriority;
        public int UniqueProcessId;
        public int InheritedFromUniqueProcessId;
        public int Size
        {
            get { return (6 * 4); }
        }
    };

    class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetPriorityClass(IntPtr handle, uint priorityClass);

        [DllImport("KERNEL32.DLL")]
        public static extern int
            OpenProcess(PROCESS_RIGHTS dwDesiredAccess, bool bInheritHandle, int
            dwProcessId);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtSetInformationProcess(int processHandle,
           PROCESS_INFORMATION_CLASS processInformationClass, IntPtr processInformation, uint processInformationLength);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtQueryInformationProcess(int processHandle,
           PROCESS_INFORMATION_CLASS processInformationClass, IntPtr processInformation, int processInformationLength,
           ref int returnLength);

        //[DllImport("NTDLL.DLL")]
        //public static extern int
        //    NtQueryInformationProcess(int hProcess, PROCESS_INFORMATION_CLASS pic, ref 
        //            PROCESS_BASIC_INFORMATION pbi, int cb, ref int pSize); 

    }
}
