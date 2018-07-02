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
                comfirm = result,
            });
        }
    }
}
