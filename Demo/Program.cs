using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JiebaNet.Analyser;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ExtractTagsDemo();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        /*public static void ExtractTagsDemo()
        {
            var text = "程序员(英文Programmer)是从事程序开发、维护的专业人员。一般将程序员分为程序设计人员和程序编码人员，但两者的界限并不非常清楚，特别是在中国。软件从业人员分为初级程序员、高级程序员、系统分析员和项目经理四大类。";
            var extractor = new TfidfExtractor();
            var keywords = extractor.ExtractTags(text);
            foreach (var keyword in keywords)
            {
                Console.WriteLine(keyword);
            }
        }*/
    }
}
