using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.Service;

namespace Demo.Controllers
{
    public class MissingController : Controller
    {
        private readonly MissingService service;

        public MissingController(DBContext context)
        {
            service = new MissingService(context);

        }
        public IActionResult MissingDetail()
        {
            return View();
        }

        public IActionResult test()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reply()
        {
            bool result = false;
            //前端向后端发送数据
            String temp = Request.Form["id"];
            String content = Request.Form["content"];
            String account = Request.Form["account"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            if (service.saveReply(id, content, account))
            {
                result = true;
            }
            return Ok(new
            {
                result = result,
                code = 200,
            });
        }

        [HttpPost]
        public IActionResult Comment()
        {
            bool result = false;
            //前端向后端发送数据
            String temp = Request.Form["id"];
            String content = Request.Form["content"];
            String account = Request.Form["account"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            if (service.saveComment(id, content, account))
            {
                result = true;
            }
            return Ok(new
            {
                result = result,
                code = 200,
            });
        }

        [HttpPost]
        public IActionResult Detail()
        {
            bool result = false;
            //前端向后端发送数据
            String temp = Request.Form["id"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            Owner owner = service.getDetail(id);
            if (owner != null)
            {
                result = true;
                return Ok(new
                {
                    complete = owner.Complete,
                    content = owner.Content,
                    createtime = owner.Time,
                    type = owner.LoseType,
                    user = owner.User,
                    result = result,
                    code = 200,
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
                    result = result,
                    code = 500,
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
            Owner owner = service.getDetail(id);
            if (owner != null)
            {
                owner.Complete = true;
                result = service.completed(owner);
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