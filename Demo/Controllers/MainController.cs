using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Service;
using Demo.Models;
using System.Collections;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    public class MainController : Controller
    {
        private readonly MainService service;

        public MainController(DBContext context)
        {
            service = new MainService(context);

        }
        // GET: /<controller>/
        public IActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public IActionResult getType()
        {
            int code = 200;
            List<Hashtable> typelist = new List<Hashtable>();
            //前端向后端发送数据
            List<LoseType> list = service.GetLoseTypes();
            for(int i = 0; i < list.Count; i++)
            {
                Hashtable table = new Hashtable();
                table.Add("index", i);
                table.Add("type", list[i].Name);
                table.Add("children", new List<Hashtable>());
                typelist.Add(table);
            }
            if (list == null)
            {
                code = 500;
            }
            return Ok(new
            {
                typelist = typelist,
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
            int index = (temp == null) ? 0 : Convert.ToInt32(temp);
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
            int index = (temp == null) ? 0 : Convert.ToInt32(temp);
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
