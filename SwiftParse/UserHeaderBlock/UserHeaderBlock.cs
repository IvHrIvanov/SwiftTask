using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Icard.SwiftParse.UserHeaderBlock
{
    public class UserHeaderBlock : IUserHeaderBlock
    {
        private static readonly string USER_HEADER_REGEX = @"({103:)([A-Z]{3}})|({113:)([A-Za-z]{4}})|({108:)([A-Z0-9]{16}})|({119:)(STP})|({115:)([A-Z0-9]{30}})";
        private static string Block_Id = "{3:";
        public string BlockId { get { return Block_Id; } }
        public string BankingPriorityCode { get; set; }
        public string MessageUserReferenc { get; set; }
        private static List<IUserHeaderBlock> messages;
        private static List<string> allMessages;
        public void AddAllMessagesToList(string input)
        {
            messages = new List<IUserHeaderBlock>();
            allMessages = new List<string>();
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
                            allMessages.Add(result);
                            break;
                        }
                    }
                }
            }
            AddMessageToUserHeaderBlockList();
        }
        public List<IUserHeaderBlock> ReturnAllMessages()
        {
            return messages;
        }

        private static void AddMessageToUserHeaderBlockList()
        {
            foreach (var currentMessage in allMessages)
            {
               
                var match = Regex.Match(currentMessage, USER_HEADER_REGEX);
               
                
                if (match.Success)
                {
                    for (int i = 1; i < match.Length; i++)
                    {
                        if (match.Groups[i].Value != "")
                        {
                            messages.Add(new UserHeaderBlock
                            {
                               
                                BankingPriorityCode = match.Groups[i].Value,
                                MessageUserReferenc = match.Groups[i+1].Value

                            });
                            break;
                        }
                    }
                }
                else
                {
                    messages = new List<IUserHeaderBlock>();
                    break;
                }

            }
        }

    }
}