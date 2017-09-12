using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Threading;

///  <summary>
///  API declarations relating to file I/O.
///  </summary>

using System;

namespace GenericHid
{
    public sealed class FileIO
    {
        public const Int32 FILE_SHARE_READ = 1;
        public const Int32 FILE_SHARE_WRITE = 2;
        public const uint GENERIC_READ = 0X80000000U;
        public const Int32 GENERIC_WRITE = 0X40000000;
        public const Int32 INVALID_HANDLE_VALUE = -1;
        public const Int32 OPEN_EXISTING = 3;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern SafeFileHandle CreateFile(String lpFileName, UInt32 dwDesiredAccess, Int32 dwShareMode, IntPtr lpSecurityAttributes, Int32 dwCreationDisposition, Int32 dwFlagsAndAttributes, Int32 hTemplateFile);
    }
}

