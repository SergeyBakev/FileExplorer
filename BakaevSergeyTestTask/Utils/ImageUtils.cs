using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;

namespace BakaevSergeyTestTask.Utils
{
    public class ShellIcon
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        class Win32
        {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0; // Large icon
            public const uint SHGFI_SMALLICON = 0x1; // Small icon
            public const uint USEFILEATTRIBUTES = 0x000000010; // when the full path isn't available
            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
            [DllImport("User32.dll")]
            public static extern int DestroyIcon(IntPtr hIcon);

            //extra
            [DllImport("Shell32.dll")]
            public extern static int ExtractIconEx(string libName, int iconIndex, IntPtr[] largeIcon, IntPtr[] smallIcon, uint nIcons);
        }

        public ShellIcon() { }
        public static Icon GetSmallIcon(string fileName)
        {
            IntPtr hImgSmall; //the handle to the system image list
            SHFILEINFO shinfo = new SHFILEINFO();
            hImgSmall = Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
            //The icon is returned in the hIcon member of the shinfo struct
            Icon icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
            Win32.DestroyIcon(shinfo.hIcon);
            return icon;
        }
        public static Icon GetLargeIcon(string fileName)
        {
            IntPtr hImgLarge; //the handle to the system image list
            SHFILEINFO shinfo = new SHFILEINFO();
            hImgLarge = Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
            try
            {
                Icon icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
                Win32.DestroyIcon(shinfo.hIcon);
                return icon;
            }
            catch
            {
                // to do, test registry??
                return null;
            }
        }
    }
    public static class ImageCache
    {
        public static Dictionary<String, BitmapSource> imageList = new Dictionary<String, BitmapSource>();

        public static BitmapSource GetImage(string fullpath)
        {
            string ext = Path.GetExtension(fullpath);
            ext.ToLower();

            // if in the list we are done
            if (imageList.ContainsKey(ext))
            {
                return imageList[ext];
            }

            // get the image
            BitmapSource myIcon;
            myIcon = Utils.GetIconFn.GetIconDll(fullpath);

            // put myIcon in the imageList, unless its extension says that it
            if ((ext != "") && (ext != ".exe") && (ext != ".lnk") && (ext != ".ico"))
            {
                imageList.Add(ext, myIcon);
            }
            return myIcon;
        }
    }
    public static class GetIconFn
    {

        public static System.Windows.Media.Imaging.BitmapSource GetIconDll(string fileName)
        {
            BitmapSource myIcon = null;

            Boolean validDrive = false;
            foreach (DriveInfo D in System.IO.DriveInfo.GetDrives())
            {   //D.DriveType.
                if (fileName == D.Name)
                {
                    validDrive = true;
                }
            }

            if ((System.IO.File.Exists(fileName)) || (System.IO.Directory.Exists(fileName)) || (validDrive))
            {
                using (System.Drawing.Icon sysIcon = ShellIcon.GetLargeIcon(fileName))
                {
                    try
                    {
                        myIcon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                                        sysIcon.Handle,
                                        System.Windows.Int32Rect.Empty,
                                        System.Windows.Media.Imaging.BitmapSizeOptions.FromWidthAndHeight(34, 34));
                    }
                    catch
                    {
                        myIcon = null;
                    }
                }
            }
            return myIcon;
        }
    }
}
