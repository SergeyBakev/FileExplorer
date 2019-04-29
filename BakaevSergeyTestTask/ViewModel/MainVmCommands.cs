using BakaevSergeyTestTask.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BakaevSergeyTestTask.ViewModel
{
    public partial class MainVm
    {
        private int GetIndexFolderPlanes(string path)
        {
            int indexInPlanes = -1;
            for (int i = 0; i <= FolderPlanes.Count - 1; i++)
            {
                if (FolderPlanes[i].FullPathName == path) { indexInPlanes = i; }
            }
            return indexInPlanes;
        }
        RelayCommand selectedPathFromTreeCommand;
        public ICommand SelectedPathFromTreeCommand
        {
            get
            {
                return selectedPathFromTreeCommand ??
                       (selectedPathFromTreeCommand =
                              new RelayCommand(x => SelectedPath = (x as string)));
            }
        }
        RelayCommand folderPlaneItemDoubleClickCommand;
        public ICommand FolderPlaneItemDoubleClickCommand
        {
            get
            {
                return folderPlaneItemDoubleClickCommand ??
                       (folderPlaneItemDoubleClickCommand =
                              new RelayCommand(x => OnFolderDownClick(x), x => true));
            }
        }
        public void OnFolderDownClick(object p)
        {
            if (p == null) return;
            string path = (p as FolderPlaneItem).FullPathName;

            bool isDrive = FolderItemUtils.IsDrive(path);
            bool isFolder = FolderItemUtils.IsFolder(path);
            if (isDrive || isFolder)
            {
                UseCurrentPlane = true;
                try
                {
                    SelectedPath = path;
                }
                finally { UseCurrentPlane = false; }
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                { }
            }
        }
        RelayCommand folderUpCommand;
        public ICommand FolderUpCommand
        {
            get { return folderUpCommand ?? (folderUpCommand = new RelayCommand(FolderUp, x => FolderPlanes.Count > 0)); }
        }
        public void FolderUp(object p)
        {
            if (SelectedFolderPlane != null)
            {
                string path = FolderItemUtils.FolderUp(SelectedFolderPlane.FullPathName);
                UseCurrentPlane = true;
                try
                {
                    SelectedPath = path;
                }
                finally { UseCurrentPlane = false; }

            }
        }
        RelayCommand closeTabCommand;
        public ICommand CloseTabCommand
        {
            get { return closeTabCommand ?? (closeTabCommand = new RelayCommand(CloseTab, x => FolderPlanes.Count > 0)); }
        }
        public void CloseTab(object p)
        {
            if (SelectedFolderPlane != null)
            {
                int i = GetIndexFolderPlanes(SelectedFolderPlane.FullPathName);

                if (i != -1)
                {
                    FolderPlanes.RemoveAt(i);
                    if (FolderPlanes.Count != 0)
                    {
                        i = i - 1;
                        if (i < 0) i = i + 1;
                        SelectedPath = FolderPlanes[i].FullPathName;
                    }
                    else
                    {
                        SelectedFolderPlane = null;
                        SelectedPath = "";
                    }
                }
            }
        }
        RelayCommand rebuildTree;
        public ICommand RebuildTreeCommand
        {
            get { return rebuildTree ?? (rebuildTree = new RelayCommand(RebuildTree)); }
        }
        public void RebuildTree(object p)
        {
            TabbedTrees.RebuildTree(null);
        }
        RelayCommand searchInFolder;
        public ICommand SearchInFolder
        {
            get{return searchInFolder ?? (searchInFolder = new RelayCommand(Search));}
        }
        public void Search(object p)
        {
            if(SelectedPath == null)
            {
                MessageBox.Show("Укажите папку в которой искать");
                return;
            }
            FolderPlane plane = new FolderPlane();
            plane.FullPathName = SelectedPath;
            foreach (var item in SelectedFolderPlane.FolderPlaneItems)
            {
                if (item.Name.IndexOf(SearchText, StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    plane.FolderPlaneItems.Add(item);
                }
            }
            SelectedFolderPlane = plane;
        }
        RelayCommand openListViewWithTextBox;
        public ICommand OpenListViewWithTextBox
        {
            get { return searchInFolder ?? (openListViewWithTextBox = new RelayCommand(Line)); }
        }
        public void Line(object p)
        {
            var str = p.ToString();
            if (str == "") { SetProperty(ref selectedPath, str, "SelectedPath"); return; }
            if (!Directory.Exists(str))
            {
                MessageBox.Show("Неверно указанный путь");
                return;
            }
            if (!FolderItemUtils.hasWriteAccessToFolder(str)) return;
            if (!SetProperty(ref selectedPath, str, "SelectedPath")) return;
            int indexInPlanes = GetIndexFolderPlanes(selectedPath);
            if (indexInPlanes != -1)
            {
                SelectedFolderPlane = FolderPlanes[indexInPlanes];
                UseCurrentPlane = false;
                return;
            }
            FolderPlane newPlane = new FolderPlane();
            newPlane.SetFolderPlane(selectedPath);
            if (UseCurrentPlane)
            {
                UseCurrentPlane = false;
                indexInPlanes = GetIndexFolderPlanes(SelectedFolderPlane.FullPathName);
                if (indexInPlanes != -1) { FolderPlanes[indexInPlanes] = newPlane; } else FolderPlanes.Add(newPlane);
            }
            else
            {
                FolderPlanes.Add(newPlane);
            }
            SelectedFolderPlane = newPlane;
        }
    }
}
