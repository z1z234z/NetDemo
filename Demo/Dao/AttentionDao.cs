using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.Dao
{
    public class AttentionDao
    {
        private readonly DBContext _context;
        public AttentionDao(DBContext context)
        {
            _context = context;
        }
        public List<Attention> Select(int? id, User sender, User receiver, String content, String source, DateTime? time, bool? read)
        {
            try
            {
                var items = from s in _context.Attentions.Include("Sender").Include("Receiver")
                            where ((id == null) || s.ID == id) && ((sender == null) || s.Sender == sender) && ((time == null) || s.time == time) && ((read == null) || s.read == read)
                                   && ((receiver == null) || s.Receiver == receiver) && ((content == null) || s.content == content) && ((source == null) || s.source == source)
                            select s;
                List<Attention> list = new List<Attention>();
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

        public bool Edit(Attention attention)
        {
            try
            {
                _context.Update(attention);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public bool Create(Attention attention)
        {
            try
            {
                _context.Add(attention);
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
                Attention attention = _context.Attentions.Find(id);
                if (attention != null)
                {
                    _context.Attentions.Remove(attention);
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
