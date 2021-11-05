using Icard.FileReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Icard.EventMethods
{
    public class Events
    {
        private static StringBuilder sb;
        private static readonly TextFileReader reader = new TextFileReader();
        public void OnFileRename(object sender, RenamedEventArgs e)
        {
            sb = new StringBuilder();
            string result = reader.FileReader(e.Name);
            sb.AppendLine("==File Name Changed==");
            sb.AppendLine($"Old Name -> {e.OldName}");
            sb.AppendLine($"New Name -> {e.Name}");
            sb.AppendLine(result);
            Console.WriteLine(sb.ToString());
        }
        public void OnActionOnFolderPath(object sender, FileSystemEventArgs e)
        {
            sb = new StringBuilder();
            sb.AppendLine($"==File {e.ChangeType}==");
            sb.AppendLine($"{e.ChangeType} -> {e.Name}");
            string result = reader.FileReader(e.Name);
            sb.AppendLine(result);
            Console.WriteLine(sb.ToString());
        }
    }
}