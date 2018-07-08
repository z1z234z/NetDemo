using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Dao
{
    public class OwnerDao
    {
        private readonly DBContext _context;
        public OwnerDao(DBContext context)
        {
            _context = context;
        }
        public List<Owner> Select(int? id, User user, String content, bool? complete, DateTime? time, DateTime? lasttime, LoseType losetype, bool? hidden, String title, int index)
        {
            try
            {
                var items = from s in _context.Owners.Include("LoseType").Include("User")
                            where ((id == null) || s.ID == id) && ((user == null) || s.User == user) && ((content == null) || s.Content == content) && ((complete == null) || s.Complete == complete)
                            && ((time == null) || s.Time == time) && ((lasttime == null) || s.LastReplyTime == lasttime) && ((losetype == null) || s.LoseType == losetype) && ((hidden == null) || s.hidden == hidden) && ((title == null) || s.Title == title)
                            select s;
                if (index != 0)
                {
                    items = items.OrderByDescending(u => u.LastReplyTime).Skip(10 * (index - 1)).Take(10);
                }
                List<Owner> list = new List<Owner>();
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

        public List<Owner> SearchSelect(String content,String title, LoseType losetype, int index)
        {
            try
            {
                var items = from s in _context.Owners.Include("LoseType").Include("User")
                            where (s.Content.Contains(content)) || (s.Title.Contains(title)) && ((losetype == null) || s.LoseType == losetype)
                            select s;
                if (index != 0)
                {
                    items = items.OrderByDescending(u => u.LastReplyTime).Skip(10 * (index - 1)).Take(10);
                }
                List<Owner> list = new List<Owner>();
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


        public bool Edit(Owner owner)
        {
            try
            {
                _context.Update(owner);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(Owner owner)
        {
            try
            {
                _context.Add(owner);
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
                Owner owner = _context.Owners.Find(id);
                if (owner != null)
                {
                    _context.Owners.Remove(owner);
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
