using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Reply
    {
        public int ID { get; set; }
        public string ReplyContent { get; set; }
        public DateTime time { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("ownerID")]
        public Owner owner { get; set; }
    }
}
