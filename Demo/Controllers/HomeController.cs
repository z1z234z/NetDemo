﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.Service;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService service;

        public HomeController(DBContext context)
        {
            service = new HomeService(context);

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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
        public IActionResult getType()
        {
            int code = 200;
            List<LoseType> list = new List<LoseType>();
            //前端向后端发送数据
            list = service.GetLoseTypes();
            if(list == null)
            {
                code = 500;
            }
            return Ok(new
            {
                typelist = list,
                code = code
            });
        }

        [HttpPost]
        public IActionResult getOwnerByType()
        {
            int code = 200;
            List<Owner> list = new List<Owner>();
            //前端向后端发送数据
            string type = Request.Form["type"];
            string temp = Request.Form["index"];
            int index = (temp == null) ? 0 : Convert.ToInt32(Request.Form["index"]);
            type = (type == "") ? null : type;
            list = service.GetOwnerByType(type, index);
            if (list == null)
            {
                code = 500;
            }
            return Ok(new
            {
                ownerlist = list,
                code = code
            });
        }

        [HttpPost]
        public IActionResult getFinderByType()
        {
            int code = 200;
            List<Finder> list = new List<Finder>();
            //前端向后端发送数据
            string type = Request.Form["type"];
            string temp = Request.Form["index"];
            int index = (temp == null) ? 0 : Convert.ToInt32(Request.Form["index"]);
            type = (type == "") ? null : type;
            list = service.GetFinderByType(type, index);
            if (list == null)
            {
                code = 500;
            }
            return Ok(new
            {
                finderlist = list,
                code = code
            });
        }
    }
}
