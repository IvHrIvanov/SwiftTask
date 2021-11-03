using Icard.EventMethods;
using Icard.MoveFile;
using Icard.SwiftParse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Icard
{
    class Program
    {
        static void Main(string[] args)
        {
            Events methods = new Events();
            string folderPath = @"C:\Users\Ivan\OneDrive\Desktop\Folder";
            FileSystemWatcher watcher = new FileSystemWatcher(folderPath);
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.Size;
            watcher.Filter = "*.txt";
            watcher.EnableRaisingEvents = true;
         
            watcher.Changed += methods.OnActionOnFolderPath;
            watcher.Created += methods.OnActionOnFolderPath;
            watcher.Renamed += methods.OnFileRename;

            Console.WriteLine("Press eny key to exit");
            Console.ReadLine();
        }









        //private static void OnFileRename(object sender, RenamedEventArgs e)
        //{
        //    string result = TextFileReader(e.Name);
        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendLine("==File Name Changed==");
        //    sb.AppendLine($"Old Name -> {e.OldName}");
        //    sb.AppendLine($"New Name -> {e.Name}");
        //    sb.AppendLine(result);
            
        //    Console.WriteLine(sb.ToString().TrimEnd());
        //}

        //private static void OnActionOnFolderPath(object sender, FileSystemEventArgs e)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine($"==File {e.ChangeType}==");
        //    sb.AppendLine($"{e.ChangeType} -> {e.Name}");
        //    string result = TextFileReader(e.Name);
        //    sb.AppendLine(result);
        //    Console.WriteLine(sb.ToString().TrimEnd());
        //}
        //private static string TextFileReader(string name)
        //{   
        //    string swiftMessage = File.ReadAllText(@$"C:\Users\Ivan\OneDrive\Desktop\Folder\{name}");

        //    if (!String.IsNullOrEmpty(swiftMessage))
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        BasicHeaderBlock basicHeader = new BasicHeaderBlock();
        //        List<IBasicBlock> basicBlock = new List<IBasicBlock>();

        //        sb.AppendLine("MT103 Messages:");
        //        basicBlock = basicHeader.Create(swiftMessage);
        //        string result = MoveFile(basicHeader, basicBlock, name);
        //        sb.AppendLine(result);

        //        return sb.ToString().TrimEnd();
        //    }
        //    else
        //    {
        //        return $"{name} is Empty";
        //    }
        //}

        //private static string MoveFile(BasicHeaderBlock basicHeader, List<IBasicBlock> basicBlock, string name)
        //{
        //    MoveToFolder move = new MoveToFolder();
        //    string result = move.MoveFileToFolder(basicHeader,basicBlock,name);      
        //    return result;
        //}
    }
}