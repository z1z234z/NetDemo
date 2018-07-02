using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.Dao
{
    public class UserDao
    {
        private readonly DBContext _context;
        public UserDao(DBContext context)
        {
            _context = context;
        }
        public List<User> Select(int? id, String account, String password, String name, String sex, String age, String school, String phone, String address, String email)
        {
            try
            {
                var items = from s in _context.Users
                            where ((id == null) || s.ID == id) && ((account == null) || s.Account == account) && ((password == null) || s.Password == password) && ((name == null) || s.Name == name)
                            && ((sex == null) || s.Sex == sex) && ((age == null) || s.Age == age) && ((school == null) || s.School == school) && ((phone == null) || s.Phone == phone) &&
                            ((address == null) || s.Address == address) && ((email == null) || s.email == email)
                            select s;
                List<User> list = new List<User>();
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

        public bool Edit(User user)
        {
            try
            {
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }catch(Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(User user)
        {
            try
            {
                _context.Add(user);
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
                User user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

    }
}
