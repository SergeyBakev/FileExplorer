using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BakaevSergeyTestTask.View
{
    public static class DataGridDoubleClick
    {
        public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.RegisterAttached(
                                  "DoubleClickCommand",
                                  typeof(ICommand),
                                  typeof(DataGridDoubleClick),
                                  new PropertyMetadata(new PropertyChangedCallback(AttachOrRemoveDoubleClickCommandEvent)));
        public static void SetDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }
        public static void AttachOrRemoveDoubleClickCommandEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DataGrid dataGrid = obj as DataGrid;
            if (dataGrid != null)
            {
                ICommand cmd = (ICommand)args.NewValue;
                if (args.OldValue == null && args.NewValue != null)
                {
                    dataGrid.MouseDoubleClick += ExecuteDataGridDoubleClick;
                }
                else if (args.OldValue != null && args.NewValue == null)
                {
                    dataGrid.MouseDoubleClick -= ExecuteDataGridDoubleClick;
                }
            }
        }
        private static void ExecuteDataGridDoubleClick(object sender, MouseButtonEventArgs args)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj.GetValue(DoubleClickCommandProperty);
            if (cmd != null)
            {
                object currentItem = ((sender as DataGrid).CurrentItem);
                if (cmd.CanExecute(currentItem))
                {
                    cmd.Execute(currentItem);
                }
            }
        }
    }
}
