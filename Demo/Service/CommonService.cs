﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Dao;
using Demo.Models;


namespace Demo.Service
{
    public class CommonService
    {
        private readonly LoseTypesDao loseTypesDao;

        private readonly OwnerDao ownerDao;

        private readonly FinderDao finderDao;

        private readonly UserDao userDao;

        public CommonService(DBContext context)
        {
            loseTypesDao = new LoseTypesDao(context);
            ownerDao = new OwnerDao(context);
            finderDao = new FinderDao(context);
            userDao = new UserDao(context);
        }

        public List<LoseType> GetLoseTypes()
        {
            return loseTypesDao.Select(null, null, null);
        }

        public List<LoseType> GetChildrenLoseTypes(LoseType fathertype)
        {
            return loseTypesDao.Select(null, null, fathertype);
        }

        public User getUserInfo(String account)
        {
            User user = null;
            if (userDao.Select(null, account, null, null, null, null, null, null, null, null, null, null, null).Count > 0)
            {
                user = userDao.Select(null, account, null, null, null, null, null, null, null, null, null, null, null)[0];
            }
            return user;
        }
    }
}
