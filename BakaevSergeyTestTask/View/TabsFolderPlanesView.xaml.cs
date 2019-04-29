using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BakaevSergeyTestTask.View
{
    public partial class TabsFolderPlanesView : UserControl
    {
        public TabsFolderPlanesView()
        {
            InitializeComponent();
        }
        private void FolderPlanesHeaders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (sender as ListBox);
            if (lb != null)
            {
                lb.ScrollIntoView(lb.SelectedItem);
                lb.UpdateLayout();
            }
        }
    }
}
