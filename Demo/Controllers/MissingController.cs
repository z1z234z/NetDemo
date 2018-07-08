using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.Service;
using System.Collections;

namespace Demo.Controllers
{
    public class MissingController : Controller
    {
        private readonly MissingService service;

        public MissingController(DBContext context)
        {
            service = new MissingService(context);

        }
        public IActionResult MissingDetail(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                Owner owner = service.getDetail(id.Value);
                return View(owner);
            }
        }

        public IActionResult test()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reply()
        {
            bool result = false;
            //前端向后端发送数据
            String temp = Request.Form["id"];
            String content = Request.Form["content"];
            String account = Request.Form["account"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            if (service.saveReply(id, content, account))
            {
                result = true;
            }
            return Ok(new
            {
                result = result,
                code = 200,
            });
        }

        [HttpPost]
        public IActionResult Comment()
        {
            bool result = false;
            //前端向后端发送数据
            String temp = Request.Form["id"];
            String content = Request.Form["content"];
            String account = Request.Form["account"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            if (service.saveComment(id, content, account))
            {
                result = true;
            }
            return Ok(new
            {
                result = result,
                code = 200,
            });
        }

        [HttpPost]
        public IActionResult Message()
        {
            bool result = false;
            //前端向后端发送数据
            String sourceurl = Request.Form["sourceurl"];
            String touser = Request.Form["touser"];
            String content = Request.Form["content"];
            String account = Request.Form["account"];
            if (service.saveMessage(account, touser, content, sourceurl))
            {
                result = true;
            }
            return Ok(new
            {
                result = result,
                code = 200,
            });
        }

        [HttpPost]
        public IActionResult Detail()
        {
            Hashtable result = new Hashtable();
            //前端向后端发送数据
            String temp = Request.Form["id"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            Owner owner = service.getDetail(id);
            if (owner != null)
            {
                result.Add("complete", owner.Complete);
                result.Add("missingContent", owner.Content);
                result.Add("createtime", owner.Time);
                result.Add("type", owner.LoseType);
                result.Add("account", owner.User.Account);
                result.Add("missingTitle", owner.Title);
                result.Add("missingId", owner.ID);
                return Ok(new
                {
                    result = result,
                    code = 200,
                });
            }
            else
            {
                return Ok(new
                {
                    result = result,
                    code = 500,
                });
            }
        }


        [HttpPost]
        public IActionResult Completed()
        {
            bool result = false;
            //前端向后端发送数据
            String temp = Request.Form["id"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            Owner owner = service.getDetail(id);
            if (owner != null)
            {
                owner.Complete = true;
                result = service.completed(owner);
                if (result)
                {
                    return Ok(new
                    {
                        result = result,
                        code = 200
                    });
                }
                else
                {
                    return Ok(new
                    {
                        result = result,
                        code = 500
                    });
                }
            }
            else
            {
                return Ok(new
                {
                    result = result,
                    code = 500
                });
            }
        }

        [HttpPost]
        public IActionResult DetailReply()
        {
            //前端向后端发送数据
            String temp = Request.Form["id"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            Owner owner = service.getDetail(id);
            List<Hashtable> result = new List<Hashtable>();
            if (owner != null)
            {
                List<Reply> replylist = service.getReplyListByOwner(owner);
                foreach (Reply item in replylist)
                {
                    Hashtable table = new Hashtable();
                    Hashtable account = new Hashtable();
                    List<ReplyComment> replyComments = service.getReplyCommentListByReply(item);
                    account.Add("username", item.User.UserName);
                    account.Add("avatarURL", item.User.head);
                    table.Add("account", account);
                    table.Add("replyDateTime", item.time);
                    table.Add("replyContent", item.ReplyContent);
                    table.Add("commentCount", replyComments.Count);
                    table.Add("id", item.ID);
                    result.Add(table);
                }
                return Ok(new
                {
                    result = result,
                    code = 200,
                });
            }
            else
            {
                return Ok(new
                {
                    result = result,
                    code = 500,
                });
            }
        }

        [HttpPost]
        public IActionResult DetailComment()
        {
            //前端向后端发送数据
            String temp = Request.Form["id"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            Reply reply = service.getReply(id);
            List<Hashtable> result = new List<Hashtable>();
            if (reply != null)
            {
                List<ReplyComment> replyComments = service.getReplyCommentListByReply(reply);
                foreach (ReplyComment item in replyComments)
                {
                    Hashtable table = new Hashtable();
                    Hashtable commenter = new Hashtable();
                    commenter.Add("id", item.ID);
                    commenter.Add("username", item.User.UserName);
                    commenter.Add("avatarURL", item.User.head);
                    table.Add("commenter", commenter);
                    table.Add("commentContent", item.ReplyContent);
                    table.Add("commentDateTime", item.time);
                    result.Add(table);
                }
                return Ok(new
                {
                    result = result,
                    code = 200,
                });
            }
            else
            {
                return Ok(new
                {
                    result = result,
                    code = 500,
                });
            }
        }
    }
}