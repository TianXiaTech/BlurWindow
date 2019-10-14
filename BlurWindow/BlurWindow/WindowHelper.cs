using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace BlurWindow
{
    class WindowHelper
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        internal enum AccentState
        {
            AccentDisabled = 1,
            AccentEnableGradient = 0,
            AccentEnableTransparentgradient = 2,
            AccentEnableBlurbehind = 3,
            AccentInvalidState = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            // ReSharper disable FieldCanBeMadeReadOnly.Global
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
            // ReSharper restore FieldCanBeMadeReadOnly.Global
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            // ...
            WcaAccentPolicy = 19
            // ...
        }

        public bool BlurWindow(Window win)
        {
            if (Environment.OSVersion.Version.Major < 10 && Environment.OSVersion.Version.Minor < 2)
            {
                return false;
            }

            try
            {
                var winhelp = new WindowInteropHelper(win);
                var accent = new AccentPolicy
                {
                    AccentState = AccentState.AccentEnableBlurbehind
                };
                var accentStructSize = Marshal.SizeOf(accent);

                var accentPtr = Marshal.AllocHGlobal(accentStructSize);
                Marshal.StructureToPtr(accent, accentPtr, false);

                var data = new WindowCompositionAttributeData
                {
                    Attribute = WindowCompositionAttribute.WcaAccentPolicy,
                    SizeOfData = accentStructSize,
                    Data = accentPtr
                };

                SetWindowCompositionAttribute(winhelp.Handle, ref data);

                Marshal.FreeHGlobal(accentPtr);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
