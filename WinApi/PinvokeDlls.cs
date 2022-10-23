using System;
using System.Runtime.InteropServices;

namespace WinApi
{
    internal class PinvokeDlls
    {
        [DllImport("kernel32.dll")]
        // static extern uint GetLogicalDriveStrings(uint nBufferLength,
        //    [Out] StringBuilder lpBuffer); --- Don't do this!

        // if we were to use the StringBuilder, only the first string would be returned
        // so, since arrays are reference types, we can pass an array of chars
        // just initialize it prior to call the function as
        // char[] lpBuffer = new char[nBufferLength];
        internal static extern uint GetLogicalDriveStrings(uint nBufferLength, [Out] char[] lpBuffer);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData
        );

        [DllImport("kernel32", CharSet = CharSet.Auto)]
        public static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32", CharSet = CharSet.Auto)]
        internal static extern bool FindClose(IntPtr hFindFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetFileTime(IntPtr hFile, ref long lpCreationTime, ref long lpLastAccessTime,
            ref long lpLastWriteTime);
    }

    internal enum FINDEX_INFO_LEVELS
    {
        FindExInfoStandard = 0,
        FindExInfoBasic = 1
    }

    internal enum FINDEX_SEARCH_OPS
    {
        FindExSearchNameMatch = 0,
        FindExSearchLimitToDirectories = 1,
        FindExSearchLimitToDevices = 2
    }

    // The CharSet must match the CharSet of the corresponding PInvoke signature
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WIN32_FIND_DATA
    {
        public uint dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint dwReserved0;
        public uint dwReserved1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cFileName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;

        public uint dwFileType;
        public uint dwCreatorType;
        public uint wFinderFlags;
    }
}