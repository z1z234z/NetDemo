using System;
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
        public IActionResult GetUserInfo()
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
