using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Models;
using Demo.Service;

namespace CoremvcDemo.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _service;

        public LoginController(DBContext context)
        {
            _service = new LoginService(context);

        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult testlogin()
        {
            string result = "false";
            //前端向后端发送数据
            string account = Request.Form["username"];
            string password = Request.Form["password"];
            if (_service.AccountComfirm(account, password))
            {
                result = "true";

            }
            return Ok(new
            {
                comfirm = result
            });
        }

        [HttpPost]
        public IActionResult register()
        {
            string result = "false";
            //前端向后端发送数据
            string account = Request.Form["username"];
            string password = Request.Form["password"];
            string email = Request.Form["email"];
            string name = Request.Form["name"];
            string sex = Request.Form["sex"];
            string age = Request.Form["age"];
            string school = Request.Form["school"];
            string phone = Request.Form["phone"];
            string address = Request.Form["address"];
            User user = new User();
            user.Account = account;
            user.Password = password;
            user.email = email;
            user.Name = name;
            user.Sex = sex;
            user.Age = age;
            user.School = school;
            user.Phone = phone;
            user.Address = address;
            if (_service.Register(user))
            {
                result = "true";
            }
            return Ok(new
            {
                success = result
            });
        }
    }
}
