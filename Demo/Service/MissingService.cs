using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Demo.Dao;

namespace Demo.Service
{
    public class MissingService
    {
        private readonly UserDao userDao;

        private readonly OwnerDao ownerDao;

        private readonly ReplyDao replyDao;

        private readonly ReplyCommentDao replyCommentDao;

        public MissingService(DBContext context)
        {
            userDao = new UserDao(context);
            ownerDao = new OwnerDao(context);
            replyDao = new ReplyDao(context);
            replyCommentDao = new ReplyCommentDao(context);
        }

        public bool saveReply(int id, String content, String account)
        {
            bool result = false;
            Owner owenr = null;
            User user = null;
            if (ownerDao.Select(id, null, null, null, null, null, null, null, 0).Count > 0)
            {
                owenr = ownerDao.Select(id, null, null, null, null, null, null, null, 0)[0];
            }
            else
            {
                return result;
            }
            if (userDao.Select(null, account, null, null, null, null, null, null, null, null, null).Count > 0)
            {
                user = userDao.Select(null, account, null, null, null, null, null, null, null, null, null)[0];
            }
            else
            {
                return result;
            }
            Reply reply = new Reply();
            reply.owner = owenr;
            reply.ReplyContent = content;
            reply.time = DateTime.Now;
            reply.User = user;
            owenr.LastReplyTime = reply.time;
            if (replyDao.Create(reply) && ownerDao.Edit(owenr))
            {
                result = true;
            }
            return result;
        }

        public bool saveComment(int id, String content, String account)
        {
            bool result = false;
            Reply reply = null;
            User user = null;
            Owner owner = null;
            if (replyDao.Select(id, null, null, null, null).Count > 0)
            {
                reply = replyDao.Select(id, null, null, null, null)[0];
            }
            else
            {
                return result;
            }
            owner = reply.owner;
            if (userDao.Select(null, account, null, null, null, null, null, null, null, null, null).Count > 0)
            {
                user = userDao.Select(null, account, null, null, null, null, null, null, null, null, null)[0];
            }
            else
            {
                return result;
            }
            ReplyComment replycomment = new ReplyComment();
            replycomment.Replier = reply;
            replycomment.ReplyContent = content;
            replycomment.time = DateTime.Now;
            replycomment.User = user;
            owner.LastReplyTime = replycomment.time;
            if (replyCommentDao.Create(replycomment) && ownerDao.Edit(owner))
            {
                result = true;
            }
            return result;
        }
        public Owner getDetail(int id)
        {
            bool result = false;
            Reply reply = null;
            User user = null;
            Owner owner = null;
            if (ownerDao.Select(id, null, null, null, null, null, null, null, 0).Count > 0)
            {
                owner = ownerDao.Select(id, null, null, null, null, null, null, null, 0)[0];
            }
            return owner;
        }
    }
}
