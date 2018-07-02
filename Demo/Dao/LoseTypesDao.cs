using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Dao
{
    public class LoseTypesDao
    {
        private readonly DBContext _context;
        public LoseTypesDao(DBContext context)
        {
            _context = context;
        }
        public IQueryable Select(int? id, String name)
        {
            try
            {
                var items = from s in _context.LoseTypes
                            where ((id == null) || s.ID == id) && ((name == null) || s.Name == name)
                            select s;
                return items;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return null;
            }
        }

        public bool Edit(LoseType type)
        {
            try
            {
                _context.Update(type);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(LoseType type)
        {
            try
            {
                _context.Add(type);
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
                LoseType type = _context.LoseTypes.Find(id);
                if (type != null)
                {
                    _context.LoseTypes.Remove(type);
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
