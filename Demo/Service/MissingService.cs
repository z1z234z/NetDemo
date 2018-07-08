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

        private readonly PrivateMessageDao privateMessageDao;

        public MissingService(DBContext context)
        {
            userDao = new UserDao(context);
            ownerDao = new OwnerDao(context);
            replyDao = new ReplyDao(context);
            replyCommentDao = new ReplyCommentDao(context);
            privateMessageDao = new PrivateMessageDao(context);
        }

        public bool saveReply(int id, String content, String account)
        {
            bool result = false;
            Owner owenr = null;
            User user = null;
            if (ownerDao.Select(id, null, null, null, null, null, null, null, null, 0).Count > 0)
            {
                owenr = ownerDao.Select(id, null, null, null, null, null, null, null, null, 0)[0];
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
            if (replyDao.Select(id, null, null, null, null, 0).Count > 0)
            {
                reply = replyDao.Select(id, null, null, null, null, 0)[0];
            }
            else
            {
                return result;
            }
            owner = reply.owner;
            if (userDao.Select(null, account, null, null, null, null, null, null, null, null, null, null, null).Count > 0)
            {
                user = userDao.Select(null, account, null, null, null, null, null, null, null, null, null, null, null)[0];
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

        public bool saveMessage(String account, String touser, String content, String url)
        {
            bool result = false;
            User sender = null;
            User receiver = null;
            if (userDao.Select(null, account, null, null, null, null, null, null, null, null, null, null, null).Count > 0)
            {
                sender = userDao.Select(null, account, null, null, null, null, null, null, null, null, null, null, null)[0];
            }
            else
            {
                return result;
            }
            if (userDao.Select(null, touser, null, null, null, null, null, null, null, null, null, null, null).Count > 0)
            {
                receiver = userDao.Select(null, touser, null, null, null, null, null, null, null, null, null, null, null)[0];
            }
            else
            {
                return result;
            }
            PrivateMessage privateMessage = new PrivateMessage();
            privateMessage.content = content;
            privateMessage.Receiver = receiver;
            privateMessage.time = DateTime.Now;
            privateMessage.Sender = sender;
            privateMessage.source = url;
            if (privateMessageDao.Create(privateMessage))
            {
                result = true;
            }
            return result;
        }
        public Owner getDetail(int id)
        {
            Owner owner = null;
            if (ownerDao.Select(id, null, null, null, null, null, null, null, null, 0).Count > 0)
            {
                owner = ownerDao.Select(id, null, null, null, null, null, null, null, null, 0)[0];
            }
            return owner;
        }

        public Reply getReply(int id)
        {
            Reply reply = null;
            if (replyDao.Select(id, null, null, null, null, 0).Count > 0)
            {
                reply = replyDao.Select(id, null, null, null, null, 0)[0];
            }
            return reply;
        }

        public bool completed(Owner owner)
        {
            return ownerDao.Edit(owner);
        }

        public List<Reply> getReplyListByOwner(Owner owner)
        {
            List<Reply> list = new List<Reply>();
            if (replyDao.Select(null, null, null, null, owner, 0).Count > 0)
            {
                list = replyDao.Select(null, null, null, null, owner, 0);
            }
            return list;
        }

        public List<ReplyComment> getReplyCommentListByReply(Reply reply)
        {
            List<ReplyComment> list = new List<ReplyComment>();
            if (replyCommentDao.Select(null, null, null, null, reply).Count > 0)
            {
                list = replyCommentDao.Select(null, null, null, null, reply);
            }
            return list;
        }
    }
}
