using BakaevSergeyTestTask.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BakaevSergeyTestTask.ViewModel
{
    public class TreeVm : ViewModelBase
    {
        private string treeName = "";
        public string TreeName
        {
            get { return treeName; }
            set { SetProperty(ref treeName, value, "TreeName"); }
        }
        private int rootNr;
        public int RootNr
        {
            get { return rootNr; }
            set { SetProperty(ref rootNr, value, "RootNr"); }
        }
        private ObservableCollection<ITreeItem> rootChildren = new ObservableCollection<ITreeItem> { };
        public ObservableCollection<ITreeItem> RootChildren
        {
            get { return rootChildren; }
            set { SetProperty(ref rootChildren, value, "RootChildren"); }
        }
        public void RebuildTree(int pRootNr = -1, bool pIncludeFileChildren = false)
        {
            RootNr = pRootNr;
            ATreeItem treeRootItem = TreeRootItemUtils.ReturnRootItem();
            TreeName = treeRootItem.FriendlyName;
            foreach (ITreeItem item in RootChildren) { item.DeleteChildren(); }
            RootChildren.Clear();
            foreach (ITreeItem item in treeRootItem.Children) { RootChildren.Add(item); }
        }
        public TreeVm()
        {
            RebuildTree(0);
        }
    }
}
