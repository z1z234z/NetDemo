using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Dao
{
    public class OwnerDao
    {
        private readonly DBContext _context;
        public OwnerDao(DBContext context)
        {
            _context = context;
        }
        public IQueryable Select(int? id, User user, String content, bool? complete, DateTime time, LoseType losetype)
        {
            try
            {
                var items = from s in _context.Owners
                            where ((id == null) || s.ID == id) && ((user == null) || s.User == user) && ((content == null) || s.Content == content) && ((complete == null) || s.Complete == complete)
                            && ((time == null) || s.Time == time) && ((losetype == null) || s.LoseType == losetype)
                            select s;
                return items;
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
