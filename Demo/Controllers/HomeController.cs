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
        private readonly HomeService _service;

        public HomeController(DBContext context)
        {
            _service = new HomeService(context);

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
            list = _service.GetLoseTypes();
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
            list = _service.GetOwnerByType(type);
            if (list == null)
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
        public IActionResult getFinderByType()
        {
            int code = 200;
            List<Finder> list = new List<Finder>();
            //前端向后端发送数据
            string type = Request.Form["type"];
            list = _service.GetFinderByType(type);
            if (list == null)
            {
                code = 500;
            }
            return Ok(new
            {
                typelist = list,
                code = code
            });
        }
    }
}
