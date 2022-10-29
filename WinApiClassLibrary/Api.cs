using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using static WinApi.PinvokeDlls;

namespace WinApi
{
    public static class Api
    {
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        /// <returns>List of all logical disks</returns>
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
        
        /// <param name="directoryName">directoryName</param>
        /// <returns>Dictionary of file names and timestamps</returns>
        public static Dictionary<string, Dictionary<string, DateTime>> GetFilesByDirectoryName(string directoryName)
        {
            WIN32_FIND_DATA wfd = new WIN32_FIND_DATA();
            IntPtr file = FindFirstFile(directoryName + @"\*.*", out wfd);
            Dictionary<string, Dictionary<string, DateTime>> filesWithFileTime =
                new Dictionary<string, Dictionary<string, DateTime>>();
            while (FindNextFile(file, out wfd))
                filesWithFileTime.Add(directoryName + wfd.cFileName,
                    new Dictionary<string, DateTime>()
                    {
                        {"File creation time:", DateTime.FromFileTime(FileTimeToInterval(wfd.ftCreationTime))},
                        {"File last access time:", DateTime.FromFileTime(FileTimeToInterval(wfd.ftLastAccessTime))},
                        {"File last write time:", DateTime.FromFileTime(FileTimeToInterval(wfd.ftLastWriteTime))}
                    }
                );
            FindClose(file);
            return filesWithFileTime;
        }

        public static IntPtr GetFileDescriptor(string path)
        {
            IntPtr fileHandle = CreateFile(
                path,
                (FileAccess) EFileAccess.FILE_WRITE_ATTRIBUTES,
                FileShare.ReadWrite,
                IntPtr.Zero,
                FileMode.Open,
                (FileAttributes) EFileAttributes.BackupSemantics,
                IntPtr.Zero
            );

            if (fileHandle == INVALID_HANDLE_VALUE)
            {
                CloseHandle(fileHandle);
                throw new Exception("Invalid file descriptor");
            }

            return fileHandle;
        }

        public static void SetFileTimes(string path, DateTime creationTime, DateTime accessTime, DateTime writeTime)
        {
            IntPtr hFile = GetFileDescriptor(path);
            long lCreationTime = creationTime.ToFileTime();
            long lAccessTime = accessTime.ToFileTime();
            long lWriteTime = writeTime.ToFileTime();

            if (!SetFileTime(hFile, ref lCreationTime, ref lAccessTime, ref lWriteTime))
            {
                CloseHandle(hFile);
                throw new Win32Exception();
            }
            CloseHandle(hFile);
        }

        private static long FileTimeToInterval(FILETIME fileTime)
        {
            long interval = 0;
            interval |= (uint) fileTime.dwHighDateTime;
            interval <<= sizeof(uint) * 8;
            interval |= (uint) fileTime.dwLowDateTime;
            return interval;
        }
    }
}