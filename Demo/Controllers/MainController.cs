﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Service;
using Demo.Models;
using System.Collections;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    public class MainController : Controller
    {
        private readonly MainService service;

        public MainController(DBContext context)
        {

            service = new MainService(context);

        }
        // GET: /<controller>/
        public IActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public IActionResult getType()
        {
            int code = 200;
            List<Hashtable> typelist = new List<Hashtable>();
            //前端向后端发送数据
            List<LoseType> list = service.GetLoseTypes();
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].FatherType == null)
                {
                    Hashtable table = new Hashtable();
                    table.Add("index", (i + 1).ToString());
                    table.Add("type", list[i].Name);
                    List<LoseType> childrenlists = service.GetChildrenLoseTypes(list[i]);
                    List<Hashtable> childrentypelist = new List<Hashtable>();
                    for (int j = 0; j < childrenlists.Count; j++)
                    {
                        Hashtable childrentable = new Hashtable();
                        childrentable.Add("index", (i + 1).ToString() + "-" + (j + 1).ToString());
                        childrentable.Add("type", childrenlists[j].Name);
                        childrentypelist.Add(childrentable);
                    }
                    table.Add("children", childrentypelist);
                    typelist.Add(table);
                }
            }
            if (list == null)
            {
                code = 500;
            }
            return Ok(new
            {
                typelist = typelist,
                code = code
            });
        }

        [HttpPost]
        public IActionResult getOwnerByType()
        {
            int code = 200;
            List<Owner> list = new List<Owner>();
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string type = Request.Form["type"];
            string temp = Request.Form["index"];
            int index = (temp == "" || type == null) ? 0 : Convert.ToInt32(temp);
            type = (type == "" || type == null) ? null : type;
            list = service.GetOwnerByType(type, index);
            var all = service.GetOwnerByType(type, 0);
            if (list == null)
            {
                code = 500;
            }
            else{
                foreach(Owner item in list){
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
                    table.Add("id",item.User.ID);
                    table.Add("avatarURL", item.User.head);
                    table.Add("username", item.User.UserName);
                    table.Add("infoId", item.ID);
                    table.Add("infoTitle", item.Title);
                    table.Add("fatherType", item.LoseType.FatherType.Name);
                    table.Add("type", item.LoseType.Name);
                    table.Add("hidden", item.hidden);
                    table.Add("complete", item.Complete);
                    table.Add("completetext", completetext);
                    table.Add("infourl", "/Missing/MissingDetail?id="+item.ID.ToString());
                    infolist.Add(table);
                }
            }
            return Ok(new
            {
                infolist = infolist,
                total = all.Count,
                code = code
            });
        }

        [HttpPost]
        public IActionResult getFinderByType()
        {
            int code = 200;
            List<Finder> list = new List<Finder>();
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string type = Request.Form["type"];
            string temp = Request.Form["index"];
            int index = (temp == "" || type == null) ? 0 : Convert.ToInt32(temp);
            type = (temp == "" || type == null) ? null : type;
            list = service.GetFinderByType(type, index);
            var all = service.GetFinderByType(type, 0);
            if (list == null)
            {
                code = 500;
            }
            else
            {
                foreach (Finder item in list)
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
                    LoseType losttype = service.GetLoseTypesBuName(item.LoseType.Name);
                    Hashtable table = new Hashtable();
                    table.Add("id", item.User.ID);
                    table.Add("avatarURL", item.User.head);
                    table.Add("username", item.User.UserName);
                    table.Add("infoId", item.ID);
                    table.Add("infoTitle", item.Title);
                    table.Add("fatherType", losttype.FatherType.Name);
                    table.Add("type", item.LoseType.Name);
                    table.Add("hidden", item.hidden);
                    table.Add("complete", item.Complete);
                    table.Add("completetext", completetext);
                    table.Add("infourl", "/Finding/FinderDetail?id=" + item.ID.ToString());
                    infolist.Add(table);
                }
            }
            return Ok(new
            {
                infolist = infolist,
                total = all.Count,
                code = code
            });
        }

        [HttpPost]
        public IActionResult Search()
        {
            int code = 200;
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string text = Request.Form["searchtext"];
            string temp = Request.Form["missingorfind"];
            string type = Request.Form["type"];
            string temp2 = Request.Form["index"];
            int index = (temp2 == "" || temp2 == null) ? 0 : Convert.ToInt32(temp2);
            int missingorfind = (temp == null) ? 0 : Convert.ToInt32(temp);
            int all = 0;
            type = (type == "") ? null : type;
            if (missingorfind == 1)
            {
                List<Owner> list = new List<Owner>();
                if (text != "")
                {
                    list = service.GetOwnerAndTextByType(type, text, index);
                    all = service.GetOwnerAndTextByType(type, text, 0).Count;
                    foreach (var item in list)
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
                        LoseType losttype = service.GetLoseTypesBuName(item.LoseType.Name);
                        Hashtable table = new Hashtable();
                        table.Add("id", item.User.ID);
                        table.Add("avatarURL", item.User.head);
                        table.Add("username", item.User.UserName);
                        table.Add("infoId", item.ID);
                        table.Add("infoTitle", item.Title);
                        table.Add("fatherType", losttype.FatherType.Name);
                        table.Add("type", item.LoseType.Name);
                        table.Add("hidden", item.hidden);
                        table.Add("complete", item.Complete);
                        table.Add("completetext", completetext);
                        table.Add("infourl", "/Missing/MissingDetail?id=" + item.ID.ToString());
                        infolist.Add(table);
                    }
                }
                else
                {
                    list = service.GetOwnerByType(type, index);
                    all = service.GetOwnerByType(type, 0).Count;
                    foreach (var item in list)
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
                        LoseType losttype = service.GetLoseTypesBuName(item.LoseType.Name);
                        Hashtable table = new Hashtable();
                        table.Add("id", item.User.ID);
                        table.Add("avatarURL", item.User.head);
                        table.Add("username", item.User.UserName);
                        table.Add("infoId", item.ID);
                        table.Add("infoTitle", item.Title);
                        table.Add("fatherType", losttype.FatherType.Name);
                        table.Add("type", item.LoseType.Name);
                        table.Add("hidden", item.hidden);
                        table.Add("complete", item.Complete);
                        table.Add("completetext", completetext);
                        table.Add("infourl", "/Missing/MissingDetail?id=" + item.ID.ToString());
                        infolist.Add(table);
                    }
                }
                return Ok(new
                {
                    infolist = infolist,
                    total = all,
                    code = code
                });
            }
            else if (missingorfind == 2)
            {
                List<Finder> list = new List<Finder>();
                if (text != "")
                {
                    list = service.GetFinderAndTextByType(type, text, index);
                    all = service.GetFinderAndTextByType(type, text, 0).Count;
                    foreach (var item in list)
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
                        LoseType losttype = service.GetLoseTypesBuName(item.LoseType.Name);
                        Hashtable table = new Hashtable();
                        table.Add("id", item.User.ID);
                        table.Add("avatarURL", item.User.head);
                        table.Add("username", item.User.UserName);
                        table.Add("infoId", item.ID);
                        table.Add("infoTitle", item.Title);
                        table.Add("fatherType", losttype.FatherType.Name);
                        table.Add("type", item.LoseType.Name);
                        table.Add("hidden", item.hidden);
                        table.Add("complete", item.Complete);
                        table.Add("completetext", completetext);
                        table.Add("infourl", "/Finding/FinderDetail?id=" + item.ID.ToString());
                        infolist.Add(table);
                    }
                }
                else
                {
                    list = service.GetFinderByType(type, index);
                    all = service.GetFinderByType(type, 0).Count;
                    foreach (var item in list)
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
                        LoseType losttype = service.GetLoseTypesBuName(item.LoseType.Name);
                        Hashtable table = new Hashtable();
                        table.Add("id", item.User.ID);
                        table.Add("avatarURL", item.User.head);
                        table.Add("username", item.User.UserName);
                        table.Add("infoId", item.ID);
                        table.Add("infoTitle", item.Title);
                        table.Add("fatherType", losttype.FatherType.Name);
                        table.Add("type", item.LoseType.Name);
                        table.Add("hidden", item.hidden);
                        table.Add("complete", item.Complete);
                        table.Add("completetext", completetext);
                        table.Add("infourl", "/Finding/FinderDetail?id=" + item.ID.ToString());
                        infolist.Add(table);
                    }
                }
                return Ok(new
                {
                    infolist = infolist,
                    total = all,
                    code = code
                });
            }
            else
            {
                code = 500;
                return Ok(new
                {
                    infolist = infolist,
                    total = all,
                    code = code
                });
            }
        }
    }
}
