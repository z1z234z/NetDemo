using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Dao
{
    public class ReplyDao
    {
        private readonly DBContext _context;
        public ReplyDao(DBContext context)
        {
            _context = context;
        }
        public List<Reply> Select(int? id, String replycontent, DateTime? time, User user, Owner owner)
        {
            try
            {
                var items = from s in _context.Replies.Include("owner").Include("User")
                            where ((id == null) || s.ID == id) && ((replycontent == null) || s.ReplyContent == replycontent) && ((time == null) || s.time == time)
                                   && ((user == null) || s.User == user) && ((owner == null) || s.owner == owner)
                            select s;
                List<Reply> list = new List<Reply>();
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

        public bool Edit(Reply reply)
        {
            try
            {
                _context.Update(reply);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(Reply reply)
        {
            try
            {
                _context.Add(reply);
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
                Reply reply = _context.Replies.Find(id);
                if (reply != null)
                {
                    _context.Replies.Remove(reply);
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
