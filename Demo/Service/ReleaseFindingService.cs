using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Dao;
using Demo.Models;

namespace Demo.Service
{
    public class ReleaseFindingService
    {
        private readonly LoseTypesDao loseTypesDao;

        private readonly FinderDao finder;

        private readonly FinderDao finderDao;

        private readonly UserDao userDao;

        public ReleaseFindingService(DBContext context)
        {
            loseTypesDao = new LoseTypesDao(context);
            finderDao = new FinderDao(context);
            finderDao = new FinderDao(context);
            userDao = new UserDao(context);
        }
        public bool saveInfomation(String title, String fathertype, String type, String content, String account, String question)
        {
            bool result = false;
            LoseType loseType = null;
            LoseType fatherType = fatherType = loseTypesDao.Select(null, fathertype, null)[0];
            User user = null;
            if (loseTypesDao.Select(null, type, fatherType).Count > 0)
            {
                loseType = loseTypesDao.Select(null, type, fatherType)[0];
            }
            else
            {
                return result;
            }
            if (userDao.Select(null, account, null, null, null, null, null, null, null, null, null, null, null).Count > 0)
            {
                user = userDao.Select(null, account, null, null, null, null, null, null, null, null, null, null, null)[0];
            }
            else
            {
                return result;
            }
            Finder finder = new Finder();
            finder.hidden = false;
            finder.Complete = false;
            finder.Time = DateTime.Now;
            finder.LastReplyTime = finder.Time;
            finder.LoseType = loseType;
            finder.Content = content;
            finder.User = user;
            finder.Question = question;
            finder.Title = title;
            if (finderDao.Create(finder))
            {
                result = true;
            }
            return result;
        }
    }
}
