﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Admin
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public DateTime Last_Login_Time { get; set; }
        public string Email { get; set; }
    }
}
