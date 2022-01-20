using Icard.Paths;
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
        public string MoveFileToFolder(string fileName, bool isCorrectFail)//Move the current created file to folder SUCCESS or FAILED
        {
            StringBuilder sb = new StringBuilder();
            string fileMoveToDestionation = String.Format(FolderPath.FILE_DESTINATION,fileName);

            if (isCorrectFail)
            {
                string successDestination = String.Format(FolderPath.SUCCESS_FOLDER_PATH,fileName);
                File.Move(fileMoveToDestionation, successDestination);

                sb.AppendLine(String.Format(FolderPath.FILE_SUCCESS,fileName));
            }
            else
            {
                string filedDestination = String.Format(FolderPath.FILED_FOLDER_PATH,fileName);
                File.Move(fileMoveToDestionation, filedDestination);
                sb.AppendLine(String.Format(FolderPath.FILE_FILED,fileName));
            }
            return sb.ToString().TrimEnd();
        }  
    }
}