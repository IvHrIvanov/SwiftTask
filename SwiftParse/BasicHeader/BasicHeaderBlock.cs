using Icard.SwiftParse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Icard
{
    public class BasicHeaderBlock : IBasicBlock
    {
        public static readonly string BLOCK_ID = "1";

        public char ApplicationID { get; set; }
        public string ServiceId { get; set; }
        public string LogicalAddres { get; set; }
        public string SessionNumber { get; set; }
        public string SequenceNumber { get; set; }
        public string BlockId { get { return BLOCK_ID; } }
        public  List<IBasicBlock> Create(string swiftMessage)
        {
            List<IBasicBlock> basicHeaderBlock = new List<IBasicBlock>();

            string pattern = @"(1:)([FAL])(01|21|03)([A-Z]{8}[^X][A-Z]{3})([0-9]{4})([0-9]{6})";
            var match = Regex.Match(swiftMessage,pattern);
            if(match.Success)
            {
                basicHeaderBlock.Add(new BasicHeaderBlock
                { 
                ApplicationID = match.Groups[1].Value[0],
                ServiceId = match.Groups[2].Value,
                LogicalAddres = match.Groups[3].Value,
                SessionNumber = match.Groups[4].Value,
                SequenceNumber = match.Groups[5].Value
                });
                return basicHeaderBlock;
            }
            else
            {             
                return basicHeaderBlock;
            }
        }
    }
}