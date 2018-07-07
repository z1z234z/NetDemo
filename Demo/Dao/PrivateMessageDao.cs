using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Dao
{
    public class PrivateMessageDao
    {
        private readonly DBContext _context;
        public PrivateMessageDao(DBContext context)
        {
            _context = context;
        }
        public List<PrivateMessage> Select(int? id, User sender, User receiver, String content, String source, DateTime? time,int index)
        {
            try
            {
                var items = from s in _context.PrivateMessages.Include("Sender").Include("Receiver")
                            where ((id == null) || s.ID == id) && ((sender == null) || s.Sender == sender) && ((time == null) || s.time == time)
                                   && ((receiver == null) || s.Receiver == receiver) && ((content == null) || s.content == content) && ((source == null) || s.source == source)
                            select s;
                if (index != 0)
                {
                    items = items.OrderByDescending(u => u.time).Skip(50 * (index - 1)).Take(50);
                }
                List<PrivateMessage> list = new List<PrivateMessage>();
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

        public bool Edit(PrivateMessage privateMessage)
        {
            try
            {
                _context.Update(privateMessage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(PrivateMessage privateMessage)
        {
            try
            {
                _context.Add(privateMessage);
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
                PrivateMessage privateMessage = _context.PrivateMessages.Find(id);
                if (privateMessage != null)
                {
                    _context.PrivateMessages.Remove(privateMessage);
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
