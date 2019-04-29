using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakaevSergeyTestTask.Model
{
    public static class TreeRootItemUtils
    {
        public const string LastPartRootItemName = "RootItem";
        public static ATreeItem ReturnRootItem()
        {
            Type selectedType = typeof(DriveRootItem);
            string selectedName = "Drive";

            var entityTypes =
              from t in System.Reflection.Assembly.GetAssembly(typeof(ATreeItem)).GetTypes() where t.IsSubclassOf(typeof(ATreeItem)) select t;


            foreach (var tt in entityTypes)
            {
                if (tt.Name.EndsWith(LastPartRootItemName))
                {

                    selectedType = Type.GetType(tt.FullName);
                    selectedName = tt.Name.Replace(LastPartRootItemName, "");
                    break;

                }
            }
            ATreeItem rootItem = (ATreeItem)Activator.CreateInstance(selectedType);
            rootItem.FriendlyName = selectedName;
            return rootItem;
        }
        public static List<string> ListNavTreeRootItemsByConvention()
        {
            List<string> List = new List<string> { };
            // By convention: all classes that end with "RootItem" form the rootlist 
            // Use reflection for list of all NavTreeItem classes, 
            var entityTypes =
              from t in System.Reflection.Assembly.GetAssembly(typeof(ATreeItem)).GetTypes() where t.IsSubclassOf(typeof(ATreeItem)) select t;

            foreach (var t in entityTypes)
            {
                if (t.Name.EndsWith(LastPartRootItemName))
                {
                    //Console.Write("* Root * "); 
                    List.Add(t.Name.Replace(LastPartRootItemName, ""));
                }
                //Console.WriteLine(t.Name);
            }
            return List;
        }
    }
}
    

