using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class PrivateMessage
    {
        public int ID { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string content { get; set; }
        public string source { get; set; }
        public DateTime time { get; set; }
    }
}
