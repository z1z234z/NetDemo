using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<LoseType> LoseTypes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Finder> Finders { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<ReplyComment> ReplyComments { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<Attention> Attentions { get; set; }
    }
}
