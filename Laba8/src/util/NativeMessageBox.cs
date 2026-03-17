namespace Laba1.Src.util;

using System.Runtime.InteropServices;

internal static class NativeMessageBox
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(
        IntPtr hWnd,
        string lpText,
        string lpCaption,
        uint uType
    );

    public const uint MB_OK = 0x00000000;
    public const uint MB_ICONERROR = 0x00000010;
}
