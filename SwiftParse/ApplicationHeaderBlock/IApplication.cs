using System;
using System.Collections.Generic;
using System.Text;

namespace Icard.SwiftParse.ApplicationHeaderBlock
{
    public interface IApplication
    {
        string BlockID { get; }
        string Input { get; set; }
        string MessageType { get; set; }
        string Address { get; set; }
        string MessagePriority { get; set; }
        string Delivery { get; set; }
        string Period { get; set; }
        string Priority { get; set; }
        public List<IApplication> Create(string swiftMessage);
       
    }
}
