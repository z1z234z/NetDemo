﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebSoket
{
    public class MsgTemplate
    {
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string MessageType { get; set; }
        public string Content { get; set; }
    }
}
