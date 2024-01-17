using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BlurWindow.Helpers
{
    [StructLayout(LayoutKind.Sequential)]
    public struct _OSVERSIONINFOW
    {
        public uint dwOSVersionInfoSize;
        public uint dwMajorVersion;
        public uint dwMinorVersion;
        public uint dwBuildNumber;
        public uint dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;
    }

    public class OsVersionHelper
    {
        public const uint STATUS_SUCCESS = 0;

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int RtlGetVersion(ref _OSVERSIONINFOW versionInfo);


        public static bool IsGreaterThanWindows1122H2()
        {
            //22621(22H2)
            _OSVERSIONINFOW _OSVERSIONINFOW = new _OSVERSIONINFOW();
            if(RtlGetVersion(ref _OSVERSIONINFOW) == STATUS_SUCCESS)
            {
                return _OSVERSIONINFOW.dwBuildNumber >= 22621;
            }
            else
            {
                return false;
            }
        }
    }
}
