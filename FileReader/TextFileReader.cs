using Icard.MoveFile;
using Icard.SwiftParse;
using Icard.SwiftParse.ApplicationHeaderBlock;
using Icard.SwiftParse.UserHeaderBlock;
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
        private static readonly string PATTER_BLOCK_ID = @"({1:|{2:|{3:|{4:|{5:)";

        private static readonly MoveToFolder move = new MoveToFolder();
        private static readonly BasicHeaderBlock basicHeaderBlock = new BasicHeaderBlock();
        private static readonly ApplicationHeader applicationHeaderBlock = new ApplicationHeader();
        private static readonly UserHeaderBlock userHeaderBlock = new UserHeaderBlock();

        private static List<IBasicBlock> basicHeader = new List<IBasicBlock>();
        private static List<IApplication> applicationHeader = new List<IApplication>();
        private static List<IUserHeaderBlock> userHeader = new List<IUserHeaderBlock>();
        private static Queue<string> allSwiftMessages = new Queue<string>();

        public string FileReader(string fileName)
        {
            sb = new StringBuilder();
        
            string successFolderPath = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\SUCCESS\{fileName}";
            string failedFolderPath = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\FAILED\{fileName}";
            if (File.Exists(successFolderPath))
            {
                return $"Warning cannot use same file twice {fileName} in folder SUCCESS!!!";
            }
            else if (File.Exists(failedFolderPath))
            {
                return $"Warning cannot use same file twice {fileName} in folder FAILED!!!";
            }
            string path = @$"C:\Users\Ivan\OneDrive\Desktop\Folder\{fileName}";
            string[] allLines = File.ReadAllLines(path);
            allLines = allLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            bool isVallidMessage = true;

            if (allLines.Length > 0)
            {
                sb.AppendLine($"MT103 Messages:");

                for (int i = 0; i < allLines.Length; i++)
                {
                    string match = Regex.Match(allLines[i], PATTER_BLOCK_ID).ToString();
                    if (match == "{1:")
                    {
                        basicHeader = basicHeaderBlock.Create(allLines[i]);
                        if (basicHeader.Count == 0)
                        {
                            isVallidMessage = false;
                            break;
                        }
                        allSwiftMessages.Enqueue(basicHeaderBlock.ReturnResult());

                    }
                    else if (match == "{2:")
                    {
                        applicationHeader = applicationHeaderBlock.Create(allLines[i]);
                        if (applicationHeader.Count == 0)
                        {
                            isVallidMessage = false;
                            break;
                        }
                        allSwiftMessages.Enqueue(applicationHeaderBlock.ReturnResult());

                    }
                    else if (match == "{3:")
                    {
                        userHeaderBlock.AddAllMessagesToList(allLines[i]);
                        userHeader = userHeaderBlock.ReturnAllMessages();
                        if (userHeader.Count == 0 || !userHeader.Any(x => x.BankingPriorityCode == "{119:" && x.MessageUserReferenc == "STP}"))
                        {
                            isVallidMessage = false;
                            break;
                        }
                        foreach (var item in userHeader)
                        {

                            string concatItemParts = string.Concat($"{item.BankingPriorityCode}{item.MessageUserReferenc}");
                            allSwiftMessages.Enqueue(concatItemParts);
                        }


                    }
                    else
                    {
                        allSwiftMessages = new Queue<string>();
                        isVallidMessage = false;
                        break;
                    }
                    sb.AppendLine($"{allLines[i]} -> Successfull!");
                }
                
                string result = move.MoveFileToFolder(fileName, isVallidMessage);
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