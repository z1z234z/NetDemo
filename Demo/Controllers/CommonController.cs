﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Service;
using Demo.Models;
using System.Collections;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Demo.Controllers
{
    public class CommonController : Controller
    {
        private readonly CommonService service;

        public CommonController(DBContext context)
        {
            service = new CommonService(context);

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public class TemUrl
        {
            public int id { get; set; }
            public string url { get; set; }
        }

        [HttpPost]
        public IActionResult getChoiceType()
        {
            int code = 200;
            List<Hashtable> typelist = new List<Hashtable>();
            //前端向后端发送数据
            List<LoseType> list = service.GetLoseTypes();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].FatherType == null)
                {
                    Hashtable table = new Hashtable();
                    table.Add("value", list[i].Name);
                    table.Add("label", list[i].Name);
                    List<LoseType> childrenlists = service.GetChildrenLoseTypes(list[i]);
                    List<Hashtable> childrentypelist = new List<Hashtable>();
                    for (int j = 0; j < childrenlists.Count; j++)
                    {
                        Hashtable childrentable = new Hashtable();
                        childrentable.Add("value", childrenlists[j].Name);
                        childrentable.Add("label", childrenlists[j].Name);
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
                options = typelist,
                code = code
            });
        }

        [HttpPost]
        public IActionResult SendPrivateMessage()
        {
            bool result = false;
            //前端向后端发送数据
            string account = Request.Form["account"];
            User user = service.getUserInfo(account);
            if (user != null)
            {
                result = true;
                return Ok(new
                {
                    user = user,
                    result = result,
                    code = 200
                });
            }
            else
            {
                return Ok(new
                {
                    user = user,
                    result = result,
                    code = 200
                });
            }
        }


    }
}
