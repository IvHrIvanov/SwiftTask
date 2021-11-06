using System;
using System.Collections.Generic;
using System.Text;

namespace Icard.SwiftParse.UserHeaderBlock
{
    public interface IUserHeaderBlock
    {
        private static string BlockId = "{3:";
        public string BankingPriorityCode { get; set; }
        public string MessageUserReferenc { get; set; }
      
    }
}
