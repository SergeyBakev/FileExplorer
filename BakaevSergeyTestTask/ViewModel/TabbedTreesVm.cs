using BakaevSergeyTestTask.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakaevSergeyTestTask.ViewModel
{
    public class TabbedTreesVm : ViewModelBase
    {
        private const int TabsPerRow = 3;
        public List<string> listNamesNavTrees { get; set; }
        private ObservableCollection<TreeVm> navTrees;
        public ObservableCollection<TreeVm> NavTrees
        {
            get { return navTrees ?? (navTrees = new ObservableCollection<TreeVm>()); }
        }
        private TreeVm selectedNavTree;
        public TreeVm SelectedNavTree
        {
            get { return selectedNavTree; }
            set
            {
                SetProperty(ref selectedNavTree, value, "SelectedNavTree");
                SelectedNavTree.RebuildTree();
            }
        }
        public void RebuildTree(object p)
        {
            foreach(var el in navTrees)
            {
                el.RebuildTree();
            }
        }
        private int maxRowsNavTrees;
        public int MaxRowsNavTrees
        {
            get { return maxRowsNavTrees; }
            set { SetProperty(ref maxRowsNavTrees, value, "MaxRowsNavTrees"); }
        }
        public TabbedTreesVm()
        {
            navTrees = new ObservableCollection<TreeVm>();
            listNamesNavTrees = TreeRootItemUtils.ListNavTreeRootItemsByConvention();
            TreeVm newTree;
            MaxRowsNavTrees = 2;
            int nrTrees = MaxRowsNavTrees * TabsPerRow;
            int nrRootItems = listNamesNavTrees.Count();
            for (int rootNr = 0; rootNr < nrTrees; rootNr++)
            {
                newTree = new TreeVm();
                newTree.TreeName = (rootNr < nrRootItems) ? listNamesNavTrees[rootNr] : listNamesNavTrees[0] + (rootNr - nrRootItems).ToString("d");
                NavTrees.Add(newTree);
            }
        }
    }
}
