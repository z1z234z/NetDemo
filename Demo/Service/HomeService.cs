using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Dao;
using Demo.Models;

namespace Demo.Service
{
    public class HomeService
    {
        private readonly LoseTypesDao loseTypesDao;

        private readonly OwnerDao ownerDao;

        private readonly FinderDao finderDao;

        private readonly UserDao userDao;

        private readonly ReplyDao replyDao;

        private readonly PrivateMessageDao privateMessageDao;

        private readonly AttentionDao attentionDao;

        public HomeService(DBContext context)
        {
            loseTypesDao = new LoseTypesDao(context);
            ownerDao = new OwnerDao(context);
            finderDao = new FinderDao(context);
            replyDao = new ReplyDao(context);
            privateMessageDao = new PrivateMessageDao(context);
            attentionDao = new AttentionDao(context);
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

        public List<Owner> getOwnerByUser(User user)
        {
            return ownerDao.Select(null, user, null, null, null, null, null, null, null, 0);
        }

        public List<Finder> getFinderByUser(User user)
        {
            return finderDao.Select(null, user, null, null, null, null, null, null, null, null, 0);
        }

        public List<Reply> getReplyByUser(User user)
        {
            return replyDao.Select(null, null, null, user, null);
        }

        public List<PrivateMessage> getPMByUser(User user)
        {
            return privateMessageDao.Select(null, null, user, null, null, null);
        }

        public Owner getOwnerById(int id)
        {
            Owner owner = null;
            if (ownerDao.Select(id, null, null, null, null, null, null, null, null, 0).Count > 0)
            {
                owner = ownerDao.Select(id, null, null, null, null, null, null, null, null, 0)[0];
            }
            return owner;
        }

        public Finder getFinderById(int id)
        {
            Finder finder = null;
            if (finderDao.Select(id, null, null, null, null, null, null, null, null, null, 0).Count > 0)
            {
                finder = finderDao.Select(id, null, null, null, null, null, null, null, null, null, 0)[0];
            }
            return finder;
        }

        public bool editProfile(User user)
        {
            return userDao.Edit(user);
        }
    }
}
