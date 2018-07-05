using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    public class FindingController : Controller
    {
        private readonly FindingService service;

        public FindingController(DBContext context)
        {
            service = new FindingService(context);

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Detail()
        {
            bool result = false;
            //前端向后端发送数据
            String temp = Request.Form["id"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            Finder finder = service.getDetail(id);
            if (finder != null)
            {
                result = true;
                return Ok(new
                {
                    complete = finder.Complete,
                    content = finder.Content,
                    createtime = finder.Time,
                    type = finder.LoseType,
                    user = finder.User,
                    question = finder.Question,
                    answer = finder.Answer,
                    result = result,
                    code = 200
                });
            }
            else
            {
                return Ok(new
                {
                    complete = "",
                    content = "",
                    createtime = "",
                    type = "",
                    user = "",
                    queation = "",
                    answer = "",
                    result = result,
                    code = 500
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
            Finder finder = service.getDetail(id);
            if (finder != null)
            {
                finder.Complete = true;
                result = service.completed(finder);
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
    }
}
