using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Icard.SwiftParse.ApplicationHeaderBlock
{
    public class ApplicationHeader : IAplication
    {
        public static readonly string BLOCK_ID = "2";
        private static List<IAplication> aplicationHeader = new List<IAplication>();


        public string BlockID { get { return BLOCK_ID; } }

        public string Input { get; set; }
        public string MessageType { get; set; }
        public string Address { get; set; }
        public string Priority { get; set; }
        public string Delivery { get; set; }
        public string Period { get; set; }

        public List<IAplication> Create(string swiftMessage)
        {
            string inputPattern = @"({2:)(I[0-9]{3}|[A-Z]{8}[X][A-Z]{3})([S|N|U])([1|2|3])([0-9]{3}})";
            var match = Regex.Match(swiftMessage, inputPattern);
        }
    }
}
//List<IBasicBlock> basicHeaderBlock = new List<IBasicBlock>();

//string pattern = @"(1:)([FAL])(01|21|03)([A-Z]{8}[^X][A-Z]{3})([0-9]{4})([0-9]{6})";
//var match = Regex.Match(swiftMessage, pattern);
//if (match.Success)
//{
//    basicHeaderBlock.Add(new BasicHeaderBlock
//    {
//        ApplicationID = match.Groups[1].Value[0],
//        ServiceId = match.Groups[2].Value,
//        LogicalAddres = match.Groups[3].Value,
//        SessionNumber = match.Groups[4].Value,
//        SequenceNumber = match.Groups[5].Value
//    });
//    return basicHeaderBlock;
//}
//else
//{
//    return basicHeaderBlock;
//}