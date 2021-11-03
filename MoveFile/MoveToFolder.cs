using Icard.SwiftParse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Icard.MoveFile
{
    public class MoveToFolder
    {
        private static BasicHeaderBlock basickHeader = new BasicHeaderBlock();
        private static List<IBasicBlock> basickBlock = new List<IBasicBlock>();

        public string MoveFileToFolder(BasicHeaderBlock basicHeader, List<IBasicBlock> basicBlock, string name)
        {
            StringBuilder sb = new StringBuilder();
            string fileMoveToDestionation = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\{name}";

            if (basicBlock.Any())
            {
                string successDestination = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\SUCCESS\{name}";
                File.Move(fileMoveToDestionation, successDestination);
                sb.AppendLine($"File is success and moved to Folder -> SUCCESS");

            }
            else
            {
                string filedDestination = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\FAILED\{name}";
                File.Move(fileMoveToDestionation, filedDestination);
                sb.AppendLine(@"File is failed and move to Folder -> FAILED");

            }
            return sb.ToString().TrimEnd();
        }
    }
}
