﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Demo.Dao;

namespace Demo.Service
{
    public class LoginService
    {
        private readonly UserDao userDao;

        public LoginService(DBContext context)
        {
            userDao = new UserDao(context);
        }

        public bool AccountComfirm(string account, string password)
        {
            bool result = false;
            var items = userDao.Select(null, account, password, null, null, null, null, null, null, null);
            foreach(var item in items)
            {
                result = true;
            }
            return result;
        }

        public bool CheckAccount(String account)
        {
            bool result = true;
            var items = userDao.Select(null, account, null, null, null, null, null, null, null, null);
            foreach (var item in items)
            {
                result = false;
            }
            return result;
        }

        public bool Register(User user)
        {
            return userDao.Create(user);
        }
    }
}
