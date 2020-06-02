using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace DisableScreensaver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Preventing UserLock while this app is running...");
            Console.WriteLine("Close app to restore user lock");
            ThreadStatusManager.PreventUserLock();

            while (true)
            {
                Thread.Sleep(600000);
            }
        }

    }

    internal static class ThreadStatusManager
    {
        public static void PreventSleep()
        {
            SetThreadExecutionState(ExecutionState.EsContinuous | ExecutionState.EsSystemRequired);
        }

        public static void ResetThreadStatus()
        {
            SetThreadExecutionState(ExecutionState.EsContinuous);
        }

        public static void PreventUserLock()
        {
            SetThreadExecutionState(ExecutionState.EsDisplayRequired | ExecutionState.EsContinuous);
        }


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        [FlagsAttribute]
        private enum ExecutionState : uint
        {
            EsAwaymodeRequired = 0x00000040,
            EsContinuous = 0x80000000,
            EsDisplayRequired = 0x00000002,
            EsSystemRequired = 0x00000001
        }
    }
}
