using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Owner
    {
        public int ID { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public bool Complete { get; set; }
        public DateTime Time { get; set; }
        public DateTime LastReplyTime { get; set; }
        public LoseType LoseType { get; set; }
        public bool hidden { get; set; }
    }
}
