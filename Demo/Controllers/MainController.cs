using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Service;
using Demo.Models;
using System.Collections;
using System.IO;

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
            for(int i = 0; i < list.Count/2; i++)
            {
                Hashtable table = new Hashtable();
                table.Add("index", (i+1).ToString());
                table.Add("type", list[i].Name);
                List<LoseType> childrenlists = service.GetLoseTypes();
                List<Hashtable> childrentypelist = new List<Hashtable>();
                for (int j = 3; j < childrenlists.Count; j++)
                {
                    Hashtable childrentable = new Hashtable();
                    childrentable.Add("index", (i + 1).ToString() + "-" + (j + 1).ToString());
                    childrentable.Add("type", childrenlists[i].Name);
                    childrentypelist.Add(childrentable);
                }
                table.Add("children", childrentypelist);
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
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string type = Request.Form["type"];
            string temp = Request.Form["index"];
            int index = (temp == "" || type == null) ? 0 : Convert.ToInt32(temp);
            type = (type == "" || type == null) ? null : type;
            list = service.GetOwnerByType(type, index);
            if (list == null)
            {
                code = 500;
            }
            else{
                foreach(Owner item in list){
                    String completetext = null;
                    if (item.Complete)
                    {
                        completetext = "已找到";
                    }
                    else
                    {
                        completetext = "未找到";
                    }
                    Hashtable table = new Hashtable();
                    table.Add("id",item.User.ID);
                    table.Add(" avatarURL", "http://localhost:25978/wwwroot/upload/head/default.jpg");
                    table.Add("username", item.User.UserName);
                    table.Add("infoId", item.ID);
                    table.Add("infoTitle", "default");
                    table.Add("fatherType", "fathertype");
                    table.Add("type", item.LoseType.Name);
                    table.Add("hidden", item.hidden);
                    table.Add("complete", item.Complete);
                    table.Add("completetext", completetext);
                    table.Add("infourl", "http://localhost:25978/Missing/MissingDetail?id="+item.ID.ToString());
                    infolist.Add(table);
                }
            }
            return Ok(new
            {
                infolist = infolist,
                code = code
            });
        }

        [HttpPost]
        public IActionResult getFinderByType()
        {
            int code = 200;
            List<Finder> list = new List<Finder>();
            List<Hashtable> infolist = new List<Hashtable>();
            //前端向后端发送数据
            string type = Request.Form["type"];
            string temp = Request.Form["index"];
            int index = (temp == "" || type == null) ? 0 : Convert.ToInt32(temp);
            type = (type == "") ? null : type;
            list = service.GetFinderByType(type, index);
            if (list == null)
            {
                code = 500;
            }
            else
            {
                foreach (Finder item in list)
                {
                    String completetext = null;
                    if (item.Complete)
                    {
                        completetext = "已找到";
                    }
                    else
                    {
                        completetext = "未找到";
                    }
                    Hashtable table = new Hashtable();
                    table.Add("id", item.User.ID);
                    table.Add(" avatarURL", "http://localhost:59823/wwwroot/images/head/default.jpg");
                    table.Add("username", item.User.UserName);
                    table.Add("infoId", item.ID);
                    table.Add("infoTitle", "default");
                    table.Add("fatherType", "fathertype");
                    table.Add("type", item.LoseType.Name);
                    table.Add("hidden", item.hidden);
                    table.Add("complete", item.Complete);
                    table.Add("completetext", completetext);
                    table.Add("infourl", "http://localhost:59823/Finding/FinderDetail?id=" + item.ID.ToString());
                    infolist.Add(table);
                }
            }
            return Ok(new
            {
                infolist = infolist,
                code = code
            });
        }

        [HttpPost]
        public IActionResult Search()
        {
            int code = 200;
            List<Finder> finderlist = new List<Finder>();
            List<Owner> ownerlist = new List<Owner>();
            //前端向后端发送数据
            string text = Request.Form["searchtext"];
            string temp = Request.Form["missingorfind"];
            string type = Request.Form["type"];
            int missingorfind = (temp == null) ? 0 : Convert.ToInt32(temp);
            type = (type == "") ? null : type;
            if (missingorfind == 1)
            {
                List<Owner> list = service.GetOwnerByType(type, 0);
                foreach(Owner item in list)
                {
                    if (item.Content.Contains(text)/*|| item.Title.Contains(text)*/)
                    {
                        ownerlist.Add(item);
                    }
                }
                return Ok(new
                {
                    infolist = ownerlist,
                    code = code
                });
            }
            else if (missingorfind == 2)
            {
                List<Finder> list = service.GetFinderByType(type, 0);
                foreach (Finder item in list)
                {
                    if (item.Content.Contains(text)/*|| item.Title.Contains(text)*/)
                    {
                        finderlist.Add(item);
                    }
                }
                return Ok(new
                {
                    infolist = finderlist,
                    code = code
                });
            }
            else
            {
                code = 500;
                return Ok(new
                {
                    infolist = "",
                    code = code
                });
            }
        }
    }
}
