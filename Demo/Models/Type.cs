using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class LoseType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("FatherTypeID")]
        public LoseType FatherType { get; set; }
    }
}
