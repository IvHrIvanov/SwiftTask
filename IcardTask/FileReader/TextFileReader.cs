using Icard.FileObject;
using Icard.MoveFile;
using Icard.Paths;
using Icard.SwiftParse;
using Icard.SwiftParse.ApplicationHeaderBlock;
using Icard.SwiftParse.TextBlock;
using Icard.SwiftParse.UserHeaderBlock;
using Swift.Db.Models;
using Swift.Db.Models.Models;
using Swift.Initializator.InsertDataToDB;
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

        private static readonly MoveToFolder move = new MoveToFolder();
        private static readonly BasicHeaderBlock basicHeaderBlock = new BasicHeaderBlock();
        private static readonly ApplicationHeader applicationHeaderBlock = new ApplicationHeader();
        private static readonly UserHeaderBlock userHeaderBlock = new UserHeaderBlock();
        private static readonly TextBlock textBlock = new TextBlock();

        private static List<IBasicBlock> basicHeader = new List<IBasicBlock>();
        private static List<IApplication> applicationHeader = new List<IApplication>();
        private static List<IUserHeaderBlock> userHeader = new List<IUserHeaderBlock>();
        private static List<ITextBlock> textBlocks = new List<ITextBlock>();

        public string FileReader(string fileName)//When the file is created, the method reads it
        {

            sb = new StringBuilder();

            if (File.Exists(String.Format(FolderPath.SUCCESS_FOLDER_PATH, fileName)) ||
                File.Exists(String.Format(FolderPath.FILED_FOLDER_PATH, fileName))) //Check if the fail is exist
            {
                return String.Format(FolderPath.FILED_FILE_CANNOT_USE_TWICE, fileName);
            }

            string[] allLines = File.ReadAllLines(String.Format(FolderPath.FILE_DESTINATION, fileName)).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            bool isVallidMessage = true;
            List<Transaction> transactions = new List<Transaction>();
            if (allLines.Length > 0)
            {
                sb.AppendLine($"MT103 Messages:");

                for (int i = 0; i < allLines.Length; i++)
                {
                    string match = Regex.Match(allLines[i], FolderPath.PATTER_BLOCK_ID).ToString();
                    //Start checking Block Id is correct
                    if (match == "{1:")
                    {
                        basicHeader = basicHeaderBlock.ParseMessageBasicHeaderBlock(allLines[i]);
                        if (basicHeader.Count == 0)
                        {
                            isVallidMessage = false;
                            break;
                        }
                    }
                    else if (match == "{2:")
                    {
                        applicationHeader = applicationHeaderBlock.ParseMessageApplicationHeaderBlock(allLines[i]);
                        if (applicationHeader.Count == 0)
                        {
                            isVallidMessage = false;
                            break;
                        }
                    }
                    else if (match == "{3:")
                    {
                        userHeaderBlock.ParseMessageUserHeaderBlock(allLines[i]);
                        userHeader = userHeaderBlock.ReturnAllMessages();
                        if (userHeader.Count == 0 ||
                            !userHeader.Any(x => x.BankingPriorityCode == "{119:" && x.MessageUserReferenc == "STP}"))
                        {
                            isVallidMessage = false;
                            break;
                        }
                    }
                    else if (match == "{4:")
                    {
                        List<string> textBlockMessage = new List<string>();

                        for (int a = i; a < allLines.Length; a++)
                        {

                            if (allLines[a].StartsWith("-}"))
                            {
                                i = a;
                                sb.AppendLine($"{allLines[a]} -> Successfull!");

                                textBlockMessage.Add(allLines[a]);
                                break;
                            }
                            if (allLines[a].StartsWith("{4:") ||
                                Regex.IsMatch(allLines[a], FolderPath.SWIFT_FORMAT_FIELDS))
                            {
                                sb.AppendLine(allLines[a]);
                                textBlockMessage.Add(allLines[a]);
                                i = a;
                            }

                        }
                        textBlock.TextBlockParse(textBlockMessage);
                        textBlocks.Add(textBlock);
                        if (textBlocks.Count == 0)
                        {
                            sb.AppendLine($"TextBlock -> Filed!!!");
                            isVallidMessage = false;
                            break;
                        }
                        Transaction transaction = TransactionCreate.TransactionCreated(
                           textBlock.Amount, textBlock.Currency, textBlock.Reason,
                           textBlock.SenderName, basicHeaderBlock.SenderBIC, textBlock.SenderBankAccount,
                           textBlock.RecieverName, applicationHeaderBlock.RecieverBIC, textBlock.RecieverBankAccount);
                        transactions.Add(transaction);
                        continue;

                    }
                    else if (match == "{5:")
                    {
                        for (int a = i; a < allLines.Length; a++)
                        {
                            if (allLines[a].Contains("{CHK:")) //Check Mandatory {CHK:12!} is valid and contains
                            {
                                int index = allLines[a].IndexOf("{CHK:") + 4;
                                string chk = allLines[a].Substring(index, 12);
                                if (chk.Length == 12)
                                {
                                    i = a;
                                    break;
                                }
                                else
                                {
                                    isVallidMessage = false;
                                    break;
                                }

                            }
                        }

                    }
                    //Log if the current message is Failed
                    else
                    {
                        sb.AppendLine($"{allLines[i]} -> Failed!!!");
                        isVallidMessage = false;
                        break;
                    }
                    //Log if the current message is Success
                    sb.AppendLine($"{allLines[i]} -> Successfull!");

                }
                if (isVallidMessage) // IF current message/transaction is valid we craeted file and transaction and send them to DB
                {
                    DateTime fileDateTime = File.GetCreationTime(String.Format(FolderPath.FILE_DESTINATION, fileName));
                   
              
                    FileCreated.FileCreate(fileName, fileDateTime, transactions);
                    FileTxt file = FileCreated.ReturnFile();
                }
                sb.AppendLine(move.MoveFileToFolder(fileName, isVallidMessage));
                sb.AppendLine($"=== \"{fileName}\" is added to DataBase ===");
                return sb.ToString().TrimEnd();
            }
            else 
            {
                return $"{fileName} is Empty";
            }
        }
    }
}