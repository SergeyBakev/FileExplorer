using BakaevSergeyTestTask.Model;
using BakaevSergeyTestTask.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BakaevSergeyTestTask.ViewModel
{
    public partial class MainVm : ViewModelBase
    {
        private bool UseCurrentPlane = false;
        public TabbedTreesVm TabbedTrees { get; set; }
        #region Property
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                SetProperty(ref searchText, value, "SearchText");
            }
        }
        private string selectedPath;
        public string SelectedPath
        {
            get
            {
                return selectedPath;
            }
            set
            {
                if (value == "") { SetProperty(ref selectedPath, value, "SelectedPath"); return; }
                if (!FolderItemUtils.hasWriteAccessToFolder(value)) return;
                if (!Directory.Exists(value)) return;
                if (!SetProperty(ref selectedPath, value, "SelectedPath")) return;
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
        private FolderPlane selectedFolderPlane;
        public FolderPlane SelectedFolderPlane
        {
            get { return selectedFolderPlane ?? (selectedFolderPlane = new FolderPlane()); }
            set
            {
                selectedFolderPlane = value;
                if (selectedFolderPlane != null)
                    if (SelectedPath != selectedFolderPlane.FullPathName) { SelectedPath = selectedFolderPlane.FullPathName; }
                RaisePropertyChanged("SelectedFolderPlane");
            }
        }
        private ObservableCollection<FolderPlane> folderPlanes;
        public ObservableCollection<FolderPlane> FolderPlanes
        {
            get { return folderPlanes ?? (folderPlanes = new ObservableCollection<FolderPlane>()); }
        }
        #endregion
        #region Ctor()
        public MainVm()
        {
            TabbedTrees = new TabbedTreesVm();
            TabbedTrees.SelectedNavTree = TabbedTrees.NavTrees[0];
        }
        #endregion
    }
}
