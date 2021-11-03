using System;
using System.Collections.Generic;
using System.Text;

namespace Icard.SwiftParse.ApplicationHeaderBlock
{
    public interface IAplication
    {
        string BlockID { get; }
        string Input { get; set; }
        string MessageType { get; set; }
        string Address { get; set; }
        string Priority { get; set; }
        string Delivery { get; set; }
        string Period { get; set; }

        public List<IAplication> Create(string swiftMessage);
       
    }
}
