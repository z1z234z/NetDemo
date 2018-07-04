using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class PrivateMessage
    {
        public int ID { get; set; }
        [ForeignKey("SenderID")]
        public User Sender { get; set; }
        [ForeignKey("ReceiverID")]
        public User Receiver { get; set; }
        public string content { get; set; }
        public string source { get; set; }
        public DateTime time { get; set; }
    }
}
