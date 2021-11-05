using Icard.MoveFile;
using Icard.SwiftParse;
using Icard.SwiftParse.ApplicationHeaderBlock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Icard.FileReader
{
    public class TextFileReader
    {
        private static StringBuilder sb;
        private static readonly string PATTER_BLOCK_ID = @"(1:|2:|3:|4:|5:)";

        private static readonly MoveToFolder move = new MoveToFolder();
        private static readonly BasicHeaderBlock basicHeader = new BasicHeaderBlock();
        private static readonly ApplicationHeader applicationHeaderBlock = new ApplicationHeader();

        private static List<IBasicBlock> basicBlock = new List<IBasicBlock>();
        private static List<IApplication> applicationHeader = new List<IApplication>();

        private readonly Queue<string> allSwiftMessages = new Queue<string>();
        
        public string FileReader(string fileName)
        {
            string path = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\{fileName}";
            string[] allLines = File.ReadAllLines(path);
            allLines = allLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            bool isCorrect = true;
          
            if (allLines.Length > 0)
            {
                sb = new StringBuilder();
                sb.AppendLine($"MT103 Messages:");
                for (int i = 0; i < allLines.Length; i++)
                {
                    string match = Regex.Match(allLines[i], PATTER_BLOCK_ID).ToString();
                    if (match == "1:")
                    {
                        basicBlock = basicHeader.Create(allLines[i]);
                        if (basicBlock.Count == 0)
                        {
                            isCorrect = false;
                            break;
                        }
                        allSwiftMessages.Enqueue(basicHeader.ReturnResult());
                        sb.AppendLine($"{allLines[i]} -> Successfull!");

                    }
                    else if (match == "2:")
                    {
                        applicationHeader = applicationHeaderBlock.Create(allLines[i]);
                        if (applicationHeader.Count == 0)
                        {
                            isCorrect = false;
                            break;
                        }
                        allSwiftMessages.Enqueue(applicationHeaderBlock.ReturnResult());
                        sb.AppendLine($"{allLines[i]} -> Successfull!");

                    }
                    else
                    {
                        isCorrect = false;
                        break;
                    }
                }

                string result = move.MoveFileToFolder(fileName, isCorrect);
                sb.AppendLine(result);
                return sb.ToString().TrimEnd();
            }
            else
            {
                return $"{fileName} is Empty";
            }
        }
    }
}


//List<string> b = new List<string>();

//foreach (var item in basicBlock)
//{
//    b.Add(item.BlockId.ToString());
//    b.Add(item.ApplicationID.ToString());
//    b.Add(item.ServiceId.ToString());
//    b.Add(item.LogicalAddres.ToString());
//    b.Add(item.SessionNumber.ToString());
//    b.Add(item.SequenceNumber.ToString());
//}
//Console.WriteLine(string.Join("", b));