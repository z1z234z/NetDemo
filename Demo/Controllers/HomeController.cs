using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.Service;
using System.Collections;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService service;

        public HomeController(DBContext context)
        {
            service = new HomeService(context);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult UserInformation()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult GetUserInfo()
        {
            Hashtable result = new Hashtable();
            //前端向后端发送数据
            string account = Request.Form["account"];
            User user = service.getUserInfo(account);
            if (user != null)
            {
                result.Add("profile", user.summary);
                result.Add("school", user.School);
                result.Add("username", user.UserName);
                result.Add("avatarURL", user.head);
                result.Add("id", user.ID);
                result.Add("account", user.Account);
                return Ok(new
                {
                    userinfo = result,
                    code = 200
                });
            }
            else
            {
                return Ok(new
                {
                    userinfo = result,
                    code = 200
                });
            }
        }

        [HttpPost]
        public IActionResult GetMissingByUser()
        {
            int code = 200;
            List<Owner> ownerlist = new List<Owner>();
            User user = null;
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string account = Request.Form["account"];
            string temp = Request.Form["pageindex"];
            int index = (temp == null) ? 0 : Convert.ToInt32(temp);
            user = service.getUserInfo(account);
            ownerlist = service.getOwnerByUser(user, index);
            var all = service.getOwnerByUser(user, 0);
            if (user == null)
            {
                code = 500;
            }
            else
            {
                int sum = 1;
                foreach (Owner item in ownerlist)
                {
                    String completetext = null;
                    if (item.Complete)
                    {
                        completetext = "已找到";
                    }
                    else
                    {
                        completetext = "未找到";
                    }
                    Hashtable table = new Hashtable();
                    table.Add("releasetime", item.Time);
                    table.Add("title", item.Title);
                    table.Add("pageindex", sum);
                    table.Add("complete", item.Complete);
                    table.Add("completetext", completetext);
                    table.Add("infourl", "/Missing/MissingDetail?id=" + item.ID.ToString());
                    table.Add("total", all.Count);
                    infolist.Add(table);
                    sum++;
                }
            }
            return Ok(new
            {
                result = infolist,
                code = code
            });
        }

        [HttpPost]
        public IActionResult GetFindingByUser()
        {
            int code = 200;
            List<Finder> finderlist = new List<Finder>();
            User user = null;
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string account = Request.Form["account"];
            string temp = Request.Form["pageindex"];
            int index = (temp == null) ? 0 : Convert.ToInt32(temp);
            user = service.getUserInfo(account);
            finderlist = service.getFinderByUser(user, index);
            var all = service.getFinderByUser(user, 0);
            if (user == null)
            {
                code = 500;
            }
            else
            {
                int sum = 1;
                foreach (Finder item in finderlist)
                {
                    String completetext = null;
                    if (item.Complete)
                    {
                        completetext = "已找到";
                    }
                    else
                    {
                        completetext = "未找到";
                    }
                    Hashtable table = new Hashtable();
                    table.Add("releasetime", item.Time);
                    table.Add("title", item.Title);
                    table.Add("pageindex", sum);
                    table.Add("complete", item.Complete);
                    table.Add("completetext", completetext);
                    table.Add("infourl", "/Finding/FinderDetail?id=" + item.ID.ToString());
                    table.Add("total", all.Count);
                    infolist.Add(table);
                    sum++;
                }
            }
            return Ok(new
            {
                result = infolist,
                code = code
            });
        }

        [HttpPost]
        public IActionResult GetReplyByUser()
        {
            int code = 200;
            List<Reply> replylist = new List<Reply>();
            User user = null;
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string account = Request.Form["account"];
            string temp = Request.Form["pageindex"];
            int index = (temp == null) ? 0 : Convert.ToInt32(temp);
            user = service.getUserInfo(account);
            replylist = service.getReplyByUser(user, index);
            var all = service.getReplyByUser(user, 0);
            if (user == null)
            {
                code = 500;
            }
            else
            {
                foreach (Reply item in replylist)
                {
                    Hashtable table = new Hashtable();
                    table.Add("title", item.owner.Title);
                    table.Add("releasetime", item.time);
                    table.Add("infourl", "/Missing/MissingDetail?id=" + item.owner.ID.ToString());
                    table.Add("total", all.Count);
                    infolist.Add(table);
                }
            }
            return Ok(new
            {
                result = infolist,
                code = code
            });
        }

        [HttpPost]
        public IActionResult GetPMByUser()
        {
            int code = 200;
            List<PrivateMessage> pmlist = new List<PrivateMessage>();
            User user = null;
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string account = Request.Form["account"];
            string temp = Request.Form["pageindex"];
            int index = (temp == null) ? 0 : Convert.ToInt32(temp);
            user = service.getUserInfo(account);
            pmlist = service.getPMByUser(user, index);
            var all = service.getPMByUser(user, 0);
            if (user == null)
            {
                code = 500;
            }
            else
            {
                foreach (PrivateMessage item in pmlist)
                {
                    String title = "";
                    int id = Convert.ToInt32(item.source.Split("?id=")[1]);
                    if (item.source.Contains("Missing"))
                    {
                        Owner owner = service.getOwnerById(id);
                        title = owner.Title;
                    }
                    else if (item.source.Contains("Finding"))
                    {
                        Finder finder = service.getFinderById(id);
                        title = finder.Title;
                    }
                    else
                    {
                        title = "default";
                    }
                    Hashtable table = new Hashtable();
                    Hashtable info = new Hashtable();
                    info.Add("username", item.Sender.UserName);
                    info.Add("avatarURL", item.Sender.head);
                    info.Add("id", item.Sender.ID);
                    info.Add("account", item.Sender.Account);
                    table.Add("senduserinfo", info);
                    table.Add("content", item.content);
                    table.Add("sourcetitle", title);
                    table.Add("sourceurl",item.source);
                    table.Add("sendtime",item.time);
                    table.Add("total", all.Count);
                    infolist.Add(table);
                }
            }
            return Ok(new
            {
                result = infolist,
                code = code
            });
        }

        [HttpPost]
        public IActionResult EditProfile()
        {
            int code = 200;
            bool result = false;
            User user = null;
            //前端向后端发送数据
            String profile = Request.Form["profile"];
            String account = Request.Form["account"];
            user = service.getUserInfo(account);
            if(User == null)
            {
                code = 500;
            }
            user.summary = profile;
            if (service.editProfile(user))
            {
                result = true;
            }
            return Ok(new
            {
                result = result,
                code = code,
            });
        }
    }
}
