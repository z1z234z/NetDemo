using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Dao
{
    public class FinderDao
    {
        private readonly DBContext _context;
        public FinderDao(DBContext context)
        {
            _context = context;
        }
        public List<Finder> Select(int? id, User user, String content, bool? complete, String question, String answer, DateTime? time, LoseType losetype)
        {
            try
            {
                var items = from s in _context.Finders
                            where ((id == null) || s.ID == id) && ((user == null) || s.User == user) && ((content == null) || s.Content == content) && ((complete == null) || s.Complete == complete)
                             && ((question == null) || s.Question == question) && ((answer == null) || s.Answer == answer) && ((time == null) || s.Time == time) && ((losetype == null) || s.LoseType == losetype)
                            select s;
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
