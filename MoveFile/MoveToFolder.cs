using Icard.SwiftParse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icard.MoveFile
{
    public class MoveToFolder
    {
        public string MoveFileToFolder(string fileName, bool isCorrectFail)
        {
            StringBuilder sb = new StringBuilder();
            string fileMoveToDestionation = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\{fileName}";

            if (isCorrectFail)
            {
                string successDestination = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\SUCCESS\{fileName}";
                File.Move(fileMoveToDestionation, successDestination);

                sb.AppendLine($"File is success and moved to Folder -> SUCCESS");
            }
            else
            {
                string filedDestination = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\FAILED\{fileName}";
                File.Move(fileMoveToDestionation, filedDestination);
                sb.AppendLine(@"File is failed and move to Folder -> FAILED");
            }
            return sb.ToString().TrimEnd();
        }

        //public string MoveToSuccesFolder(string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    string fileMoveToDestionation = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\{fileName}";
        //    string successDestination = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\SUCCESS\{fileName}";
        //    File.Move(fileMoveToDestionation, successDestination);
        //   return sb.AppendLine($"File is success and moved to Folder -> SUCCESS").ToString().TrimEnd();

        //}
        //public string MoveToFailedFolder(string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    string fileMoveToDestionation = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\{fileName}";
        //    string successDestination = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\FAILED\{fileName}";
        //    File.Move(fileMoveToDestionation, successDestination);
        //    return sb.AppendLine($"File is failed and move to Folder -> FAILED").ToString().TrimEnd();

        //}
    }
}