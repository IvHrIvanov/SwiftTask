using Icard.MoveFile;
using Icard.SwiftParse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Icard.FileReader
{
    public class TextFileReader
    {
        private static StringBuilder sb = new StringBuilder();

        public string FileReader(string name)
        {
            string swiftMessage = File.ReadAllText(@$"C:\Users\Ivan\OneDrive\Desktop\Folder\{name}");
            if (!String.IsNullOrEmpty(swiftMessage))
            {
                MoveToFolder move = new MoveToFolder();

                BasicHeaderBlock basicHeader = new BasicHeaderBlock();
                List<IBasicBlock> basicBlock = new List<IBasicBlock>();

                sb.AppendLine("MT103 Messages:");
                basicBlock = basicHeader.Create(swiftMessage);
                string result = move.MoveFileToFolder(basicHeader, basicBlock, name);
                sb.AppendLine(result);

                return sb.ToString().TrimEnd();
            }
            else
            {
                return $"{name} is Empty";
            }
        }


    }
}
