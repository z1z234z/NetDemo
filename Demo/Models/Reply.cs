using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Reply
    {
        public int ID { get; set; }
        public string ReplyContent { get; set; }
        public DateTime time { get; set; }
        public User User { get; set; }
        public Owner owner { get; set; }
    }
}
