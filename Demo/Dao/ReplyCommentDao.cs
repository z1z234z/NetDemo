using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Dao
{
    public class ReplyCommentDao
    {
        private readonly DBContext _context;
        public ReplyCommentDao(DBContext context)
        {
            _context = context;
        }
        public List<ReplyComment> Select(int? id, String replycontent, DateTime? time, User user, Reply replier)
        {
            try
            {
                var items = from s in _context.ReplyComments.Include("Replier").Include("User")
                            where ((id == null) || s.ID == id) && ((replycontent == null) || s.ReplyContent == replycontent) && ((time == null) || s.time == time)
                                   && ((user == null) || s.User == user) && ((replier == null) || s.Replier == replier)
                            select s;
                List<ReplyComment> list = new List<ReplyComment>();
                foreach (var item in items)
                {
                    list.Add(item);
                }
                return list;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return null;
            }
        }

        public bool Edit(ReplyComment replyComment)
        {
            try
            {
                _context.Update(replyComment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(ReplyComment replyComment)
        {
            try
            {
                _context.Add(replyComment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Delete(int? id)
        {
            try
            {
                ReplyComment replyComment = _context.ReplyComments.Find(id);
                if (replyComment != null)
                {
                    _context.ReplyComments.Remove(replyComment);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }
    }
}
