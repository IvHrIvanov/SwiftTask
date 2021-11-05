using System;
using System.Collections.Generic;
using System.Text;

namespace Icard.SwiftParse.UserHeaderBlock
{
    public class UserHeaderBlock
    {
        public void Substring(string input)
        {
            List<string> output = new List<string>();
            string subString = input.Substring(3, input.Length - 4);
            for (int i = 0; i < subString.Length; i++)
            {
                string result = "";
                string currentString = subString[i].ToString();
                if (currentString == "{")
                {
                    for (int a = i; a < subString.Length; a++)
                    {
                        result += subString[a];
                        if (subString[a] == '}')
                        {
                            output.Add(result);
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(string.Join(" ", output));
        }
    }
}