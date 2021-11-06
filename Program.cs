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
         
            watcher.Created += methods.OnActionOnFolderPath;
            watcher.Renamed += methods.OnFileRename;

            Console.WriteLine("Press eny key to exit");
            Console.ReadLine();
        }
    }
}