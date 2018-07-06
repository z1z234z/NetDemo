using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.Service;
using System.Collections;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    public class FinderController : Controller
    {
        private readonly FindingService service;

        public FinderController(DBContext context)
        {
            service = new FindingService(context);

        }
        // GET: /<controller>/
        public IActionResult FinderDetail(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                Finder finder = service.getDetail(id.Value);
                return View(finder);
            }
        }

        [HttpPost]
        public IActionResult Detail()
        {
            Hashtable result = new Hashtable();
            //前端向后端发送数据
            String temp = Request.Form["id"];
            int id = (temp == null) ? 0 : Convert.ToInt32(temp);
            Finder finder = service.getDetail(id);
            if (finder != null)
            {
                result.Add("complete", finder.Complete);
                result.Add("finderContent", finder.Content);
                result.Add("createtime", finder.Time);
                result.Add("type", finder.LoseType);
                result.Add("account", finder.User.Account);
                result.Add("finderTitle", finder.Title);
                result.Add("finderId", finder.ID);
                result.Add("question", finder.Question);
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
