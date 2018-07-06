using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Dao;
using Demo.Models;

namespace Demo.Service
{
    public class ReleaseMissingService
    {
        private readonly LoseTypesDao loseTypesDao;

        private readonly OwnerDao ownerDao;

        private readonly FinderDao finderDao;

        private readonly UserDao userDao;

        public ReleaseMissingService(DBContext context)
        {
            loseTypesDao = new LoseTypesDao(context);
            ownerDao = new OwnerDao(context);
            finderDao = new FinderDao(context);
            userDao = new UserDao(context);
        }

        public bool saveInfomation(String title, String fathertype, String type, String content, String account)
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
            Owner owner = new Owner();
            owner.hidden = false;
            owner.Complete = false;
            owner.Time = DateTime.Now;
            owner.LastReplyTime = owner.Time;
            owner.LoseType = loseType;
            owner.Content = content;
            owner.User = user;
            owner.Title = title;
            if (ownerDao.Create(owner))
            {
                result = true;
            }
            return result;
        }
    }
}
