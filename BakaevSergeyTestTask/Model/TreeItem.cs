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
    public class RootNode : ATreeItem
    {
        public override BitmapSource GetMyIcon()
        {
            return myIcon = null;
        }

        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            return new ObservableCollection<ITreeItem> { };
        }
    }
    public class DriveRootItem : ATreeItem
    {
        public DriveRootItem()
        {
            //Constructor sets some properties
            FriendlyName = "DriveRoot";
            IsExpanded = true;
            FullPathName = "$xxDriveRoot$";
        }
        public override BitmapSource GetMyIcon()
        {
            // Note: introduce more "speaking" icons for RootItems
            string Param = "pack://application:,,,/" + "MyImages/bullet_blue.png";
            Uri uri1 = new Uri(Param, UriKind.RelativeOrAbsolute);
            return myIcon = BitmapFrame.Create(uri1);
        }
        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            ITreeItem item1;
            string fn = "";

            //string[] allDrives = System.Environment.GetLogicalDrives();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
                if (drive.IsReady)
                {
                    item1 = new DriveItem();

                    // Some processing for the FriendlyName
                    fn = drive.Name.Replace(@"\", "");
                    item1.FullPathName = fn;
                    if (drive.VolumeLabel == string.Empty)
                    {
                        fn = drive.DriveType.ToString() + " (" + fn + ")";
                    }
                    else if (drive.DriveType == DriveType.CDRom)
                    {
                        fn = drive.DriveType.ToString() + " " + drive.VolumeLabel + " (" + fn + ")";
                    }
                    else
                    {
                        fn = drive.VolumeLabel + " (" + fn + ")";
                    }

                    item1.FriendlyName = fn;
                    childrenList.Add(item1);
                }

            return childrenList;
        }
    }
    public class FolderItem : ATreeItem
    {
        public override BitmapSource GetMyIcon()
        {
            return myIcon = Utils.GetIconFn.GetIconDll(this.FullPathName);
        }
        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            ITreeItem item1;

            try
            {
                DirectoryInfo di = new DirectoryInfo(this.FullPathName); // may be acces not allowed
                if (!di.Exists) return childrenList;
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    item1 = new FolderItem();
                    item1.FullPathName = FullPathName + "\\" + dir.Name;
                    item1.FriendlyName = dir.Name;
                    item1.DataCreate = dir.CreationTime.ToShortDateString();
                    childrenList.Add(item1);
                }

                foreach (FileInfo file in di.GetFiles())
                {
                    item1 = new FileItem();
                    item1.FullPathName = FullPathName + "\\" + file.Name;
                    item1.FriendlyName = file.Name;
                    item1.DataCreate = file.CreationTime.ToShortDateString();
                    childrenList.Add(item1);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            return childrenList;
        }
    }
    public class DriveItem : ATreeItem
    {
        public override BitmapSource GetMyIcon()
        {
            return myIcon = Utils.GetIconFn.GetIconDll(this.FullPathName);
        }
        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            ITreeItem item1;

            DriveInfo drive = new DriveInfo(this.FullPathName);
            if (!drive.IsReady) return childrenList;

            DirectoryInfo di = new DirectoryInfo(((DriveInfo)drive).RootDirectory.Name);
            if (!di.Exists) return childrenList;

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                item1 = new FolderItem();
                item1.FullPathName = FullPathName + "\\" + dir.Name;
                item1.FriendlyName = dir.Name;
                childrenList.Add(item1);
            }


            foreach (FileInfo file in di.GetFiles())
            {
                item1 = new FileItem();
                item1.FullPathName = FullPathName + "\\" + file.Name;
                item1.FriendlyName = file.Name;
                childrenList.Add(item1);
            }

            return childrenList;
        }
        

        //public override string ToString()
        //{
        //    return this.FriendlyName;
        //}
    }
    public class FileItem : ATreeItem
    {
        public override BitmapSource GetMyIcon()
        {
            // to do, use a cache for .ext != "" or ".exe" or ".lnk"
            return myIcon = Utils.GetIconFn.GetIconDll(this.FullPathName);
        }

        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            return childrenList;
        }
    }


}
