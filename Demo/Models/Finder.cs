using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Finder
    {
        public int ID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        public string Content { get; set; }
        public bool Complete { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime Time { get; set; }
        public DateTime LastReplyTime { get; set; }
        [ForeignKey("LoseTypeID")]
        public LoseType LoseType { get; set; }
        public bool hidden { get; set; }
    }
}
