using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Models;
using Demo.Service;
using System.Collections;

namespace CoremvcDemo.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService service;

        public LoginController(DBContext context)
        {
            service = new LoginService(context);

        }

        // GET: Admins
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult testlogin()
        {
            string comfirm = "false";
            Hashtable result = new Hashtable();
            //前端向后端发送数据
            string account = Request.Form["account"];
            string password = Request.Form["password"];
            if (service.AccountComfirm(account, password))
            {
                User user = service.GetUserByAccount(account, password);
                result.Add("username", user.Name);
                result.Add("avatarURL", "http://localhost:25978/wwwroot/upload/head/default.jpg");
                result.Add("id", user.ID);
                result.Add("account", user.Account);
                comfirm = "true";

            }
            return Ok(new
            {
                userinfo = result,
                comfirm = comfirm,
                code = 200
            });
        }

        [HttpPost]
        public IActionResult accountRepeatable()
        {
            bool result = false;
            //前端向后端发送数据
            string account = Request.Form["account"];
            if (service.CheckAccount(account))
            {
                result = true;
            }
            return Ok(new
            {
                result = result,
                code = 200
            });
        }

        [HttpPost]
        public IActionResult register()
        {
            string result = "false";
            //前端向后端发送数据
            string account = Request.Form["accountreg"];
            string password = Request.Form["passwordreg"];
            string email = Request.Form["emailreg"];
            string username = Request.Form["usernamereg"];
            string name = Request.Form["name"];
            string sex = Request.Form["gender"];
            string temp = Request.Form["age"];
            int age = (temp == null) ? 0 : Convert.ToInt32(Request.Form["age"]);
            string school = Request.Form["school"];
            string phone = Request.Form["phone"];
            string address = Request.Form["address"];
            User user = new User();
            user.Account = account;
            user.Password = password;
            user.UserName = username;
            user.email = email;
            user.Name = name;
            user.Sex = sex;
            user.Age = age;
            user.School = school;
            user.Phone = phone;
            user.Address = address;
            if (service.Register(user))
            {
                result = "true";
            }
            return Ok(new
            {
                result = result,
                code = 200
            });
        }
    }
}
