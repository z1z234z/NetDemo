using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class ReplyComment
    {
        public int ID { get; set; }
        public string ReplyContent { get; set; }
        public DateTime time { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("ReplierID")]
        public Reply Replier { get; set; }
    }
}
