using Icard.MoveFile;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Icard.SwiftParse.ApplicationHeaderBlock
{
    public class ApplicationHeader : IApplication
    {
        private static readonly string BLOCK_ID = "{2:";

        private static readonly List<IApplication> applicationHeader = new List<IApplication>();

        private static readonly string INPUT_PATTERN = @"({2:)(I[0-9]{3})([A-Z]{8})([X])([A-Z]{3})([S|N|U])([1|2|3])(003|020})";

        private static readonly string OUTPUT_PATTERN = @"({2:)(O)([0-9]{3})([0-9]{4})([0-9A-Z]{28})([0-9]{6})([0-9]{4})([S|N|U]})";
        public string BlockID { get { return BLOCK_ID; } }

        private string result = "";

        public string Input { get; set; }
        public string MessageType { get; set; }
        public string Address { get; set; }
        public string MessagePriority { get; set; }
        public string Delivery { get; set; }
        public string Period { get; set; }
        public string Priority { get; set; }

        public List<IApplication> Create(string swiftMessage)
        {

            var matchInput = Regex.Match(swiftMessage, INPUT_PATTERN);
            var matchOutput = Regex.Match(swiftMessage, OUTPUT_PATTERN);

            if (matchInput.Success)
            {
                applicationHeader.Add(new ApplicationHeader
                {
                    Input = matchInput.Groups[1].Value,
                    MessageType = matchInput.Groups[2].Value,
                    Address = matchInput.Groups[3].Value,
                    MessagePriority = matchInput.Groups[4].Value,
                    Delivery = matchInput.Groups[5].Value,
                    Period = matchInput.Groups[6].Value
                });
                AddParsetDataApplicationBlockInput(applicationHeader);
            }
            else if (matchOutput.Success)
            {
                applicationHeader.Add(new ApplicationHeader
                {
                    Input = matchOutput.Groups[2].Value,
                    MessageType = matchOutput.Groups[3].Value,
                    Address = matchOutput.Groups[4].Value,
                    MessagePriority = matchOutput.Groups[5].Value,
                    Delivery = matchOutput.Groups[6].Value,
                    Period = matchOutput.Groups[7].Value,
                    Priority = matchOutput.Groups[8].Value
                });
                AddParsetDataApplicationBlockOutput(applicationHeader);
            }
            return applicationHeader;
        }
        private void AddParsetDataApplicationBlockInput(List<IApplication> aplicationHeader)
        {
            foreach (var item in aplicationHeader)
            {
                result = string.Concat(
                    item.BlockID.ToString(),
                    item.Input.ToString(),
                    item.MessageType.ToString(),
                    item.MessagePriority.ToString(),
                    item.Delivery.ToString(),
                    item.Period.ToString()
                    );
            }
        }
        private void AddParsetDataApplicationBlockOutput(List<IApplication> aplicationHeader)
        {
            foreach (var item in aplicationHeader)
            {
                result = string.Concat(
                    item.BlockID.ToString(),
                    item.Input.ToString(),
                    item.MessageType.ToString(),
                    item.MessagePriority.ToString(),
                    item.Delivery.ToString(),
                    item.Period.ToString(),
                    item.Priority.ToString()
                    );
            }
        }
        public string ReturnResult()
        {
            return result;
        }
    }
}