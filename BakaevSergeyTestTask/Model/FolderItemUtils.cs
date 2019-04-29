using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;

namespace BakaevSergeyTestTask.Model
{
    public static class FolderItemUtils
    {
        public static bool IsFolder(string path)
        {
            bool isFolder;
            isFolder = System.IO.Directory.Exists(path);
            return isFolder;
        }
        public static bool IsDrive(string path)
        {
            bool isDrive = false;
            // path here X: ; str X:// 
            foreach (string str in Directory.GetLogicalDrives())
            {
                if (str.Contains(path)) { isDrive = true; }
            }
            return isDrive;
        }
        public static bool IsLink(string path)
        {
            bool isLink = false;

            string ext = Path.GetExtension(path);
            ext.ToLower();

            isLink = (ext == ".lnk");
            return isLink;
        }
        public static bool hasWriteAccessToFolder(string folderPath)
        {
            try
            {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
        public static string FolderUp(string path)
        {
            string[] folders = path.Split('\\');

            if (folders.Length <= 1)
            {
                // the sky is the limit
                return path;
            }
            else
            {
                // Remove from path tailer = "//" + folders[folders.Length - 1]
                // for (int i = 0; i <= folders.Length - 3; i++) { result = result + folders[i] + "\\"; }
                // result = result + folders[folders.Length - 2];

                string result = path.Remove(path.Length - (2 + folders[folders.Length - 1].Length) + 1);
                return result;
            }
        }
        public static string MyShortFriendlyName(string text)
        {
            String str = "";
            string[] words = text.Split(new char[] { ' ', '-', '_', '.' });
            int NrChar = text.Length;
            int NrLines = (NrChar <= 10) ? NrLines = 1 : (NrChar <= 40) ? NrLines = 2 : NrLines = 3; 
            int NrCharLine = NrChar / NrLines;
            if ((NrLines == 1) || (words.Count() == 1)) return text; 
            int indexInTextSeparator = -1;
            int startInLineCurrentWord = 0;
            int endInLineCurrentWord = 0;
            int lengthWord = 0;
            for (int iWord = 0; iWord <= words.Length - 1; iWord++)
            {
                lengthWord = words[iWord].Length;
                endInLineCurrentWord = startInLineCurrentWord + lengthWord - 1;
                if (((endInLineCurrentWord + 1 <= 1.05 * (NrCharLine)) || (startInLineCurrentWord <= 0.2 * NrCharLine)))
                {
                    if ((iWord != 0) && (indexInTextSeparator < text.Length - 1))
                    {
                        str = str + text[indexInTextSeparator];
                    };
                    str = str + words[iWord];
                    startInLineCurrentWord = endInLineCurrentWord + 1;
                }
                else
                {
                    str = str + System.Environment.NewLine + words[iWord];
                    startInLineCurrentWord = lengthWord;
                }

                indexInTextSeparator = indexInTextSeparator + (lengthWord + 1);
            }

            return str;
        }
    }
}
