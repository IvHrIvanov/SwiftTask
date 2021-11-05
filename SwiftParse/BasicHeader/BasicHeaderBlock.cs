using Icard.MoveFile;
using Icard.SwiftParse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Icard
{
    public class BasicHeaderBlock : IBasicBlock
    {
        private static readonly string BASIC_HEADER_PATTERN = @"({1:)([FAL])(01|21)([A-Z]{8}[^X][A-Z]{3})([0-9]{4})([0-9]{6}})";
        private List<string> addedBSHB = new List<string>();
        private string result = "";

        public static readonly string BLOCK_ID = "{1:";
        public string ApplicationID { get; set; }
        public string ServiceId { get; set; }
        public string LogicalAddres { get; set; }
        public string SessionNumber { get; set; }
        public string SequenceNumber { get; set; }
        public string BlockId { get { return BLOCK_ID; } }

        public List<IBasicBlock> Create(string swiftMessage)
        {
            List<IBasicBlock> basicHeaderBlock = new List<IBasicBlock>();

            var match = Regex.Match(swiftMessage, BASIC_HEADER_PATTERN);
            if (match.Success)
            {
                basicHeaderBlock.Add(new BasicHeaderBlock
                {
                    ApplicationID = match.Groups[2].Value,
                    ServiceId = match.Groups[3].Value,
                    LogicalAddres = match.Groups[4].Value,
                    SessionNumber = match.Groups[5].Value,
                    SequenceNumber = match.Groups[6].Value
                });
                AddParsetDataBasicHeaderBlock(basicHeaderBlock);
                return basicHeaderBlock;
            }
            else
            {
                return basicHeaderBlock;
            }
        }
        private void AddParsetDataBasicHeaderBlock(List<IBasicBlock> basicHeaderBlock)
        {
            foreach (var item in basicHeaderBlock)
            {
                 result = string.Concat(
                     item.BlockId.ToString(),
                     item.ApplicationID.ToString(),
                     item.ServiceId.ToString(),
                     item.LogicalAddres.ToString(),
                     item.SessionNumber.ToString(),
                     item.SequenceNumber.ToString()
                     );
            }
        }
        public string ReturnResult()
        {
            return result;
        }
    }
}