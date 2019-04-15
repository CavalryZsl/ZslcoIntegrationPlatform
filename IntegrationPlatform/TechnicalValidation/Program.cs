using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TechnicalValidation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// 构建webserver 返回一个实现了IWebHostBuilder的对象WebHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            //运行默认的设置 Kestrel WEB servre 内置的跨平台的
            //UseIIS() 启动运行时环境 并使用Inprocess模式
            //UseIISIntergration()如果程序运行于IIS那么这个方法将允许IIS通过Windows的凭证验证到Kestrel
            //LOG配置
            //IConfiguration 实现了此类的Configuration获取配置信息
            /*
             * 配置信息的来源
             * appsetting.json
             * User Secrets
             * 环境变量
             * 命令行参数
             * 优先级:从源码中看 先加载json文件,加载dev.json文件 加载User Secrets,加载环境变量,最后加载命令行启动参数
             */
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();//实例化Startup 并调用类里面的两个方法 首先调用ConfigureServices 再调用 Configure
    }
}
