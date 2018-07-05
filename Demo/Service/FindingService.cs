using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Demo.Dao;

namespace Demo.Service
{
    public class FindingService
    {
        private readonly UserDao userDao;

        private readonly FinderDao finderDao;

        private readonly ReplyDao replyDao;

        private readonly ReplyCommentDao replyCommentDao;

        public FindingService(DBContext context)
        {
            userDao = new UserDao(context);
            finderDao = new FinderDao(context);
            replyDao = new ReplyDao(context);
            replyCommentDao = new ReplyCommentDao(context);
        }

        public Finder getDetail(int id)
        {
            Finder finder = null;
            if (finderDao.Select(id, null, null, null, null, null, null, null, null, null, 0).Count > 0)
            {
                finder = finderDao.Select(id, null, null, null, null, null, null, null, null, null, 0)[0];
            }
            return finder;
        }

        public bool completed(Finder finder)
        {
            return finderDao.Edit(finder);
        }
    }
}
