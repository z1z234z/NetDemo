using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Dao
{
    public class AdminDao
    {
        private readonly DBContext _context;
        public AdminDao(DBContext context)
        {
            _context = context;
        }
        public List<Admin> Select(int? id, String account, String password, DateTime logintime, String email)
        {
            try
            {
                var items = from s in _context.Admins
                            where ((id == null) || s.ID == id) && ((account == null) || s.Account == account) && ((password == null) || s.Password == password)
                                   && ((logintime == null) || s.Last_Login_Time == logintime) && ((email == null) || s.Email == email)
                            select s;
                List<Admin> list = new List<Admin>();
                foreach(var item in items)
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

        public bool Edit(Admin admin)
        {
            try
            {
                _context.Update(admin);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(Admin admin)
        {
            try
            {
                _context.Add(admin);
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
                Admin admin = _context.Admins.Find(id);
                if (admin != null)
                {
                    _context.Admins.Remove(admin);
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
