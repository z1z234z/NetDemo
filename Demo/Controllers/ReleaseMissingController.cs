using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Demo.Models;
using Demo.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    public class ReleaseMissingController : Controller
    {
        private readonly ReleaseMissingService service;

        public ReleaseMissingController(DBContext context)
        {
            service = new ReleaseMissingService(context);

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public class TemUrl
        {
            public int id { get; set; }
            public string url { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpload([FromServices]IHostingEnvironment environment)
        {
            List<TemUrl> list = new List<TemUrl>();
            var files = Request.Form.Files;
            string webRootPath = environment.WebRootPath;
            string contentRootPath = environment.ContentRootPath;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + ".jpg";
                    var path = Path.Combine(environment.WebRootPath + "\\images\\upload", fileName);
                    using (var stream = new FileStream(path, FileMode.CreateNew))
                    {
                        await formFile.CopyToAsync(stream);
                        TemUrl tu = new TemUrl();
                        tu.url = @"/images/upload/" + fileName;
                        list.Add(tu); ;
                    }
                }
            }

            return new JsonResult(list);

        }

        [HttpPost]
        public IActionResult Submit()
        {
            bool result = false;
            //前端向后端发送数据
            String title = Request.Form["title"];
            String temp = Request.Form["type"];
            String content = Request.Form["content"];
            String account = Request.Form["account"];
            String[] types = temp.Split('/');
            String type = types[1];
            if (service.saveInfomation(title, type, content, account))
            {
                result = true;
            }
            return Ok(new
            {
                result = result,
                code = 200,
            });
        }
    }
}
