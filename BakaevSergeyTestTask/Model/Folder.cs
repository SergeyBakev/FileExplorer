using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BakaevSergeyTestTask.Model
{
    public interface IFolderPlane
    {
        string FullPathName { get; set; }
        string FriendlyName { get; set; }
        void SetFolderPlane(string path, bool clear = false);
        ObservableCollection<FolderPlaneItem> FolderPlaneItems { get; set; }
    }
    public class FolderPlaneItem
    {
        public String FullPathName { get; set; }
        public String Name { get; set; }
        public String Ext { get; set; }
        public String Date { get; set; }
        public long Size { get; set; }
        public BitmapSource MyIcon { get; set; }
    }
    public class FolderPlane : IFolderPlane
    {
        public string FullPathName { get; set; }
        public string FriendlyName { get; set; }
        public string Data { get; set; }
        public long Size { get; set; }
        public String Ext { get; set; }
        public ObservableCollection<FolderPlaneItem> FolderPlaneItems { get; set; }
        public FolderPlane()
        {
            FolderPlaneItems = new ObservableCollection<FolderPlaneItem> { };
            Data = null;
            Size = 0;
            Ext = null;
        }
        public void SetFolderPlane(string path, bool clear = false)
        {
            if (!Directory.Exists(path)) path = "";
            FullPathName = path;

            if ((path == null) || (path == ""))
            {
                FolderPlaneItems.Clear();
                return;
            }

            if (clear) FolderPlaneItems.Clear();

            bool isDrive = FolderItemUtils.IsDrive(path);

            if (isDrive)
            {
                FriendlyName = path;
                DriveInfo drive = new DriveInfo(path);
                DirectoryInfo di = new DirectoryInfo(((DriveInfo)drive).RootDirectory.Name);
                GetFoldersAndFiles(di);
                Data = di.LastWriteTime.ToLongTimeString();
                Ext = di.Extension;
                Size = 0;
                return;
            }
            bool isFolder = FolderItemUtils.IsFolder(path);
            if (isFolder)
            {
                DateTime dt;
                string[] folders = path.Split('\\');
                string str = folders[folders.Length - 1];
                FriendlyName = FolderItemUtils.MyShortFriendlyName(str);
                DirectoryInfo di = new DirectoryInfo(path);
                Ext = di.Extension;
                Size = GetFoldersAndFiles(di);
                dt = di.LastWriteTime;
                string format = " yyyy/MM/dd  HH.mm";
                Data = dt.ToString(format);
                return;
            }
        }
        private long GetFoldersAndFiles(DirectoryInfo di)
        {
            long totalSize = 0;
            FolderPlaneItem item;
            try
            {
                DateTime dt;
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    item = new FolderPlaneItem();
                    item.FullPathName = dir.FullName;
                    item.Name = dir.Name;
                    item.Ext = "";
                    dt = dir.LastWriteTime; //DateTime.Now;
                    string format = " yyyy/MM/dd  HH.mm";
                    item.Date = dt.ToString(format);
                    item.Size = 0;
                    item.MyIcon = Utils.ImageCache.GetImage(dir.FullName);
                    FolderPlaneItems.Add(item);
                }
                char[] aPoint = { '.' };
                foreach (FileInfo file in di.GetFiles())
                {
                    if ((!file.Attributes.HasFlag(FileAttributes.Hidden)))
                    {
                        item = new FolderPlaneItem();
                        item.FullPathName = file.FullName;
                        item.Name = file.Name;
                        item.Ext = file.Extension.TrimStart(aPoint);
                        dt = file.LastWriteTime;
                        string format = " yyyy/MM/dd  HH.mm";
                        item.Date = dt.ToString(format);
                        item.Size = file.Length / 1024;
                        item.MyIcon = Utils.ImageCache.GetImage(file.FullName);
                        totalSize += item.Size;
                        FolderPlaneItems.Add(item);
                    }
                }
                return totalSize;
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
                return 0;
            }

        }
    }
}
