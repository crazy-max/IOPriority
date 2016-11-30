using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOPriority
{
    class Program
    {
        public const uint PROCESS_MODE_BACKGROUND_BEGIN = 0x00100000;

        static int pid;
        static uint ioPrio;

        static void Main(string[] args)
        {
            if (!initArgs(args))
                return;

            //Win32.SetPriorityClass(Process.GetCurrentProcess().Handle, PROCESS_MODE_BACKGROUND_BEGIN);
            int hProcess = Win32.OpenProcess(PROCESS_RIGHTS.PROCESS_ALL_ACCESS, false, pid);
            if (hProcess == 0)
            {
                throw new Exception("Fuck! I can't open the process. Try to give her something to drink.");
            }

            setIOPrio(hProcess, ioPrio);
            printIOPrio(hProcess);
        }

        private static bool initArgs(string[] args)
        {
            if (args.Length < 2)
            {
                printUsage();
                return false;
            }

            pid = int.Parse(args[0]);
            ioPrio = uint.Parse(args[1]);

            return true;
        }

        private static void printUsage()
        {
            Console.WriteLine("You suck! I need 2 params, hehe: pid and prio(0,1,2)");
        }

        unsafe static void setIOPrio(int hProcess, uint newPrio)
        {
            uint ioPrio = newPrio;
            Win32.NtSetInformationProcess(hProcess, PROCESS_INFORMATION_CLASS.ProcessIoPriority,
                 (IntPtr)(&ioPrio), 4);
        }

        unsafe static void printIOPrio(int hProcess)
        {
            int sizeofResult = 0;
            uint ioPrio;
            Win32.NtQueryInformationProcess(hProcess, PROCESS_INFORMATION_CLASS.ProcessIoPriority,
                 (IntPtr)(&ioPrio), 4, ref sizeofResult);
            Console.WriteLine("new IOPrio: " + ioPrio);
        }
    }
}
