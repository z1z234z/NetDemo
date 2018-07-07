using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Dao
{
    public class FinderDao
    {
        private readonly DBContext _context;
        public FinderDao(DBContext context)
        {
            _context = context;
        }
        public List<Finder> Select(int? id, User user, String content, bool? complete, String question, String title, DateTime? time, DateTime? lasttime, LoseType losetype, bool? hidden, int index)
        {
            try
            {
                var items = from s in _context.Finders.Include("LoseType").Include("User")
                            where ((id == null) || s.ID == id) && ((user == null) || s.User == user) && ((content == null) || s.Content == content) && ((complete == null) || s.Complete == complete)
                             && ((question == null) || s.Question == question) && ((title == null) || s.Title == title) && ((time == null) || s.Time == time) && ((lasttime == null) || s.LastReplyTime == lasttime)
                             && ((losetype == null) || s.LoseType == losetype) && ((hidden == null) || s.hidden == hidden)
                            select s;
                if (index != 0)
                {
                    items = items.OrderByDescending(u => u.LastReplyTime).Skip(10 * (index - 1)).Take(10);
                }
                List<Finder> list = new List<Finder>();
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

        public bool Edit(Finder finder)
        {
            try
            {
                _context.Update(finder);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(Finder finder)
        {
            try
            {
                _context.Add(finder);
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
                Finder finder = _context.Finders.Find(id);
                if (finder != null)
                {
                    _context.Finders.Remove(finder);
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
