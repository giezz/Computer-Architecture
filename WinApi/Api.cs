using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using static WinApi.PinvokeDlls;

namespace WinApi
{
    public class Api
    {
        public static List<string> GetDisks()
        {
            const int size = 512;
            char[] buffer = new char[size];
            uint code = GetLogicalDriveStrings(size, buffer);

            if (code == 0)
            {
                Console.WriteLine("Call failed");
                return new List<string>();
            }

            StringCollection list = new StringCollection();
            int start = 0;
            for (int i = 0; i < code; ++i)
            {
                if (buffer[i] == 0)
                {
                    string s = new string(buffer, start, i - start);
                    list.Add(s);
                    start = i + 1;
                }
            }

            List<string> disks = new List<string>();
            foreach (string s in list)
                disks.Add(s);
            return disks;
        }

        public static List<string> GetFilesByDirectoryName(string directoryName)
        {
            WIN32_FIND_DATA wfd = new WIN32_FIND_DATA();
            IntPtr h = FindFirstFile(directoryName + @"\*.*", out wfd);
            List<string> files = new List<string>();
            while (FindNextFile(h, out wfd))
                files.Add(directoryName + wfd.cFileName);
            FindClose(h);
            return files;
        }

        public static Dictionary<string, Dictionary<string, FILETIME>> Foo(string directoryName)
        {
            WIN32_FIND_DATA wfd = new WIN32_FIND_DATA();
            IntPtr file = FindFirstFile(directoryName + @"\*.*", out wfd);
            Dictionary<string, Dictionary<string, FILETIME>> filesWithFileTime =
                new Dictionary<string, Dictionary<string, FILETIME>>();
            while (FindNextFile(file, out wfd))
                filesWithFileTime.Add(directoryName + wfd.cFileName,
                    new Dictionary<string, FILETIME>()
                    {
                        {"File creation time", wfd.ftCreationTime},
                        {"File last access time", wfd.ftLastAccessTime},
                        {"File last write time", wfd.ftLastWriteTime}
                    }
                );
            FindClose(file);
            return filesWithFileTime;
        }

        /// <param name="path">path</param>
        /// <returns>array of two: [0] - IntPtr, [1] - WIN32_FIND_DATA</returns>
        public static ValueType[] GetFileByPath(string path)
        {
            WIN32_FIND_DATA wfd = new WIN32_FIND_DATA();
            IntPtr file = FindFirstFile(path, out wfd);
            FindClose(file);
            return new ValueType[] {file, wfd};
        }

        public static void SetFileTimes(IntPtr hFile, DateTime creationTime, DateTime accessTime, DateTime writeTime)
        {
            long lCreationTime = creationTime.ToFileTime();
            long lAccessTime = accessTime.ToFileTime();
            long lWriteTime = writeTime.ToFileTime();

            if (!SetFileTime(hFile, ref lCreationTime, ref lAccessTime, ref lWriteTime))
            {
                throw new Win32Exception();
            }
        }
    }
}